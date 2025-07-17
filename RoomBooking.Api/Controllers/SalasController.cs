using MediatR;
using Microsoft.AspNetCore.Mvc;
using RoomBooking.Application.Commands.Sala.AtualizarSala;
using RoomBooking.Application.Commands.Sala.CriarSala;
using RoomBooking.Application.DTOs;
using RoomBooking.Application.Queries.Sala.ObterSalaPorId;
using RoomBooking.Application.Queries.Sala.ObterSalas;

namespace RoomBooking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalasController : ControllerBase
{
    private readonly IMediator _mediator;

    public SalasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IEnumerable<SalaDto>> GetAll()
        => await _mediator.Send(new ObterSalasQuery());

    [HttpGet("{id}")]
    public async Task<ActionResult<SalaDto>> GetById(Guid id)
    {
        var dto = await _mediator.Send(new ObterSalaPorIdQuery(id));
        if (dto == null) return NotFound();
        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CriarSalaCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] AtualizarSalaCommand command)
    {
        if (id != command.Id) return BadRequest();
        await _mediator.Send(command);
        return NoContent();
    }
}