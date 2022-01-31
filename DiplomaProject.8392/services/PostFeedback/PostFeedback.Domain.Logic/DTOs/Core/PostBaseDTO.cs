using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.DTOs
{
   public abstract class PostBaseDTO
    {
        public string Title { get; protected set; }
        public string Description { get; protected set; }
        public string Address { get; protected set; }
        public string ReferencePoint { get; protected set; }
        public string ContactNumber { get; protected set; }
        public int? RoomsNo { get; protected set; }
        public int? BathroomsNo { get; protected set; }
        public int? BedsNo { get; protected set; }
        public int MaxGuestsNo { get; protected set; }
        public int? SquareMeters { get; protected set; }
        public decimal Price { get; protected set; }
        public bool? IsWholeApartment { get; protected set; }
        public string AdditionalInfo { get; protected set; }

        protected PostBaseDTO(string title, string description,
            string address, string referencePoint,
            string contactNumber, int? roomsNo, 
            int? bathroomsNo, int? bedsNo,
            int maxGuestsNo, int? squareMeters, 
            decimal price, 
            bool? isWholeApartment,
            string additionalInfo)
        {
            Title = title;
            Description = description;
            Address = address;
            ReferencePoint = referencePoint;
            ContactNumber = contactNumber;
            RoomsNo = roomsNo;
            BathroomsNo = bathroomsNo;
            BedsNo = bedsNo;
            MaxGuestsNo = maxGuestsNo;
            SquareMeters = squareMeters;
            Price = price;
            IsWholeApartment = isWholeApartment;
            AdditionalInfo = additionalInfo;
        }
    }
}
