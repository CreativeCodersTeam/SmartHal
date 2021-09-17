﻿using System.Collections.Generic;
using System.Linq;
using CreativeCoders.SmartHal.Kernel.Base.Repositories;
using CreativeCoders.SmartHal.Web.Api.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CreativeCoders.SmartHal.Web.Api.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GatewaysController : ControllerBase
    {
        private readonly IGatewayRepository _gatewayRepository;

        public GatewaysController(IGatewayRepository gatewayRepository)
        {
            _gatewayRepository = gatewayRepository;
        }

        [HttpGet]
        public IEnumerable<GatewayModel> Get()
        {
            return _gatewayRepository
                .Select(x =>
                    new GatewayModel
                    {
                        Id = x.Id.ToString(),
                        Name = x.Name,
                        State = x.State,
                        StateText = x.State.ToString()
                    });
        }
    }
}