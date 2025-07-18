using AutoMapper;
using MediatR;
using RoomBooking.Application.DTOs;
using RoomBooking.Application.Events;
using RoomBooking.Application.Services;
using RoomBooking.Domain.Exceptions;
using RoomBooking.Domain.Interfaces;
using RoomBooking.Domain.ValueObjects;

namespace RoomBooking.Application.Commands.Reserva.CriarReserva;

public class CriarReservaCommandHandler : IRequestHandler<CriarReservaCommand, ReservaDto>
{
    private readonly IReservaRepository _reservaRepository;
    private readonly ISalaRepository _salaRepository;
    private readonly IReservaConflictValidator _conflictValidator;
    private readonly INotificationService _notificationService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CriarReservaCommandHandler(
        IReservaRepository reservaRepository,
        ISalaRepository salaRepository,
        IReservaConflictValidator conflictValidator,
        INotificationService notificationService,
        IMapper mapper,
        IMediator mediator)
    {
        _reservaRepository = reservaRepository;
        _salaRepository = salaRepository;
        _conflictValidator = conflictValidator;
        _notificationService = notificationService;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<ReservaDto> Handle(CriarReservaCommand request, CancellationToken cancellationToken)
    {
        var sala = await _salaRepository.ObterPorIdAsync(request.SalaId);
        var periodo = new Periodo(request.Inicio, request.Fim);

        bool conflito = await _conflictValidator.ExisteConflitoAsync(request.SalaId, periodo);
        if (conflito)
            throw new ReservaInvalidaException("Já existe uma reserva para este período.");

        bool excedeCapacidade = await _reservaRepository.ExcedeCapacidadeAsync(request.SalaId, request.QntdPessoas);
        if (excedeCapacidade)
            throw new ReservaInvalidaException($"A reserva excede a capacidade da sala de {sala.Capacidade.Quantidade} pessoas.");

        var reserva = new Domain.Entities.Reserva(Guid.NewGuid(), sala.Id, periodo, request.Organizador, request.QntdPessoas);

        await _reservaRepository.AdicionarAsync(reserva);

        var reservaRetorno = _mapper.Map<ReservaDto>(reserva);

        await _mediator.Publish(new ReservaCriadaEvent(reservaRetorno));

        await _notificationService.NotificarReservaCriadaAsync(reservaRetorno);

        return reservaRetorno;
    }
}