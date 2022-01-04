using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ExceptionHandling;
using FluentValidation;
using Grpc.Core;
using Post.API;
using Post.API.Services.Strategies;
using Post.Domain.Core;
using Post.Domain.Entities;
using Post.Domain.Logic.Contracts;
using Post.Domain.Logic.DTOs;
using Post.Domain.Logic.Services;
using Protos.Common;

namespace Post.API.Services
{
    public class FeedbackForUserServiceGrpc: FeedbackForUser.FeedbackForUserBase
    {
        private readonly IFeedbackStrategy<Domain.Entities.User, UserDTO> _strategy;

        public FeedbackForUserServiceGrpc(IFeedbackStrategy<Domain.Entities.User, UserDTO> strategy)
        {
            _strategy = strategy;
        }

        public override async Task<Response> LeaveFeedback(CreateFeedbackRequest request,
            ServerCallContext context)
        {
            return await _strategy.LeaveFeedbackAsync(request);
        }
        public override async Task<Response> DeleteFeedback(Request request,
           ServerCallContext context)
        {
            return await _strategy.DeleteFeedbackAsync(request);
        }
        public override async Task<FeedbackInfoResponse> GetFeedbackDetails(Request request,
          ServerCallContext context)
        {
            return await _strategy.GetFeedbackDetailsAsync(request);
        }
        public override async Task<FeedbacksListResponse> GetFeedbacks(Request request,
         ServerCallContext context)
        {
            return await _strategy.GetFeedbacksAsync(request);
        }
    }
}
