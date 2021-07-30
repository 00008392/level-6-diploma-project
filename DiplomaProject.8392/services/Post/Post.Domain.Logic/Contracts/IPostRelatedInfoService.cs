﻿using Post.Domain.Core;
using Post.Domain.Logic.Core;
using Post.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.Contracts
{
    //this service is for manipulation of entitites with relation many to many to accommodation table
    //for rules/specificities/facilities
    //AccommodationItemDTO - rule/facility/specificity DTO
    //ItemAccommodationBase - entity representing bridge table
    //ItemBase - rule/facility/specificity
    public interface IPostRelatedInfoService<T, E> 
                                                  where T: ItemAccommodationBase, new()
                                                  where E: ItemBase
    {
        Task AddItemAsync(AccommodationItemDTO itemDTO);
        Task RemoveItemAsync(long id);
    }
}
