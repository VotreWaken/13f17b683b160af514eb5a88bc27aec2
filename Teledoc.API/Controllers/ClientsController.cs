using MediatR;
using Microsoft.AspNetCore.Mvc;
using Teledoc.API.DTO;
using Teledoc.Application.Commands;
using Teledoc.Application.Queries;
using Teledoc.Application.Results;
using Teledoc.Infrastructure.Entities;
using static Teledoc.Application.Commands.ClientUpdateCommand;

namespace Teledoc.API.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class ClientsController : ControllerBase
	{
		private readonly IMediator _mediator;

		public ClientsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Client>>> GetAllClientsQuery()
		{
			var query = new GetAllProductsInfosQuery();

			CommandResult result = await _mediator.Send(query);
			return result switch
			{
				not null => Ok(result),
				null => NotFound()
			};
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetClient(int id)
		{
			var query = new GetClientByIdQuery(id);

			CommandResult result = await _mediator.Send(query);
			return result switch
			{
				not null => Ok(result),
				null => NotFound()
			};
		}

		[HttpPost]
		public async Task<ActionResult<Client>> AddClient(CreateClientDTO dto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var founders = dto.Founders
				.Select(f => new FounderCreateCommand(f.INN, f.FullName))
				.ToList();

			var command = new ClientCreateCommand(dto.INN, dto.Name, 
				dto.ClientType, founders);

			var result = await _mediator.Send(command);

			return result switch
			{
				not null => Ok(result),
				null => NotFound()
			};
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateClient(UpdateClientDTO dto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var founders = dto.Founders
				.Select(f => new FounderUpdateCommand(f.INN, f.FullName))
				.ToList();

			var command = new ClientUpdateCommand(dto.Id, dto.INN, dto.Name, 
				dto.ClientType, founders);

			var result = await _mediator.Send(command);

			return result switch
			{
				not null => Ok(result),
				null => NotFound()
			};
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteClient(int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var command = new ClientDeleteCommand(id);
			var result = await _mediator.Send(command);

			return result switch
			{
				not null => Ok(result),
				null => NotFound()
			};
		}
	}
}
