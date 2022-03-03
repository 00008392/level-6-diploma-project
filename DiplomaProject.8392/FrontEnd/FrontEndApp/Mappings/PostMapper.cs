using AutoMapper;
using FrontEndApp.Models.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Mappings
{
    public class PostMapper : Profile
    {
        public PostMapper()
        {
            CreateMap<PostResponse, EditPost>()
                .ForMember(x => x.MovingInTime, opt => opt.MapFrom(src =>
                    ParseStringToDateTime(src.MovingInTime)))
                .ForMember(x => x.MovingOutTime, opt => opt.MapFrom(src =>
                    ParseStringToDateTime(src.MovingOutTime)))
                .ForMember(x => x.Rules, opt => opt.MapFrom(src =>
                    src.Rules.Select(x => x.Id)))
                .ForMember(x => x.Facilities, opt => opt.MapFrom(src =>
                    src.Facilities.Select(x => x.Id)));
        }
        private DateTime ParseStringToDateTime(string time)
        {
            return DateTime.ParseExact
                (time, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
