using EventBus.Contracts;
using Grpc.Core;
using Post.API.Services.Strategies;
using Post.Domain.Core;
using Post.Domain.Entities;
using Post.Domain.Logic.Contracts;
using Post.Domain.Logic.DTOs;
using Post.Domain.Logic.IntegrationEvents.Events;
using Post.Domain.Logic.IntegrationEvents.Events.Core;
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

        public override async Task<Response> AddRules(AddItemsRequest request, ServerCallContext context)
        {
            var @event = GetEvent<AccommodationRuleAddedIntegrationEvent>(request);
            return await _ruleStrategy.AddItemsAsync(request, @event);
        }
        public override async Task<Response> RemoveRules(RemoveItemsRequest request, ServerCallContext context)
        {
            var @event = new AccommodationRuleRemovedIntegrationEvent(request.Ids.ToList());
            return await _ruleStrategy.RemoveItemsAsync(request, @event);
        }
        public override async Task<Response> AddFacilities(AddItemsRequest request, ServerCallContext context)
        {
            var @event = GetEvent<AccommodationFacilityAddedIntegrationEvent>(request);
            return await _facilityStrategy.AddItemsAsync(request, @event);
        }
        public override async Task<Response> RemoveFacilities(RemoveItemsRequest request, ServerCallContext context)
        {
            var @event = new AccommodationFacilityRemovedIntegrationEvent(request.Ids.ToList());
            return await _facilityStrategy.RemoveItemsAsync(request, @event);
        }
        public override async Task<Response> AddSpecificities(AddItemsRequest request, ServerCallContext context)
        {
            var @event = GetEvent<AccommodationSpecificityAddedIntegrationEvent>(request);
            return await _specificityStrategy.AddItemsAsync(request, @event);
        }
        public override async Task<Response> RemoveSpecificities(RemoveItemsRequest request, ServerCallContext context)
        {
            var @event = new AccommodationSpecificityRemovedIntegrationEvent(request.Ids.ToList());
            return await _specificityStrategy.RemoveItemsAsync(request, @event);
        }

        private ICollection<AccommodationItemEventBase> ConvertToEventListFromCollection(AddItemsRequest request)
        {
            var eventList = new List<AccommodationItemEventBase>();
            request.Items.ToList().ForEach(item => eventList.Add(new AccommodationItemEventBase(item.AccommodationId,
                item.ItemId, item.OtherValue)));
            return eventList;
        }
        private E GetEvent<E>(AddItemsRequest request)
            where E: AccommodationItemAddedIntegrationEvent
        {
            var itemsList = ConvertToEventListFromCollection(request);
            return (E)Activator.CreateInstance(typeof(E), itemsList);
        }
    }
}
