using FrontEndApp.Services.Post.Contracts;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Components
{
    //component that all photo components inherit from
    public class PhotoBaseComponent: CustomBaseComponent
    {
        [Inject]
        protected IPhotoService _service { get; set; }
    }
}
