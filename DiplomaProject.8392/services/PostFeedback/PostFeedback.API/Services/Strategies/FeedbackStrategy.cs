using AutoMapper;
using Grpc.Helpers;
using FluentValidation;
using PostFeedback.Domain.Entities;
using PostFeedback.Domain.Logic.Contracts;
using PostFeedback.Domain.Logic.DTOs;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public FeedbackStrategy(
            IMapper mapper,
            IFeedbackService<TEntity, TDTO> service)
        {
            _mapper = mapper;
            _service = service;
        }
        //delete feedback
        public async Task<Response> DeleteFeedbackAsync(Request request)
        {
            //call helper method that handles delete grpc action
            return await GrpcServiceHelper.HandleDeleteActionAsync<Response>
                 (request.Id, _service.DeleteFeedbackAsync);
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
            var reply = new FeedbackListResponse();
            //get feedbacks
            var feedbacksDTO = await _service.GetFeedbacksForItemAsync(request.Id);
            if (feedbacksDTO != null)
            {
                //map dtos to grpc objects if not null
                var feedbacks = _mapper.Map<ICollection<FeedbackResponse>>(feedbacksDTO);
                //add listof objects to grpc response
                reply.Feedbacks.AddRange(feedbacks);
            }
            return reply;
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
