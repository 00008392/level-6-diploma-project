using PostFeedback.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.Contracts
{
    //service responsible for handling events published by Account and Booking microservices
    public interface IEventHandlerService
    {
        Task CreateUserAsync(UserDTO user);
        Task DeleteUserAsync(long id);
        Task UpdateUserAsync(UserDTO user);
        Task AddBookingAsync(AddBookingDTO booking);
        Task RemoveBookingAsync(long id);
    }
}
