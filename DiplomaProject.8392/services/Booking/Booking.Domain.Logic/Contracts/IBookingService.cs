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
        Task HandleRequestStatusAsync(long id, BookingRequestSpecification specification, Enums.Status status);
        Task ManipulateCoTravelerAsync(CoTravelerDTO coTraveler, bool remove=false);
        Task<ICollection<BookingRequestInfoDTO>> GetBookings(BookingRequestSpecification specification);
    }
}
