using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Commands.PropertyTraces.Create;
using RealEstate.Application.Queries.PropertyTraces.GetByPropertyId;

namespace RealEstate.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyTraceController : ControllerBase
    {
        private readonly ILogger<PropertyController> _logger;
        private readonly IMediator _mediator;
        public PropertyTraceController(ILogger<PropertyController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePropertyTraceCommand command, CancellationToken cancellationToken)
        {
            var newId = await _mediator.Send(command, cancellationToken);
            return StatusCode(201, newId);
        }

        [HttpGet("property-with-traces/{propertyId}")]
        public async Task<IActionResult> GetPropertyWithTraces(string propertyId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetPropertyWithTracesQuery(propertyId), cancellationToken);
            return result == null ? NotFound() : Ok(result);
        }
    }
}
