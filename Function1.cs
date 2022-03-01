using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Linq;

namespace FunctionAppRBAC
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log, ClaimsPrincipal principal)
        {
            var roles = principal.Claims.Where(e => e.Type == "roles").Select(e => e.Value);

            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = principal.Identity.Name;

            string responseMessage = $"Hello, {name}. You have {roles.Count()} roles: {string.Join(", ", roles)}";

            return new OkObjectResult(responseMessage);
        }
    }
}
