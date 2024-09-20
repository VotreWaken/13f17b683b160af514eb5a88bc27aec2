using MediatR;
using Microsoft.AspNetCore.Mvc;
using Teledoc.API.DTO;
using Teledoc.Application.Commands;
using Teledoc.Application.Queries;
using Teledoc.Application.QueryObjects;
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

			IEnumerable<Client> result = await _mediator.Send(query);
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

			Teledoc.Infrastructure.Entities.Client result = await _mediator.Send(query);
			return result switch
			{
				not null => Ok(result),
				null => NotFound()
			};
		}

		[HttpPost]
		public async Task<ActionResult<Client>> AddClient(CreateClientDTO dto)
		{
			var founders = dto.Founders
				.Select(f => new FounderCreateCommand(f.INN, f.FullName))
				.ToList();

			var command = new ClientCreateCommand(dto.INN, dto.Name, dto.ClientType, founders);
			var result = await _mediator.Send(command);
			return Ok(result);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateClient(UpdateClientDTO dto)
		{
			var founders = dto.Founders
				.Select(f => new FounderUpdateCommand(f.INN, f.FullName))
				.ToList();

			var command = new ClientUpdateCommand(dto.Id, dto.INN, dto.Name, dto.ClientType, founders);
			await _mediator.Send(command);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteClient(int id)
		{
			var command = new ClientDeleteCommand(id);
			await _mediator.Send(command);
			return NoContent();
		}
	}
}
