using MediatR;
using Microsoft.AspNetCore.Mvc;
using RoomBooking.Application.Commands.Reserva.CancelarReserva;
using RoomBooking.Application.Commands.Reserva.CriarReserva;
using RoomBooking.Application.DTOs;
using RoomBooking.Application.Queries.Reserva.ObterReservaPorId;
using RoomBooking.Application.Queries.Reserva.ObterReservasPorSala;

namespace RoomBooking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservasController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReservasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("sala/{salaId}")]
    public async Task<IEnumerable<ReservaDto>> GetBySala(Guid salaId)
        => await _mediator.Send(new ObterReservasPorSalaQuery(salaId));

    [HttpGet("{id}")]
    public async Task<ActionResult<ReservaDto>> GetById(Guid id)
    {
        var dto = await _mediator.Send(new ObterReservaPorIdQuery(id));
        if (dto == null) return NotFound();
        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CriarReservaCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Cancel(Guid id)
    {
        await _mediator.Send(new CancelarReservaCommand(id));
        return NoContent();
    }
}