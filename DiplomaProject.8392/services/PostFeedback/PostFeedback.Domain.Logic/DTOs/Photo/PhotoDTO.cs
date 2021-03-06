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
        public long PostId { get; private set; }
        public byte[] Photo { get; private set; }
        //to create photo
        public PhotoDTO(
            byte[] photo,
            long postId)
        {
            Photo = photo;
            PostId = postId;
        }
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

        public PhotoDTO(
            long id,
            long postId,
            byte[] photo)
        {
            Id = id;
            PostId = postId;
            Photo = photo;
        }
    }
}
