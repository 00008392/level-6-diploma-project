using Post.Domain.Logic.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.DTOs
{
    public class UpdatePostDTO: BaseAccommodationDTO
    {
        public long Id { get; set; }


    }
}
