using AutoMapper;
using BaseClasses.Contracts;
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

namespace PostFeedback.Domain.Logic.Services
{
    public class FeedbackService<T, E> : IFeedbackService<T, E> where T : FeedbackEntity
                                                             where E: IFeedbackEntityDTO
    {
        private readonly IRepositoryWithIncludes<Feedback<T>> _feedbackRepository;
        private readonly IRepository<T> _itemRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IFeedbackValidationService<T> _feedbackvalidator;
        private readonly IMapper _mapper;
        private readonly AbstractValidator<FeedbackDTO> _validator;

        public FeedbackService(
            IRepositoryWithIncludes<Feedback<T>> feedbackRepository,
            IRepository<T> itemRepository,
            IRepository<User> userRepository,
            IFeedbackValidationService<T> feedbackvalidator,
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

        public async Task DeleteFeedbackAsync(long id)
        {
            var feedback = await _feedbackRepository.GetByIdAsync(id);

            if (feedback == null)
            {
                throw new NotFoundException(id,"Feedback");
            }
            await _feedbackRepository.DeleteAsync(feedback);
        }

        public async Task<FeedbackInfoDTO<E>> GetFeedbackDetailsAsync(long id)
        {
            var feedback = await _feedbackRepository.GetByIdAsync(id, relatedEntitiesIncluded: true);
            if (feedback != null)
            {
                var feedbackDTO = _mapper.Map<FeedbackInfoDTO<E>>(feedback);
                return feedbackDTO;
            }
            return null;
        }

        public async Task<ICollection<FeedbackInfoDTO<E>>> GetFeedbacksAsync(long itemId)
        {
            var feedbacks = await _feedbackRepository.GetFilteredAsync(x => x.ItemId == itemId,
                                                        relatedEntitiesIncluded: true);
            var feedbacksDTO = _mapper.Map<ICollection<FeedbackInfoDTO<E>>>(feedbacks);
            return feedbacksDTO;
        }

        public async Task LeaveFeedbackAsync(FeedbackDTO feedbackDTO)
        {
            //validate if user can leave feedback or not
            if(!await _feedbackvalidator.CanLeaveFeedback(feedbackDTO))
            {
                throw new LeaveFeedbackException((long)feedbackDTO.UserId, feedbackDTO.ItemId);
            }
            var result = _validator.Validate(feedbackDTO);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            if(!_itemRepository.DoesItemWithIdExist(feedbackDTO.ItemId))
            {
                throw new ForeignKeyViolationException("Item");
            }
            if(!_userRepository.DoesItemWithIdExist(feedbackDTO.UserId??0))
            {
                throw new ForeignKeyViolationException(nameof(User));
            }
            var feedback = _mapper.Map<Feedback<T>>(feedbackDTO);
            await _feedbackRepository.CreateAsync(feedback);
        }
    }
}
