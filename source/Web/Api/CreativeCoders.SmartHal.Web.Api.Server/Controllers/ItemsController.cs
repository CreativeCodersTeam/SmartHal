using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreativeCoders.SmartHal.Kernel.Base.Messages.Items;
using CreativeCoders.SmartHal.Kernel.Base.Messaging;
using CreativeCoders.SmartHal.Kernel.Base.Repositories;
using CreativeCoders.SmartHal.Web.Api.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CreativeCoders.SmartHal.Web.Api.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IMessageHub _messageHub;

        private readonly IItemRepository _itemRepository;

        public ItemsController(IMessageHub messageHub, IItemRepository itemRepository)
        {
            _messageHub = messageHub;
            _itemRepository = itemRepository;
        }

        [HttpGet]
        public IEnumerable<ItemModel> Get()
        {
            return _itemRepository.Select(x =>
                new ItemModel
                {
                    Name = x.Name,
                    Value = x.Value?.ToString() ?? string.Empty,
                    ItemType = x.ItemType.Name
                });
        }

        [HttpPost("SendCommand")]
        public Task SendCommandAsync([FromBody] SendCommandModel sendCommandModel)
        {
            _messageHub.SendMessage(new SendCommandToItemMessage(sendCommandModel.ItemName, sendCommandModel.CommandValue));

            return Task.CompletedTask;
        }
    }
}
