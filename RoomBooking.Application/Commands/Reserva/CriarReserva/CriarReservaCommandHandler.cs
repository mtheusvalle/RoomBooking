using MediatR;
using RoomBooking.Application.DTOs;
using RoomBooking.Application.Services;
using RoomBooking.Domain.Events;
using RoomBooking.Domain.Interfaces;
using RoomBooking.Domain.ValueObjects;

namespace RoomBooking.Application.Commands.Reserva.CriarReserva;

public class CriarReservaCommandHandler : IRequestHandler<CriarReservaCommand, Guid>
{
    private readonly IReservaRepository _reservaRepository;
    private readonly ISalaRepository _salaRepository;
    private readonly IReservaConflictValidator _conflictValidator;
    private readonly IMediator _mediator;
    private readonly INotificationService _notificationService;

    public CriarReservaCommandHandler(
        IReservaRepository reservaRepository,
        ISalaRepository salaRepository,
        IReservaConflictValidator conflictValidator,
        IMediator mediator,
        INotificationService notificationService)
    {
        _reservaRepository = reservaRepository;
        _salaRepository = salaRepository;
        _conflictValidator = conflictValidator;
        _mediator = mediator;
        _notificationService = notificationService;
    }

    public async Task<Guid> Handle(CriarReservaCommand request, CancellationToken cancellationToken)
    {
        var sala = await _salaRepository.ObterPorIdAsync(request.SalaId);
        var periodo = new Periodo(request.Inicio, request.Fim);
        var reserva = new Domain.Entities.Reserva(Guid.NewGuid(), sala.Id, periodo, request.Organizador, _conflictValidator);
        await _reservaRepository.AdicionarAsync(reserva);

        var evento = new ReservaCriadaEvent(reserva.Id, sala.Id, periodo.Inicio, periodo.Fim);
        await _mediator.Publish(evento, cancellationToken);

        var reservaDto = new ReservaDto
        {
            Id = reserva.Id,
            SalaId = sala.Id,
            Inicio = periodo.Inicio,
            Fim = periodo.Fim,
            Organizador = request.Organizador
        };
        await _notificationService.NotificarReservaCriadaAsync(reservaDto);

        return reserva.Id;
    }
}