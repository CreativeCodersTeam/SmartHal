using CreativeCoders.SmartHal.Web.ControlCenter.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CreativeCoders.SmartHal.Web.ControlCenter.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientConfigController : ControllerBase
    {
        private readonly string _webApiUrl;

        public ClientConfigController(IConfiguration configuration)
        {
            _webApiUrl = configuration.GetSection("WebApi")["Url"];
        }

        [HttpGet]
        public ClientConfig Get()
        {
            return new ClientConfig {WebApiUrl = _webApiUrl};
        }
    }
}
