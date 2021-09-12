using Booking.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.Contracts
{
    public interface IBookingRequestManipulationService
    {
        Task CreateBookingRequest(CreateBookingRequestDTO requestDTO);
        //can be deleted by the user who requested it until not accepted or if rejected
        Task DeleteBookingRequest(long id);
        Task AcceptBookingRequest(long id);
        Task RejectBookingRequest(long id);
        //can be cancelled by both owner and guest
        Task CancelBooking(long id);
    }
}
