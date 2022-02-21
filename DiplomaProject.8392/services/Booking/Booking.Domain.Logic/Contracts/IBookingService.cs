using Booking.Domain.Entities;
using Booking.Domain.Logic.DTOs;
using Booking.Domain.Logic.Specifications;
using Domain.Logic.Base.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.Contracts
{
    //service for booking manipulation
    public interface IBookingService
    {
        //create booking request on accommodation
        Task CreateBookingAsync(CreateBookingDTO requestDTO);
        //delete booking
        //can be deleted by the user who requested it until not accepted or if rejected/cancelled
        Task DeleteBookingAsync(long id);
        //update booking status
        //can be accepted/rejected by accommodation owner, can be cancelled by both guest and owner
        Task HandleBookingStatusAsync(UpdateBookingStatusDTO bookingStatus);
        //get bookings either by guest or by post
        //guest can view bookings created by him/her and owner can view bookings made on his/her accommodations
        Task<ICollection<BookingInfoDTO>> GetBookingsAsync(Specification<Entities.Booking> specification);
        //get booking details by id
        Task<BookingInfoDTO> GetBookingDetailsAsync(long id);
    }
}
