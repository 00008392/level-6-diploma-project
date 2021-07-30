using Grpc.Core;
using Post.API.ExceptionHandling;
using Post.API.Services.Strategy;
using Post.Domain.Core;
using Post.Domain.Entities;
using Post.Domain.Logic.Contracts;
using Post.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Post.API.Services
{
    public class PostRelatedInfoServiceGrpc: PostRelatedItems.PostRelatedItemsBase
    {
        private readonly IPostRelatedInfoStrategy<Post.Domain.Entities.AccommodationRule, Post.Domain.Entities.Rule> _ruleStrategy;
        private readonly IPostRelatedInfoStrategy<Post.Domain.Entities.AccommodationFacility, Post.Domain.Entities.Facility> _facilityStrategy;
        private readonly IPostRelatedInfoStrategy<Post.Domain.Entities.AccommodationSpecificity, Post.Domain.Entities.Specificity> _specificityStrategy;

        public PostRelatedInfoServiceGrpc(IPostRelatedInfoStrategy<Post.Domain.Entities.AccommodationRule, Post.Domain.Entities.Rule> ruleStrategy,
            IPostRelatedInfoStrategy<Post.Domain.Entities.AccommodationFacility, Post.Domain.Entities.Facility> facilityStrategy,
            IPostRelatedInfoStrategy<Post.Domain.Entities.AccommodationSpecificity, Post.Domain.Entities.Specificity> specificityStrategy
            )
        {
            _ruleStrategy = ruleStrategy;
            _facilityStrategy = facilityStrategy;
            _specificityStrategy = specificityStrategy;
        }

        public override async Task<Response> AddRule(AddItemRequest request, ServerCallContext context)
        {
            return await _ruleStrategy.AddItemAsync(request);
        }
        public override async Task<Response> RemoveRule(Request request, ServerCallContext context)
        {
            return await _ruleStrategy.RemoveItemAsync(request);
        }
        public override async Task<Response> AddFacility(AddItemRequest request, ServerCallContext context)
        {
            return await _facilityStrategy.AddItemAsync(request);
        }
        public override async Task<Response> RemoveFacility(Request request, ServerCallContext context)
        {
            return await _facilityStrategy.RemoveItemAsync(request);
        }
        public override async Task<Response> AddSpecificity(AddItemRequest request, ServerCallContext context)
        {
            return await _specificityStrategy.AddItemAsync(request);
        }
        public override async Task<Response> RemoveSpecificity(Request request, ServerCallContext context)
        {
            return await _specificityStrategy.RemoveItemAsync(request);
        }



    }
}
