using AutoMapper;
using Post.Domain.Core;
using Post.Domain.Entities;
using Post.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Post.API.Mappings
{
    public class PostDomainMapper: Profile
    {
        public PostDomainMapper()
        {
            CreateMap<CreatePostDTO, Accommodation>()
                .ForMember(x => x.DatePublished, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(x => x.MovingInTime, opt => opt.MapFrom(
                      src => src.MovingInTime == null ? null : ((DateTime)src.MovingInTime).ToString("HH:mm")
                      ))
                .ForMember(x => x.MovingOutTime, opt => opt.MapFrom(
                      src => src.MovingOutTime == null ? null : ((DateTime)src.MovingOutTime).ToString("HH:mm")
                      ));
            CreateMap<Domain.Entities.Owner, OwnerDTO>();
            CreateMap<Domain.Entities.Category, CategoryDTO>();
            CreateMap<Domain.Entities.AccommodationPhoto, AccommodationPhotoDTO>();
            CreateMap<ItemBase, ItemInfoDTO>();
            CreateMap<ItemAccommodationBase, AccommodationItemInfoDTO>()
                .Include<AccommodationFacility, AccommodationItemInfoDTO>()
                .Include<AccommodationRule, AccommodationItemInfoDTO>()
                .Include<AccommodationSpecificity, AccommodationItemInfoDTO>()
                .ForMember(x => x.Item, opt => opt.MapFrom(
                      (src, dest, prop, context) =>
                      {
                          return context.Mapper.Map<ItemInfoDTO>(src.Item);
                      })
                );
            CreateMap<AccommodationFacility, AccommodationItemInfoDTO>();
            CreateMap<AccommodationRule, AccommodationItemInfoDTO>();
            CreateMap<AccommodationSpecificity, AccommodationItemInfoDTO>();
            CreateMap<Accommodation, AccommodationInfoDTO>()
                .ForMember(x => x.Owner, opt => opt.MapFrom(
                      (src, dest, prop, context) =>
                      {
                          return context.Mapper.Map<OwnerDTO>(src.Owner);
                      })
                )
                .ForMember(x => x.Category, opt => opt.MapFrom(
                      (src, dest, prop, context) =>
                      {
                          return src.Category == null ? null : context.Mapper.Map<CategoryDTO>(src.Category);
                      })
                )
                .ForMember(x => x.AccommodationPhotos, opt => opt.MapFrom(
                      (src, dest, prop, context) =>
                      {
                          return src.AccommodationPhotos.Any() ?
                          context.Mapper.Map<ICollection<Domain.Entities.AccommodationPhoto>, ICollection<AccommodationPhotoDTO>>(src.AccommodationPhotos)
                          : null;
                      })
                )
                .ForMember(x => x.AccommodationFacilities, opt => opt.MapFrom(
                      (src, dest, prop, context) =>
                      {
                          return src.AccommodationFacilities.Any() ?
                          context.Mapper.Map<ICollection<AccommodationFacility>, ICollection<AccommodationItemInfoDTO>>(src.AccommodationFacilities)
                          : null;
                      })
                )
                .ForMember(x => x.AccommodationRules, opt => opt.MapFrom(
                      (src, dest, prop, context) =>
                      {
                          return src.AccommodationRules.Any() ?
                          context.Mapper.Map<ICollection<AccommodationRule>, ICollection<AccommodationItemInfoDTO>>(src.AccommodationRules)
                          : null;
                      })
                )
                .ForMember(x => x.AccommodationSpecificities, opt => opt.MapFrom(
                      (src, dest, prop, context) =>
                      {
                          return src.AccommodationSpecificities.Any() ?
                          context.Mapper.Map<ICollection<AccommodationSpecificity>, ICollection<AccommodationItemInfoDTO>>(src.AccommodationSpecificities)
                          : null;
                      })
                );

            CreateMap<CreateUserDTO, Domain.Entities.Owner>()
                .ConstructUsing(x => new Domain.Entities.Owner(x.Email));
            CreateMap<UpdateUserDTO, Domain.Entities.Owner>();

        }
    }
}
