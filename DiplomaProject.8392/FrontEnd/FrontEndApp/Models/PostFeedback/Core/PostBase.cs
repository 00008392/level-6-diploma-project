using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models
{
    public class PostBase: ErrorViewModel
    {
        public long? Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public long OwnerId { get; set; }
        public long? CategoryId { get; set; }
        public long? CityId { get; set; }
        public UserInfo Owner { get; set; }
        public CategoryCity Category { get; set; }
        public CategoryCity City { get; set; }
        [Required]
        public string Address { get; set; }
        public string ReferencePoint { get; set; }
        [Required]
        public string ContactNumber { get; set; }
        public int? RoomsNo { get; set; }
        public int? BathroomsNo { get; set; }
        public int? BedsNo { get; set; }
        [Required]
        public int MaxGuestsNo { get; set; }
        public int? SquareMeters { get; set; }
        [Required]
        public decimal Price { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public bool IsWholeApartment { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
