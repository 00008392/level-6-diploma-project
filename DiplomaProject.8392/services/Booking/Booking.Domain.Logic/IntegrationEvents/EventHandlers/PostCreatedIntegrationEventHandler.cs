using AutoMapper;
using Booking.Domain.Entities;
using Booking.Domain.Logic.Contracts;
using Booking.Domain.Logic.DTOs;
using Booking.Domain.Logic.IntegrationEvents.EventHandlers.Core;
using Booking.Domain.Logic.IntegrationEvents.Events;
using EventBus.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Booking.Domain.Logic.IntegrationEvents.EventHandlers
{
    //tested
    public class PostCreatedIntegrationEventHandler : BaseIntegrationEventHandler<Accommodation>,
        IIntegrationEventHandler<PostCreatedIntegrationEvent>
    {
        private readonly IMapper _mapper;
        public PostCreatedIntegrationEventHandler(IEventHandlerService<Accommodation> service,
            IMapper mapper)
            : base(service)
        {
            _mapper = mapper;
        }

        public async Task Handle(PostCreatedIntegrationEvent @event)
        {
            var accommodationDTO = _mapper.Map<AccommodationDTO>(@event);
            await _service.CreateEntityAsync(accommodationDTO);
        }
    }
}
