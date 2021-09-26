using AutoMapper;
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
        private readonly IMapper _mapper;

        public BookingInfoService(IRepository<BookingRequest> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ICollection<BookingRequestInfoDTO>> GetBookingRequestsForAccommodationAsync(long id)
        {
            return await GetRequestsAsync(r => r.AccommodationId == id, true, false);
        }

        public async Task<ICollection<BookingRequestInfoDTO>> GetBookingRequestsForUserAsync(long id)
        {
            return await GetRequestsAsync(r => r.GuestId == id, false, true);
        }
        private async Task<ICollection<BookingRequestInfoDTO>> GetRequestsAsync(
            Expression<Func<BookingRequest, bool>> filter, bool includeUser, bool includeAccommodaiton)
        {
            var requestsList = (await _repository.GetFilteredAsync(filter)).ToList();
            var requestsDTOList = new List<BookingRequestInfoDTO>();
            requestsList.ForEach(item =>
            {
                var itemDTO = _mapper.Map<BookingRequestInfoDTO>(item);
                if (includeUser)
                {
                    itemDTO.LoadUser(_mapper.Map<UserDTO>(item.Guest));
                }
                if (includeAccommodaiton)
                {
                    itemDTO.LoadAccommodation(_mapper.Map<AccommodationDTO>(item.Accommodation));
                }
                requestsDTOList.Add(itemDTO);
            });
            return requestsDTOList;
        }
    }
}
