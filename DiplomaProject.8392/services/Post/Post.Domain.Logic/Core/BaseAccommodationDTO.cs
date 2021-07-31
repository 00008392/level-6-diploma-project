using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.Core
{
   public abstract class BaseAccommodationDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public long OwnerId { get; set; }
        public long? CategoryId { get; set; }
        public string Address { get; set; }
        public string ReferencePoint { get; set; }
        public string ContactNumber { get; set; }
        public int? RoomsNo { get; set; }
        public int? BathroomsNo { get; set; }
        public int? BedsNo { get; set; }
        public int MaxGuestsNo { get; set; }
        public int? SquareMeters { get; set; }
        public decimal Price { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public bool? IsWholeApartment { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
