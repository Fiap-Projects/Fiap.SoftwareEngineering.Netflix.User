using Fiap.SoftwareEngineering.Netflix.Api.Routing;
using Fiap.SoftwareEngineering.Netflix.Api.Versioning;
using Fiap.SoftwareEngineering.Netflix.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Fiap.SoftwareEngineering.Netflix.User.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion(Versions.V1)]
    [Route(RoutePattern.VersionedRoute)]
    [Produces(ContentTypes.ApplicationJson)]
    public class UserController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return null;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]string value)
        {
            return null;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]string value)
        {
            return null;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return null;
        }
    }
}
