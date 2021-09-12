using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using CreativeCoders.Core;
using CreativeCoders.SmartHal.Web.CommandCenter.Shared;
using Microsoft.Extensions.Configuration;

namespace CreativeCoders.SmartHal.Web.CommandCenter.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientConfigController : ControllerBase
    {
        private readonly ILogger<ClientConfigController> _logger;

        private readonly string _webApiUrl;

        public ClientConfigController(ILogger<ClientConfigController> logger, IConfiguration configuration)
        {
            Ensure.IsNotNull(logger, nameof(logger));
            Ensure.IsNotNull(configuration, nameof(configuration));

            _logger = logger;

            _webApiUrl = configuration.GetSection("WebApi")["Url"];
        }

        [HttpGet]
        public Task<ClientConfigModel> GetAsync()
        {
            return Task.FromResult(new ClientConfigModel { WebApiUrl = _webApiUrl });
        }
    }
}
