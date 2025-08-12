using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealEstate.API.Dtos;
using RealEstate.Application.Commands.Properties.Create;
using RealEstate.Application.Commands.Properties.Delete;
using RealEstate.Application.DTOs;
using RealEstate.Application.Queries.Properties.GetAll;
using RealEstate.Application.Queries.Properties.GetByFilters;
using RealEstate.Application.Queries.Properties.GetCompleteInformation;

namespace RealEstate.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly ILogger<PropertyController> _logger;
        private readonly IMediator _mediator;
        public PropertyController(ILogger<PropertyController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<PropertyDto>>> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllPropertiesQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] UpsertPropertyDto dto, CancellationToken cancellationToken)
        {
            var filesData = new List<FileDataDto>();
            if (dto.ImageFiles?.Any() == true)
            {
                for (int i = 0; i < dto.ImageFiles.Count; i++)
                {
                    var ms = new MemoryStream();
                    await dto.ImageFiles[i].CopyToAsync(ms);
                    ms.Position = 0;

                    filesData.Add(new FileDataDto()
                    {
                        FileStream = ms,
                        FileName = dto.ImageFiles[i].FileName,
                        ContentType = dto.ImageFiles[i].ContentType,
                        Enabled = dto.ImageFilesEnabled[i]
                    });
                }
            }

            var command = new CreatePropertyCommand
            {
                Name = dto.Name,
                Address = dto.Address,
                Price = dto.Price,
                Year = dto.Year,
                IdOwner = dto.IdOwner,
                FilesDataDto = filesData
            };

            var newId = await _mediator.Send(command, cancellationToken);
            return StatusCode(201, newId);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] string? name, [FromQuery] string? address, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice, CancellationToken cancellationToken)
        {
            var query = new GetByFiltersPropertyQuery(name, address, minPrice, maxPrice);
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeletePropertyCommand(id), cancellationToken);
            return NoContent();
        }


        [HttpGet("complete-information/{id}")]
        public async Task<ActionResult<List<PropertyDto>>> GetCompleteInformation(string id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCompleteInformationPropertyQuery(id), cancellationToken);
            return Ok(result);
        }

    }
}
