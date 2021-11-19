using EventBus.Contracts;
using Grpc.Core;
using Post.API.Services.Strategies;
using Post.Domain.Core;
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
    public class PostItemsManipulationServiceGrpc: PostItemsManipulation.PostItemsManipulationBase
    {
        private readonly IPostRelatedInfoStrategy<AccommodationRule, Rule> _ruleStrategy;
        private readonly IPostRelatedInfoStrategy<AccommodationFacility, Facility> _facilityStrategy;
        private readonly IPostRelatedInfoStrategy<AccommodationSpecificity, Specificity> _specificityStrategy;


        public PostItemsManipulationServiceGrpc(IPostRelatedInfoStrategy<AccommodationRule, Rule> ruleStrategy,
            IPostRelatedInfoStrategy<AccommodationFacility, Facility> facilityStrategy,
            IPostRelatedInfoStrategy<AccommodationSpecificity, Specificity> specificityStrategy
            )
        {
            _ruleStrategy = ruleStrategy;
            _facilityStrategy = facilityStrategy;
            _specificityStrategy = specificityStrategy;
        }

        public override async Task<Response> AddRules(AddItemsRequest request, ServerCallContext context)
        {
           
            return await _ruleStrategy.AddItemsAsync(request);
        }
        public override async Task<Response> RemoveRules(RemoveItemsRequest request, ServerCallContext context)
        {
     
            return await _ruleStrategy.RemoveItemsAsync(request);
        }
        public override async Task<Response> AddFacilities(AddItemsRequest request, ServerCallContext context)
        {
        
            return await _facilityStrategy.AddItemsAsync(request);
        }
        public override async Task<Response> RemoveFacilities(RemoveItemsRequest request, ServerCallContext context)
        {
         
            return await _facilityStrategy.RemoveItemsAsync(request);
        }
        public override async Task<Response> AddSpecificities(AddItemsRequest request, ServerCallContext context)
        {
           
            return await _specificityStrategy.AddItemsAsync(request);
        }
        public override async Task<Response> RemoveSpecificities(RemoveItemsRequest request, ServerCallContext context)
        {
          
            return await _specificityStrategy.RemoveItemsAsync(request);
        }

      
    }
}
