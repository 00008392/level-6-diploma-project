using BaseClasses.Contracts;
using Booking.Domain.Entities;
using Booking.Domain.Logic.Contracts;
using Booking.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.Services
{
    public class BookingInfoService : IBookingInfoService
    {
        private readonly IRepository<BookingRequest> _repository;

        public BookingInfoService(IRepository<BookingRequest> repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<BookingRequestInfoDTO>> GetBookingRequestsForAccommodation(long id)
        {
            return await GetRequests(r => r.AccommodationId == id);
        }

        public async Task<ICollection<BookingRequestInfoDTO>> GetBookingRequestsForUser(long id)
        {
            return await GetRequests(r => r.GuestId == id);
        }
        private async Task<ICollection<BookingRequestInfoDTO>> GetRequests(
            Expression<Func<BookingRequest, bool>> filter)
        {
            var requestsList = (await _repository.GetFilteredAsync(filter)).ToList();
            var requestsDTOList = new List<BookingRequestInfoDTO>();
            requestsList.ForEach(item => requestsDTOList.Add(new BookingRequestInfoDTO(new UserDTO(item.Guest.FirstName,
                item.Guest.LastName, item.Guest.Email, item.Guest.PhoneNumber,
                item.Guest.Address, item.Guest.DateOfBirth),
                new BaseAccommodationDTO(item.Accommodation.Title, item.Accommodation.OwnerId,
                item.Accommodation.Address, item.Accommodation.ContactNumber,
                item.Accommodation.RoomsNo, item.Accommodation.BathroomsNo,
                item.Accommodation.BedsNo, item.Accommodation.MaxGuestsNo,
                item.Accommodation.SquareMeters, item.Accommodation.Price,
                item.Accommodation.IsWholeApartment,
                item.Accommodation.MovingInTime,
                item.Accommodation.MovingOutTime),
                item.StartDate, item.EndDate, item.IsAccepted,
                item.IsCancelled)));
            return requestsDTOList;
        }
    }
}
