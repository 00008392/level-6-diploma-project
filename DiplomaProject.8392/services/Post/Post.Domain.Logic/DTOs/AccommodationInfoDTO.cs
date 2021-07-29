using Post.Domain.Core;
using Post.Domain.Entities;
using Post.Domain.Logic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.DTOs
{
   public class AccommodationInfoDTO: BaseAccommodationDTO
    {
        public long Id { get; set; }
        public Owner Owner { get; set; }
        public DateTime DatePublished { get; set; }
        public Category Category { get; set; }
        public ICollection<AccommodationPhoto> AccommodationPhotos { get; set; }
        public ICollection<AccommodationSpecificity> AccommodationSpecificities { get; set; }
        public ICollection<AccommodationRule> AccommodationRules { get; set; }
        public ICollection<AccommodationFacility> AccommodationFacilities { get; set; }
    }
}
