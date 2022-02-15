using Account.Domain.Entities;
using Account.Domain.Enums;
using Account.Domain.Logic.Contracts;
using Account.Domain.Logic.DTOs;
using Account.Domain.Logic.Exceptions;
using Account.Domain.Logic.IntegrationEvents.Events;
using Account.Domain.Logic.Services.Core;
using Account.PasswordHandling;
using AutoMapper;
using BaseClasses.Contracts;
using BaseClasses.Exceptions;
using EventBus.Contracts;
using FluentValidation;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.Services
{
    //user CRUD service
    public class UserService : BaseService, IUserService
    {
        private readonly AbstractValidator<UserRegistrationDTO> _registrationValidator;
        private readonly AbstractValidator<UserBaseDTO> _userValidator;
        private readonly IRepository<Country> _countryRepository;
        private readonly AbstractValidator<IPasswordBaseDTO> _passwordValidator;
        private readonly IEventBus _eventBus;
        public UserService(
            IRepository<User> repository,
            AbstractValidator<UserRegistrationDTO> registrationValidator,
            IPasswordHandlingService pwdService,
            IRepository<Country> countryRepository,
            AbstractValidator<UserBaseDTO> userValidator,
            IMapper mapper,
            AbstractValidator<IPasswordBaseDTO> passwordValidator,
            IEventBus eventBus)
            : base(
                  repository,
                  mapper,
                  pwdService)
        {
            _registrationValidator = registrationValidator;
            _countryRepository = countryRepository;
            _userValidator = userValidator;
            _passwordValidator = passwordValidator;
            _eventBus = eventBus;
        }
        //method for changing password
        public async Task ChangePasswordAsync(ChangePasswordDTO passwordDTO)
        {
            //find user in the databasea and throw exception if does not exist
            var user = await FindUserAsync(passwordDTO.UserId);
            //if user exists, validate new password
            var result = _passwordValidator.Validate(passwordDTO);
            if (result.IsValid)
            {
                //if password is valid, create new salt and encrypt new password
                var salt = _pwdService.GetSalt();
                var hashedPassword = _pwdService.HashPassword(salt, passwordDTO.Password);
                //update user information and save in the database
                user.SetPassword(hashedPassword, salt);
                await _repository.UpdateAsync(user);
            }
            else
            {
                //show validation errors in the exception if password is not valid
                throw new ValidationException(result.Errors);
            }

        }
        //method for deleting account
        public async Task DeleteUserAsync(long id)
        {
            //find user in the database and throw exception if does not exist
            //include user bookings to check whether this user can be deleted
            var user = await FindUserAsync(id, x=>x.BookingsAsGuest, x=>x.BookingsAsOwner);
            //check if user has active bookings as owner or guest (active - booking which has not expired yet)
            //if user has active bookings, account cannot be deleted
            if ((user.BookingsAsGuest?.Any(x => x.EndDate > DateTime.Now) ?? false) ||
               (user.BookingsAsOwner?.Any(x => x.EndDate > DateTime.Now) ?? false))
            {
                throw new DeleteUserException();
            }
            //delete user
            await _repository.DeleteAsync(id);
            //if user is deleted successfully and no exception is thrown, publish event that user is deleted
            var integrationEvent = new UserDeletedIntegrationEvent(id);
            _eventBus.Publish(integrationEvent);
        }
        //method for account registration
        public async Task RegisterUserAsync(UserRegistrationDTO userDTO)
        {
            //validate new user
            ServiceHelper.ValidateItem(_registrationValidator, userDTO);
            //if validation is successful, check if user with the same email exists
            //if exists, throw exception
            await CheckUserEmailAsync(userDTO.Email);
            //check if indicated country exists in the database
            ServiceHelper.CheckIfRelatedEntityExists(userDTO.CountryId, _countryRepository);
            //encrypt password for user
            string salt = _pwdService.GetSalt();
            string hashedPassword = _pwdService.HashPassword(salt, userDTO.Password);
            var user = _mapper.Map<User>(userDTO);
            user.SetPassword(hashedPassword, salt);
            //create user
            await _repository.CreateAsync(user);
            //if user is registered successfully and no exception is thrown, publish event that user is registered
            var integrationEvent = _mapper.Map<UserCreatedOrUpdatedIntegrationEvent>(user);
            _eventBus.Publish(integrationEvent);
        }
        //method for updating account information
        public async Task UpdateUserAsync(UserUpdateDTO userDTO)
        {
            //find user in the database and throw exception if does not exist
            var user = await FindUserAsync(userDTO.Id);
            //validate user
            ServiceHelper.ValidateItem(_userValidator, userDTO);
            //if validation is successful, check if user with the same email exists (except email of user who updates information)
            //if exists, throw exception
            await CheckUserEmailAsync(userDTO.Email, userDTO.Id);
            //check if indicated country exists in the database
            ServiceHelper.CheckIfRelatedEntityExists(userDTO.CountryId, _countryRepository);
            //if all correct, update user entity and save in the database
            user.UpdateInfo(userDTO.FirstName, userDTO.LastName,
                userDTO.Email, userDTO.PhoneNumber, (DateTime)userDTO.DateOfBirth,
                (Gender)userDTO.Gender, userDTO.Address,
                userDTO.CountryId, userDTO.UserInfo);
            await _repository.UpdateAsync(user);
            //if user is updated successfully and no exception is thrown, publish event that user is updated
            var integrationEvent = _mapper.Map<UserCreatedOrUpdatedIntegrationEvent>(user);
            _eventBus.Publish(integrationEvent);
        }
        //method for retrieving all users including related entitites (country)
        public async Task<ICollection<UserInfoDTO>> GetAllUsersAsync()
        {
            var users = (await _repository.GetAllAsync(x=>x.Country)).ToList();
            //map domain entities to dtos
            return _mapper.Map<ICollection<User>, ICollection<UserInfoDTO>>(users);
        }
        //method for retrieving user information by id including related entitites (country)
        public async Task<UserInfoDTO> GetUserInfoAsync(long id)
        {
            var user = await _repository.GetByIdAsync(id, x=>x.Country);
            //if user exists, map domain entity to dto and return it
            if (user != null)
            {
                return _mapper.Map<UserInfoDTO>(user);
            }

            return null;
        }

        //method for finding user, called within password change, user update and user delete methods
        private async Task<User> FindUserAsync(long id, params Expression<Func<User, object>>[] includes)
        {
            //get user by id
            var user = await _repository.GetByIdAsync(id, includes);
            if (user == null)
            {
                //if user does not exist, throw exception
                throw new NotFoundException(id, nameof(User));
            }
            return user;
        }
        //method that allows to check uniqueness of user email
        //since filtering criteria for checking email are slightly different for registration and update methods
        //userId parameter is needed
        //if user id is not null, then method is called for user update
        //otherwise, method is called for user registration
        private async Task CheckUserEmailAsync(string email, long? userId=null)
        {
            User userWithEmail = null;
            //if email is checked for update method, filter by given criteria
            if(userId!=null)
            {
                //get user with the same email, but not the user whose information is getting updated
                userWithEmail = (await _repository.GetFilteredAsync
                    (u => u.Email == email && u.Id != userId)).FirstOrDefault();
            }
            //if email is checked for register method, filter by given criteria
            else
            {
                //get user with the same email
                userWithEmail = (await _repository.GetFilteredAsync
                    (u => u.Email == email)).FirstOrDefault();
            }
            if (userWithEmail != null)
            {
                //if email already exists, throw exception
                throw new UniqueConstraintViolationException("Email", email);
            }
        }
       
    }
}
