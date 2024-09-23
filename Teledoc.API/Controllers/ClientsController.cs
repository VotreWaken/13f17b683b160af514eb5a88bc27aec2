using MediatR;
using Microsoft.AspNetCore.Mvc;
using Teledoc.API.DTO;
using Teledoc.Application.Commands;
using Teledoc.Application.Queries;
using Teledoc.Application.Results;
using Teledoc.Domain.BoundedContexts.ClientManagement.Aggregates;
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
			var query = new GetAllClientsInfoQuery();

			CommandResult result = await _mediator.Send(query);
			return result.IsSuccess switch
			{
				true => Ok(result.SuccessObject),
				false => NotFound(result)
			};
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetClient([FromBody]int id)
		{
			var query = new GetClientByIdQuery(id);

			CommandResult result = await _mediator.Send(query);
			return result.IsSuccess switch
			{
				true => Ok(result.SuccessObject),
				false => HandleFailedCommand(result)
			};
		}

		[HttpPost]
		public async Task<ActionResult> AddClient([FromBody] CreateClientDTO dto)
		{

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var founders = dto.Founders
				.Select(f => new Founder(f.INN, f.FullName))
				.ToList();

			var command = new ClientCreateCommand(dto.INN, dto.Name, 
				dto.ClientType, founders);

			var result = await _mediator.Send(command);

			return result.IsSuccess switch
			{
				true => Ok(result.SuccessObject),
				false => NotFound(result)
			};
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateClient([FromBody] UpdateClientDTO dto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var founders = dto.Founders
				.Select(f => new Founder(f.INN, f.FullName))
				.ToList();

			var command = new ClientUpdateCommand(dto.Id, dto.INN, dto.Name, 
				dto.ClientType, founders);

			var result = await _mediator.Send(command);

			return result.IsSuccess switch
			{
				true => Ok(result.SuccessObject),
				false => HandleFailedCommand(result)
			};
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteClient([FromBody] int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var command = new ClientDeleteCommand(id);
			var result = await _mediator.Send(command);

			return result.IsSuccess switch
			{
				true => Ok(result.SuccessObject),
				false => HandleFailedCommand(result)
			};
		}

		protected IActionResult HandleFailedCommand(CommandResult result)
		{
			return result.FailureType switch
			{
				FailureTypes.NotFound => NotFound(),
				FailureTypes.Duplicate => BadRequest(result.FailureReasons),
				FailureTypes.BusinessRule => BadRequest(result.FailureReasons),
				_ => BadRequest()
			};
		}
	}
}
