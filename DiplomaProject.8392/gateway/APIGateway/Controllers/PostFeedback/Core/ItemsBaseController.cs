using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Post.API;
using Protos.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIGateway.Controllers.PostFeedback.Core
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ItemsBaseController : ControllerBase
    {
        protected readonly PostItemsInfo.PostItemsInfoClient _itemsInfoClient;
        protected readonly PostItemsManipulation.PostItemsManipulationClient _itemsManipulationClient;

        public ItemsBaseController(PostItemsInfo.PostItemsInfoClient itemsInfoClient,
            PostItemsManipulation.PostItemsManipulationClient itemsmanipulationClient)
        {
            _itemsInfoClient = itemsInfoClient;
            _itemsManipulationClient = itemsmanipulationClient;
        }

        public abstract Task<IActionResult> GetItems();
        public abstract Task<IActionResult> PostItems(AddItemsRequest request);
        public abstract Task<IActionResult> DeleteItems(RemoveItemsRequest request);


    }
}
