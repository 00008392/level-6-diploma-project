using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Grpc.Core;
using PostFeedback.API;
using PostFeedback.API.Services.Strategies;
using PostFeedback.Domain.Entities;
using PostFeedback.Domain.Logic.Contracts;
using PostFeedback.Domain.Logic.DTOs;
using PostFeedback.Domain.Logic.Services;
using Protos.Common;

namespace PostFeedback.API.Services
{
    //grpc service for user feedback manipulations and retrieval
    public class FeedbackForUserServiceGrpc : FeedbackForUser.FeedbackForUserBase
    {
        //inject generic strategy instead of domain logic service to reduce code duplication
        private readonly IFeedbackStrategy<Domain.Entities.User, UserDTO> _strategy;

        public FeedbackForUserServiceGrpc(IFeedbackStrategy<Domain.Entities.User, UserDTO> strategy)
        {
            _strategy = strategy;
        }
        //create new feedback
        public override async Task<Response> LeaveFeedback(CreateFeedbackRequest request,
            ServerCallContext context)
        {
            return await _strategy.LeaveFeedbackAsync(request);
        }
        //delete feedback
        public override async Task<Response> DeleteFeedback(Request request,
           ServerCallContext context)
        {
            return await _strategy.DeleteFeedbackAsync(request);
        }
        //retrieve feedback by id
        public override async Task<FeedbackResponse> GetFeedbackDetails(Request request,
          ServerCallContext context)
        {
            return await _strategy.GetFeedbackDetailsAsync(request);
        }
        //retrieve feedbacks by id of user on which feedback is left
        public override async Task<FeedbackListResponse> GetFeedbacksForItem(Request request,
         ServerCallContext context)
        {
            return await _strategy.GetFeedbacksForItemAsync(request);
        }
    }
}
