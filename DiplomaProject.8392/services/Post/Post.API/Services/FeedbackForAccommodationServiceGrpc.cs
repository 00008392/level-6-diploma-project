using AutoMapper;
using ExceptionHandling;
using FluentValidation;
using Grpc.Core;
using Post.API.Services.Strategies;
using Post.Domain.Entities;
using Post.Domain.Logic.Contracts;
using Post.Domain.Logic.DTOs;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Post.API.Services
{
    public class FeedbackForAccommodationServiceGrpc: FeedbackForAccommodation.FeedbackForAccommodationBase
    {
        private readonly IFeedbackStrategy<Accommodation, AccommodationInfoDTO> _strategy;

        public FeedbackForAccommodationServiceGrpc(IFeedbackStrategy<Accommodation, AccommodationInfoDTO> strategy)
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
