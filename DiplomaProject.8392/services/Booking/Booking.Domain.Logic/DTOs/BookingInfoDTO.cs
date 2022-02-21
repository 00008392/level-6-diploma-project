using Booking.Domain.Entities;
using Booking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.DTOs
{
    //dto that holds information about booking
    public class BookingInfoDTO
    {
        public long Id { get;private set; }
        public long GuestId { get; private set; }
        public PostDTO Post { get; private set; }
        public int GuestNo { get;private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public Status Status { get; private set; }

        public BookingInfoDTO(
            long id,
            long guestId,
            PostDTO post,
            int guestNo,
            DateTime startDate,
            DateTime endDate,
            Status status)
        {
            Id = id;
            GuestId = guestId;
            Post = post;
            GuestNo = guestNo;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
        }
    }
}
