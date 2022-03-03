using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models.Post.Core
{
    public class PostBase
    {
        public long Id { get; set; }
        [MaxLength(50)]
        [Required(ErrorMessage ="Title is required")]
        public string Title { get; set; }
        public string Description { get; set; }
        public long? CategoryId { get; set; }
        [Required(ErrorMessage = "City is required")]
        public long? CityId { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Contact number is required")]
        public string ContactNumber { get; set; }
        [Required(ErrorMessage = "Rooms number is required")]
        [Range(1,20, ErrorMessage="Rooms limit is 20")]
        public int? RoomsNo { get; set; }
        [Range(0, 20, ErrorMessage = "Bathrooms limit is 20")]
        public int? BathroomsNo { get; set; }
        [Required(ErrorMessage = "Beds number is required")]
        [Range(1, 20, ErrorMessage = "Beds limit is 20")]
        public int? BedsNo { get; set; }
        [Required(ErrorMessage = "Maximum number of guests is required")]
        [Range(1, 20, ErrorMessage = "Guest limit is 20")]
        public int? MaxGuestsNo { get; set; }
        public int? SquareMeters { get; set; }
        [Required(ErrorMessage = "Price is required")]
        [Range(0, 5000, ErrorMessage = "Price limit is 5000$")]
        public double? Price { get; set; }
        public bool IsWholeApartment { get; set; }
    }
}
