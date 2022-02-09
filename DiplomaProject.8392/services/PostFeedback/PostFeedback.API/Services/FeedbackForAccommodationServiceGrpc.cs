using AutoMapper;
using API.ExceptionHandling;
using FluentValidation;
using Grpc.Core;
using PostFeedback.API.Services.Strategies;
using PostFeedback.Domain.Entities;
using PostFeedback.Domain.Logic.Contracts;
using PostFeedback.Domain.Logic.DTOs;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostFeedback.API.Services
{
    public class FeedbackForAccommodationServiceGrpc : FeedbackForAccommodation.FeedbackForAccommodationBase
    {
        private readonly IFeedbackStrategy<Post, PostDetailsDTO> _strategy;

        public FeedbackForAccommodationServiceGrpc(IFeedbackStrategy<Post, PostDetailsDTO> strategy)
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
        public override async Task<FeedbackResponse> GetFeedbackDetails(Request request,
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
