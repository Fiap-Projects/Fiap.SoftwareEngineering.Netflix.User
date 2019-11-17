using Fiap.SoftwareEngineering.Netflix.Api.Routing;
using Fiap.SoftwareEngineering.Netflix.Api.Versioning;
using Fiap.SoftwareEngineering.Netflix.Domain.Abstractions.Services;
using Fiap.SoftwareEngineering.Netflix.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using Fiap.SoftwareEngineering.Netflix.User.Domain.DTOs.Command;

namespace Fiap.SoftwareEngineering.Netflix.User.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion(Versions.V1)]
    [Route(RoutePattern.VersionedRoute)]
    [Produces(ContentTypes.ApplicationJson)]
    public class UserController : Controller
    {
        private readonly IDomainService<Domain.Entities.User> _domainService;

        public UserController(IDomainService<Domain.Entities.User> domainService)
        {
            _domainService = domainService ?? throw new ArgumentNullException(nameof(domainService));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _domainService.Reader.GetAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _domainService.Reader.GetAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserRegister payload, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //var result = await _domainService.AddAsync(payload, cancellationToken);
            return Created(nameof(Get), 1);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]UserRegister payload, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //await _domainService.UpdateAsync(payload, cancellationToken);
            return NoContent();
        }
    }
}
