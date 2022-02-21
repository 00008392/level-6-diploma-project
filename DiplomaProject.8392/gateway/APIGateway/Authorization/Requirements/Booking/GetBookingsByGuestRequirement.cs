using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGateway.Authorization.Requirements.Booking
{
    //necessary for implementation of resource-based authorization
    //applies to action that retrieves bookings by guest id
    public class GetBookingsByGuestRequirement: IAuthorizationRequirement
    {
    }
}
