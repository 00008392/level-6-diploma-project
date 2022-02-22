using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.DTOs
{
    //dto for holding photo of accommodation
    public class PhotoDTO
    {
        public long Id { get; private set; }
        public byte[] Photo { get; private set; }
        //to create photo
        public PhotoDTO(
            byte[] photo)
        {
            Photo = photo;
        }
        //to retrieve photo
        public PhotoDTO(
            long id,
            byte[] photo)
        {
            Id = id;
            Photo = photo;
        }
    }
}
