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
        Task CreateBookingRequestAsync(CreateBookingRequestDTO requestDTO);
        //can be deleted by the user who requested it until not accepted or if rejected
        Task DeleteBookingRequestAsync(long id);
        Task AcceptBookingRequestAsync(long id);
        Task RejectBookingRequestAsync(long id);
        //can be cancelled by both owner and guest
        Task CancelBookingAsync(long id);
    }
}
