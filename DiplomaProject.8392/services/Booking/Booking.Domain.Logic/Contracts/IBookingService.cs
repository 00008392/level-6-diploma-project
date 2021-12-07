using Booking.Domain.Entities;
using Booking.Domain.Logic.DTOs;
using Booking.Domain.Logic.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.Contracts
{
    public interface IBookingService
    {
        Task CreateBookingRequestAsync(CreateBookingRequestDTO requestDTO);
        //can be deleted by the user who requested it until not accepted or if rejected
        Task DeleteBookingRequestAsync(long id);
        Task HandleRequestStatusAsync(BookingStatusDTO bookingStatus);
        Task HandleCoTravelerAsync(CoTravelerDTO coTraveler);
        Task<ICollection<BookingRequestInfoDTO>> GetBookingsAsync(BookingRequestSpecification specification);
        Task<BookingRequestInfoDTO> GetBookingDetailsAsync(long id);
    }
}
