using AutoMapper;
using FluentValidation;
using PostFeedback.Domain.Entities;
using PostFeedback.Domain.Logic.Contracts;
using PostFeedback.Domain.Logic.DTOs;
using PostFeedback.Domain.Logic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Base.Contracts;
using Domain.Logic.Base.Exceptions;
using Domain.Logic.Base.Helpers;

namespace PostFeedback.Domain.Logic.Services
{
    //service for feedback manipulations and retrieval
    //TEntity - user/post
    //TDTO - userDTO, postDetailsDTO
    public class FeedbackService<TEntity, TDTO> : IFeedbackService<TEntity, TDTO> where TEntity : FeedbackEntity
                                                             where TDTO : IFeedbackEntityDTO
    {
        private readonly IRepository<Feedback<TEntity>> _feedbackRepository;
        private readonly IRepository<TEntity> _itemRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IFeedbackValidationService<TEntity> _feedbackvalidator;
        private readonly IMapper _mapper;
        private readonly AbstractValidator<FeedbackDTO> _validator;

        public FeedbackService(
            IRepository<Feedback<TEntity>> feedbackRepository,
            IRepository<TEntity> itemRepository,
            IRepository<User> userRepository,
            IFeedbackValidationService<TEntity> feedbackvalidator,
            IMapper mapper,
            AbstractValidator<FeedbackDTO> validator)
        {
            _feedbackRepository = feedbackRepository;
            _itemRepository = itemRepository;
            _userRepository = userRepository;
            _feedbackvalidator = feedbackvalidator;
            _mapper = mapper;
            _validator = validator;
        }
        //this method deletes feedback
        public async Task DeleteFeedbackAsync(long id)
        {
            //check if feedback exists
            if (!_feedbackRepository.DoesItemWithIdExist(id))
            {
                throw new NotFoundException(id, "Feedback");
            }
            //if exists, delete it from database
            await _feedbackRepository.DeleteAsync(id);
        }
        //get average rating for feedback item
        public async Task<double?> GetAverageRating(long itemId)
        {
            //get all feedbacks for item
            var feedbacks = await _feedbackRepository.GetFilteredAsync(x => x.ItemId == itemId);
            //if no feedbacks are left for item, no average rating is returned
            if(feedbacks==null||feedbacks.Count==0)
            {
                return null;
            }
            //calculate average rating
            var rating = feedbacks.Select(x => x.Rating).Average();
            return rating;
        }

        //this method retrieves feedback by id with related entities
        public async Task<FeedbackInfoDTO<TDTO>> GetFeedbackDetailsAsync(long id)
        {
            //if feedback exists, retrieve it and map to dto
            var feedback = await _feedbackRepository.GetByIdAsync(id, x=>x.Creator, x=>x.Item);
            if (feedback != null)
            {
                var feedbackDTO = _mapper.Map<FeedbackInfoDTO<TDTO>>(feedback);
                return feedbackDTO;
            }
            return null;
        }
        //this method retrieves feedbacks by id of item (either user or post) on which feedback is left
        //including feedback creator
        public async Task<ICollection<FeedbackInfoDTO<TDTO>>> GetFeedbacksForItemAsync(long itemId)
        {
            //filter feedbacks by item id and map to dtos
            var feedbacks = await _feedbackRepository.GetFilteredAsync(x => x.ItemId == itemId,
                                                        x=>x.Creator);
            var feedbacksDTO = _mapper.Map<ICollection<FeedbackInfoDTO<TDTO>>>(feedbacks);
            return feedbacksDTO;
        }
        //this method creates new feedback
        public async Task LeaveFeedbackAsync(FeedbackDTO feedbackDTO)
        {
            //validate feedback
            ServiceHelper.ValidateItem(_validator, feedbackDTO);
            //validate if user can leave feedback or not
            if (!await _feedbackvalidator.CanLeaveFeedback((long)feedbackDTO.CreatorId, feedbackDTO.ItemId))
            {
                throw new LeaveFeedbackException((long)feedbackDTO.CreatorId, feedbackDTO.ItemId);
            }

            //check if item on which feedback is left exists
            ServiceHelper.CheckIfRelatedEntityExists(feedbackDTO.ItemId, _itemRepository);
            //check if feedback creator exists
            ServiceHelper.CheckIfRelatedEntityExists(feedbackDTO.CreatorId??0, _userRepository);
            //if all correct, map dto to domain entity and insert into the database
            var feedback = _mapper.Map<Feedback<TEntity>>(feedbackDTO);
            await _feedbackRepository.CreateAsync(feedback);
        }
    }
}
