
using Booking.Domain.Enums;
using DAL.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    //booking domain entity
    //booking on accommodation indicated in post
   public class Booking: BaseEntity
    {
        //guest - person who booked accommodation
        public long GuestId { get; private set; }
        public User Guest { get; private set; }
        //post - post with accommodation which is booked
        public long PostId { get;private set; }
        public Post Post { get; private set; }
        //number of guests that will live in the accommodation
        public int GuestNo { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        //when person books accommodation, it should be accepted by owner
        //before acceptance, status is Pending, then booking can be either accepted or rejected
        //after acceptance, booking can be cancelled
        public Status Status { get; private set; }
        //this constructor is called when booking is created
        public Booking(
            long guestId,
            long postId,
            int guestNo,
            DateTime startDate,
            DateTime endDate)
        {
            GuestId = guestId;
            PostId = postId;
            GuestNo = guestNo;
            StartDate = startDate;
            EndDate = endDate;
            //status is pending by default
            Status = Status.Pending;
        }
        //this methods updates status of booking
        public void SetStatus(Status status)
        {
            Status = status;
        }
    }
}
