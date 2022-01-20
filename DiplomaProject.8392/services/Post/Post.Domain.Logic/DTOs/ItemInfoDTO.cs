using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.DTOs
{
   public class ItemInfoDTO
    {
        public long Id { get;private set; }
        public string Name { get;private set; }
        public bool IsOther { get;private set; }

        public ItemInfoDTO(long id, string name, bool? isOther )
        {
            Id = id;
            Name = name;
            IsOther = isOther ?? false;
        }
    }
}
