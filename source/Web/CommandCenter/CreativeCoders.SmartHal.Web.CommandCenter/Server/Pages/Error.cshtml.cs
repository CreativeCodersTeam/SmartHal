using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Web.CommandCenter.Server.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        private readonly ILogger<ErrorModel> _logger;

        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        [UsedImplicitly]
        public void OnGet()
        {
            _logger.LogDebug("Get error page");

            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}
