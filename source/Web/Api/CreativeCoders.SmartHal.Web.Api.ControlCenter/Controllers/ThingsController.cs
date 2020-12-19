using System.Collections.Generic;
using System.Linq;
using CreativeCoders.SmartHal.Kernel.Base.Repositories;
using CreativeCoders.SmartHal.Web.Api.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CreativeCoders.SmartHal.Web.Api.ControlCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThingsController
    {
        private readonly IThingRepository _thingRepository;

        public ThingsController(IThingRepository thingRepository)
        {
            _thingRepository = thingRepository;
        }

        [HttpGet]
        public IEnumerable<ThingModel> Get()
        {
            return _thingRepository
                .Select(x =>
                    new ThingModel
                    {
                        Id = x.Id.ToString(),
                        Name = x.Name,
                        State = x.State,
                        StateText = x.State.ToString()
                    });
        }
    }
}