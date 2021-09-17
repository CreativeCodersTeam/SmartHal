using Microsoft.AspNetCore.Mvc;
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
        private readonly string _webApiUrl;

        public ClientConfigController(IConfiguration configuration)
        {
            Ensure.IsNotNull(configuration, nameof(configuration));

            _webApiUrl = configuration.GetSection("WebApi")["Url"];
        }

        [HttpGet]
        public Task<ClientConfigModel> GetAsync()
        {
            return Task.FromResult(new ClientConfigModel { WebApiUrl = _webApiUrl });
        }
    }
}
