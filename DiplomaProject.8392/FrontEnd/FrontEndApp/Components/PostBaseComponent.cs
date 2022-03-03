using FrontEndApp.Services.Post.Contracts;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Components
{
    public class PostBaseComponent: CustomBaseComponent
    {
        [Inject]
        protected IPostService _service { get; set; }
    }
}
