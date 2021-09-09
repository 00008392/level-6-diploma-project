using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.IntegrationEvents.Events.Core
{
   public abstract class AccommodationBaseIntegrationEvent: IntegrationEvent
    {
        public string Title { get; protected set; }
        public string Description { get; protected set; }
        public long OwnerId { get; protected set; }
        public long? CategoryId { get; protected set; }
        public string Address { get; protected set; }
        public string ReferencePoint { get; protected set; }
        public string ContactNumber { get; protected set; }
        public int? RoomsNo { get; protected set; }
        public int? BathroomsNo { get; protected set; }
        public int? BedsNo { get; protected set; }
        public int MaxGuestsNo { get; protected set; }
        public int? SquareMeters { get; protected set; }
        public decimal Price { get; protected set; }
        public decimal? Latitude { get; protected set; }
        public decimal? Longitude { get; protected set; }
        public bool? IsWholeApartment { get; protected set; }
        public string MovingInTime { get; protected set; }
        public string MovingOutTime { get; protected set; }
        public string AdditionalInfo { get; protected set; }

       
    }
}
