using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models
{
    public class EditPost
    {
        public long? Id { get; set; }
        public string Title { get;  set; }
        public string Description { get;  set; }
        public long OwnerId { get;  set; }
        public long? CategoryId { get;  set; }
        public Category Category { get;  set; }
        public long? CityId { get;  set; }
        public City City { get;  set; }
        public string Address { get;  set; }
        public string ReferencePoint { get;  set; }
        public string ContactNumber { get;  set; }
        public int? RoomsNo { get;  set; }
        public int? BathroomsNo { get;  set; }
        public int? BedsNo { get;  set; }
        public int MaxGuestsNo { get;  set; }
        public int? SquareMeters { get;  set; }
        public decimal Price { get;  set; }
        public decimal? Latitude { get;  set; }
        public decimal? Longitude { get;  set; }
        public bool IsWholeApartment { get;  set; }
        public string AdditionalInfo { get;  set; }
        public DateTime? MovingInTime { get;  set; }
        public DateTime? MovingOutTime { get;  set; }
        public ICollection<City> Cities { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
