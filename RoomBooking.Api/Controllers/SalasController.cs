using MediatR;
using Microsoft.AspNetCore.Mvc;
using RoomBooking.Application.Commands.Sala.AtualizarSala;
using RoomBooking.Application.Commands.Sala.CriarSala;
using RoomBooking.Application.DTOs;
using RoomBooking.Application.Queries.Sala.ObterSalaPorId;
using RoomBooking.Application.Queries.Sala.ObterSalas;

namespace RoomBooking.Api.Controllers;

/// <summary>
/// Gerencia as opera��es de salas.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SalasController : ControllerBase
{
    private readonly IMediator _mediator;

    public SalasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Obt�m todas as salas cadastradas.
    /// </summary>
    /// <returns>Lista de salas dispon�veis.</returns>
    /// <response code="200">Retorna a lista de salas.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SalaDto>), StatusCodes.Status200OK)]
    public async Task<IEnumerable<SalaDto>> GetAll()
        => await _mediator.Send(new ObterSalasQuery());

    /// <summary>
    /// Retorna os detalhes de uma sala espec�fica pelo ID.
    /// </summary>
    /// <param name="id">Identificador �nico da sala.</param>
    /// <returns>Detalhes da sala solicitada.</returns>
    /// <response code="200">Retorna a sala encontrada.</response>
    /// <response code="404">Se a sala n�o for encontrada.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SalaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SalaDto>> GetById(Guid id)
    {
        var dto = await _mediator.Send(new ObterSalaPorIdQuery(id));
        if (dto == null) return NotFound();
        return Ok(dto);
    }

    /// <summary>
    /// Cadastra uma nova sala.
    /// </summary>
    /// <param name="command">Dados da nova sala.</param>
    /// <returns>Detalhes da sala criada.</returns>
    /// <response code="201">Sala criada com sucesso.</response>
    /// <response code="400">Dados inv�lidos.</response>
    [HttpPost]
    [ProducesResponseType(typeof(SalaDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SalaDto>> Create([FromBody] CriarSalaCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Atualiza as informa��es de uma sala existente.
    /// </summary>
    /// <param name="id">Identificador da sala a ser atualizada.</param>
    /// <param name="command">Novos dados da sala.</param>
    /// <returns>Dados atualizados da sala.</returns>
    /// <response code="200">Atualiza��o realizada com sucesso.</response>
    /// <response code="400">Se o ID n�o corresponder ou dados inv�lidos.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(SalaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SalaDto>> Update(Guid id, [FromBody] AtualizarSalaCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}