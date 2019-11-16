using Fiap.SoftwareEngineering.Netflix.Api.Routing;
using Fiap.SoftwareEngineering.Netflix.Api.Versioning;
using Fiap.SoftwareEngineering.Netflix.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Fiap.SoftwareEngineering.Netflix.User.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion(Versions.V1)]
    [Route(RoutePattern.VersionedRoute)]
    [Produces(ContentTypes.ApplicationJson)]
    public class UserController : Controller
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
