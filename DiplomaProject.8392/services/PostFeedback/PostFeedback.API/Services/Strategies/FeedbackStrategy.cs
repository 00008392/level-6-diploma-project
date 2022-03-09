using AutoMapper;
using PostFeedback.Domain.Entities;
using PostFeedback.Domain.Logic.Contracts;
using PostFeedback.Domain.Logic.DTOs;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Base.Helpers;

namespace PostFeedback.API.Services.Strategies
{
    //generic strategy for feedback handling
    //needed for user feedback and post feedback handling to reduce code duplication
    public class FeedbackStrategy<TEntity, TDTO> : IFeedbackStrategy<TEntity, TDTO> where TEntity : FeedbackEntity
                                                                  where TDTO : IFeedbackEntityDTO
    {
        private readonly IMapper _mapper;
        //inject service from domain logic layer
        private readonly IFeedbackService<TEntity, TDTO> _service;
        private readonly IFeedbackValidationService<TEntity> _validationService;

        public FeedbackStrategy(
            IMapper mapper,
            IFeedbackService<TEntity, TDTO> service,
            IFeedbackValidationService<TEntity> validationService)
        {
            _mapper = mapper;
            _service = service;
            _validationService = validationService;
        }

        public async Task<CanLeaveFeedbackResponse> CanLeaveFeedbackAsync(CanLeaveFeedbackRequest request)
        {
            var response = await _validationService.CanLeaveFeedback(request.CreatorId ?? 0,
                request.ItemId ?? 0);
            return new CanLeaveFeedbackResponse
            {
                CanLeaveFeedback = response
            };
        }

        //delete feedback
        public async Task<Response> DeleteFeedbackAsync(Request request)
        {
            //call helper method that handles delete grpc action
            return await GrpcServiceHelper.HandleDeleteActionAsync<Response>
                 (request.Id, _service.DeleteFeedbackAsync);
        }
        //get average rating for item
        public async Task<AverageRatingResponse> GetAverageRatingAsync(Request request)
        {
            var response = new AverageRatingResponse();
            var rating = await _service.GetAverageRating(request.Id);
            if(rating==null)
            {
                response.NoRating = true;
            }
            else
            {
                response.Rating = rating;
            }
            return response;
        }

        //retrieve feedback information by id
        public async Task<FeedbackResponse> GetFeedbackDetailsAsync(Request request)
        {
            //call helper method that handles retrieval by id grpc action
            return await GrpcServiceHelper.HandleRetrievalByIdAsync<FeedbackResponse, FeedbackInfoDTO<TDTO>>
                  (request.Id, _service.GetFeedbackDetailsAsync, _mapper);
        }
        //retrieve feedbacks by id of item on which feedback is left
        public async Task<FeedbackListResponse> GetFeedbacksForItemAsync(Request request)
        {
            //get feedbacks
            var feedbacksDTO = await _service.GetFeedbacksForItemAsync(request.Id);
            //map to response
            return GrpcServiceHelper.MapItems<FeedbackListResponse, FeedbackInfoDTO<TDTO>,
                FeedbackResponse>(_mapper, feedbacksDTO);
        }
        //create new feedback
        public async Task<Response> LeaveFeedbackAsync(CreateFeedbackRequest request)
        {
            //call helper method that handles create and update grpc actions
            return await GrpcServiceHelper.HandleCreateUpdateActionAsync<FeedbackDTO, Response,
                CreateFeedbackRequest>(_service.LeaveFeedbackAsync, _mapper, request);
        }
    }
}
