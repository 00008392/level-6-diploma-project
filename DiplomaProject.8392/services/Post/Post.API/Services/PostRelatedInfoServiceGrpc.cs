using Grpc.Core;
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
        //private readonly IPostRelatedInfoService<AccommodationRuleDTO, Post.Domain.Entities.AccommodationRule, Post.Domain.Entities.Rule> _ruleService;
        //private readonly IPostRelatedInfoService<AccommodationFacilityDTO, Post.Domain.Entities.AccommodationFacility, Post.Domain.Entities.Facility> _facilityService;
        //private readonly IPostRelatedInfoService<AccommodationSpecificityDTO, Post.Domain.Entities.AccommodationSpecificity, Post.Domain.Entities.Specificity> _specificityService;

        //public PostRelatedInfoServiceGrpc(IPostRelatedInfoService<AccommodationRuleDTO, Post.Domain.Entities.AccommodationRule, Post.Domain.Entities.Rule> ruleService,
        //    IPostRelatedInfoService<AccommodationFacilityDTO, Post.Domain.Entities.AccommodationFacility, Post.Domain.Entities.Facility> facilityService,
        //    IPostRelatedInfoService<AccommodationSpecificityDTO, Post.Domain.Entities.AccommodationSpecificity, Post.Domain.Entities.Specificity> specificityService
        //    )
        //{
        //    _ruleService = ruleService;
        //    _facilityService = facilityService;
        //    _specificityService = specificityService;
        //}

        //public override async Task<Response> AddRule(AddItemRequest request, ServerCallContext context)
        //{

        //}
        //public override async Task<Response> RemoveRule(Request request, ServerCallContext context)
        //{

        //}
        //public override async Task<Response> AddFacility(AddItemRequest request, ServerCallContext context)
        //{

        //}
        //public override async Task<Response> RemoveFacility(Request request, ServerCallContext context)
        //{

        //}
        //public override async Task<Response> AddSpecificity(AddItemRequest request, ServerCallContext context)
        //{

        //}
        //public override async Task<Response> RemoveSpecificity(Request request, ServerCallContext context)
        //{

        //}
    }
}
