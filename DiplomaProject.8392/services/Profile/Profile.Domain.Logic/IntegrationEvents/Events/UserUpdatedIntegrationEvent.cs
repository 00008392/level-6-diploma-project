using Profile.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBus.Events;

namespace Profile.Domain.Logic.IntegrationEvents.Events
{
    public class UserUpdatedIntegrationEvent: IntegrationEvent
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Gender? Gender { get; set; }
        public string Address { get; set; }
        public string UserInfo { get; set; }
        public long? CityId { get; set; }
    }
}
