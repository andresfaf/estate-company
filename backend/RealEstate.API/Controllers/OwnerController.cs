using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Commands.Owners.Create;
using RealEstate.Application.Commands.Owners.Delete;
using RealEstate.Application.Commands.Owners.Update;
using RealEstate.Application.DTOs;
using RealEstate.Application.Queries.Owners.GetAll;
using RealEstate.Application.Queries.Owners.GetById;

namespace RealEstate.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnerController : ControllerBase
    {
        private readonly ILogger<PropertyController> _logger;
        private readonly IMediator _mediator;
        public OwnerController(ILogger<PropertyController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<OwnerDto>>> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllOwnersQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetByIdOwnerQuery(id), cancellationToken);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] UpsertOwnerDto dto, CancellationToken cancellationToken)
        {
            using var stream = dto.ImageFile.OpenReadStream();

            var command = new CreateOwnerCommand
            {
                Name = dto.Name,
                Address = dto.Address,
                Birthday = dto.Birthday,
                FileStream = stream,
                FileName = dto.ImageFile.FileName,
                ContentType = dto.ImageFile.ContentType
            };

            var newId = await _mediator.Send(command, cancellationToken);
            return StatusCode(201, newId);
        }

        [HttpPut()]
        public async Task<IActionResult> Update([FromForm] UpsertOwnerDto dto, CancellationToken cancellationToken)
        {
            using var stream = dto.ImageFile.OpenReadStream();

            var command = new UpdateOwnerCommand
            {
                Id = dto.Id,
                Name = dto.Name,
                Address = dto.Address,
                Birthday = dto.Birthday,
                FileStream = stream,
                FileName = dto.ImageFile.FileName,
                ContentType = dto.ImageFile.ContentType,
                SelectedFile = (bool)dto.SelectedFile
            };

            var result = await _mediator.Send(command, cancellationToken);

            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteOwnerCommand(id), cancellationToken);
            return NoContent();
        }

    }
}
