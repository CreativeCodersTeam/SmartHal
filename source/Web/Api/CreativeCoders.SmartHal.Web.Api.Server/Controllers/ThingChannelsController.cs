using System.Collections.Generic;
using System.Linq;
using CreativeCoders.SmartHal.Kernel.Base.Repositories;
using CreativeCoders.SmartHal.Web.Api.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CreativeCoders.SmartHal.Web.Api.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThingChannelsController : ControllerBase
    {
        private readonly IThingChannelRepository _thingChannelRepository;

        public ThingChannelsController(IThingChannelRepository thingChannelRepository)
        {
            _thingChannelRepository = thingChannelRepository;
        }

        [HttpGet]
        public IEnumerable<ThingChannelModel> Get()
        {
            return _thingChannelRepository
                .Select(x =>
                    new ThingChannelModel
                    {
                        Id = x.Id.ToString(),
                        Name = x.Name,
                        State = x.State,
                        Value = x.Value
                    });
        }
    }
}