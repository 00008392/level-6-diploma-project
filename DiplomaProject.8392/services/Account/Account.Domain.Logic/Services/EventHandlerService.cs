using Account.Domain.Entities;
using Account.Domain.Logic.Contracts;
using Account.Domain.Logic.DTOs;
using Account.Domain.Logic.Exceptions;
using Account.Domain.Logic.Services.Core;
using AutoMapper;
using BaseClasses.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.Services
{
    public class EventHandlerService : BaseService, IEventHandlerService
    {
        private readonly IRepository<Booking> _bookingRepository;
        public EventHandlerService(IRepositoryWithIncludes<User> repository,
                                   IRepository<Booking> bookingRepository,
                                   IMapper mapper): base(repository, mapper)
        {
            _bookingRepository = bookingRepository;
        }
        public async Task CreateBooking(AddBookingDTO bookingDTO)
        {
           if(!_repository.DoesItemWithIdExist(bookingDTO.GuestId) ||
                !_repository.DoesItemWithIdExist(bookingDTO.OwnerId))
            {
                throw new ForeignKeyViolationException("User");
            }
            var booking = _mapper.Map<Booking>(bookingDTO);
            await _bookingRepository.CreateAsync(booking);
        }

        public async Task DeleteBooking(long id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if(booking==null)
            {
                throw new NotFoundException(id, nameof(Booking));
            }
            await _bookingRepository.DeleteAsync(booking);
        }
    }
}
