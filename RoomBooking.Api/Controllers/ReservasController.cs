using MediatR;
using Microsoft.AspNetCore.Mvc;
using RoomBooking.Application.Commands.Reserva.CriarReserva;
using RoomBooking.Application.DTOs;
using RoomBooking.Application.Queries.Reserva.ObterReservaPorId;
using RoomBooking.Application.Queries.Reserva.ObterReservasPorSala;

namespace RoomBooking.Api.Controllers;

/// <summary>
/// Gerencia as operações relacionadas a reservas de salas.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ReservasController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReservasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Retorna todas as reservas associadas a uma sala específica.
    /// </summary>
    /// <param name="salaId">Identificador da sala.</param>
    /// <returns>Lista de reservas da sala.</returns>
    /// <response code="200">Lista retornada com sucesso.</response>
    [HttpGet("sala/{salaId}")]
    [ProducesResponseType(typeof(IEnumerable<ReservaDto>), StatusCodes.Status200OK)]
    public async Task<IEnumerable<ReservaDto>> GetBySala(Guid salaId)
        => await _mediator.Send(new ObterReservasPorSalaQuery(salaId));

    /// <summary>
    /// Retorna os detalhes de uma reserva específica.
    /// </summary>
    /// <param name="id">Identificador da reserva.</param>
    /// <returns>Detalhes da reserva.</returns>
    /// <response code="200">Reserva encontrada com sucesso.</response>
    /// <response code="404">Reserva não encontrada.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ReservaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReservaDto>> GetById(Guid id)
    {
        var dto = await _mediator.Send(new ObterReservaPorIdQuery(id));
        if (dto == null) return NotFound();
        return Ok(dto);
    }

    /// <summary>
    /// Cria uma nova reserva para uma sala.
    /// </summary>
    /// <param name="command">Dados da reserva.</param>
    /// <returns>Dados da reserva criada.</returns>
    /// <response code="201">Reserva criada com sucesso.</response>
    /// <response code="400">Se os dados forem inválidos ou houver conflito de horário.</response>
    [HttpPost]
    [ProducesResponseType(typeof(ReservaDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ReservaDto>> Create([FromBody] CriarReservaCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }
}
