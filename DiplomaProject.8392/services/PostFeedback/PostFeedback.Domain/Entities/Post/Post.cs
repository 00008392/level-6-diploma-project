using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Entities
{
    //Post entity contains information about accommodation that is posted for rent
    public class Post: FeedbackEntity
    {
        public string Title { get; private set; } 
        public string Description { get; private set; }
        public long OwnerId { get; private set; } 
        public User Owner { get; private set; }
        public DateTime DatePublished { get; private set; } 
        public long? CategoryId { get; private set; } 
        public Category Category { get; private set; }
        public string Address { get; private set; }
        public long CityId { get; private set; } 
        public City City { get; private set; }
        public string ContactNumber { get; private set; }
        public int RoomsNo { get; private set; }
        public int? BathroomsNo { get; private set; }
        public int BedsNo { get; private set; } 
        public int MaxGuestsNo { get; private set; } 
        public int? SquareMeters { get; private set; } 
        public decimal Price { get; private set; } 
        public bool IsWholeApartment { get; private set; } 
        //it is more convenient to retrieve moving in/out time in string format
        //in database, it is stored as time format
        public string MovingInTime { get; private set; }
        public string MovingOutTime { get; private set; }
        public ICollection<Photo> Photos { get; private set; }
        public ICollection<Rule> Rules { get; private set; }
        public ICollection<Facility> Facilities { get; private set; }
        public ICollection<Booking> Bookings { get; private set; }
        public ICollection<Feedback<Post>> Feedbacks { get; private set; }
        
        public Post(
          long id,
          string title,
          string description,
          long ownerId,
          DateTime datePublished,
          long? categoryId,
          long cityId,
          string address,
          string contactNumber,
          int roomsNo,
          int? bathroomsNo,
          int bedsNo,
          int maxGuestsNo,
          int? squareMeters,
          decimal price,
          bool isWholeApartment,
          string movingInTime,
          string movingOutTime)
        {
            Id = id;
            Title = title;
            Description = description;
            OwnerId = ownerId;
            DatePublished = datePublished;
            CategoryId = categoryId;
            CityId = cityId;
            Address = address;
            ContactNumber = contactNumber;
            RoomsNo = roomsNo;
            BathroomsNo = bathroomsNo;
            BedsNo = bedsNo;
            MaxGuestsNo = maxGuestsNo;
            SquareMeters = squareMeters;
            Price = price;
            IsWholeApartment = isWholeApartment;
            MovingInTime = movingInTime;
            MovingOutTime = movingOutTime;
            Rules = new List<Rule>();
            Facilities = new List<Facility>();
        }
        //method that is called when post information is updated
        public void UpdateInfo(string title,
                               string description,
                               long ownerId,
                               long? categoryId,
                               long cityId,
                               string address,
                               string contactNumber,
                               int roomsNo,
                               int? bathroomsNo,
                               int bedsNo,
                               int maxGuestsNo,
                               int? squareMeters,
                               decimal price,
                               bool isWholeApartment,
                               string movingInTime,
                               string movingOutTime)
        {
            Title = title;
            Description = description;
            OwnerId = ownerId;
            CategoryId = categoryId;
            CityId = cityId;
            Address = address;
            ContactNumber = contactNumber;
            RoomsNo = roomsNo;
            BathroomsNo = bathroomsNo;
            BedsNo = bedsNo;
            MaxGuestsNo = maxGuestsNo;
            SquareMeters = squareMeters;
            Price = price;
            IsWholeApartment = isWholeApartment;
            MovingInTime = movingInTime;
            MovingOutTime = movingOutTime;
        }
        //this method attaches rules to post
        public void SetRules(ICollection<Rule> rules)
        {
            Rules = rules;
        }
        //this method attaches facilities to post
        public void SetFacilities(ICollection<Facility> facilities)
        {
            Facilities = facilities;
        }
    }


}
