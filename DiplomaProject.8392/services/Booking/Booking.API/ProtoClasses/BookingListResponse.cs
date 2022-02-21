using Grpc.Base.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.API
{
    //need to implement interface for method that maps collection of dtos to grpc generated objects and adds it to grpc reponse
    public partial class BookingListResponse: IItemList<BookingInfoResponse>
    {
    }
}
