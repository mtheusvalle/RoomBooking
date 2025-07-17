using MediatR;
using RoomBooking.Domain.Interfaces;

namespace RoomBooking.Application.Commands.Reserva.CancelarReserva;

public class CancelarReservaCommandHandler : IRequestHandler<CancelarReservaCommand, Unit>
{
    private readonly IReservaRepository _reservaRepository;

    public CancelarReservaCommandHandler(IReservaRepository reservaRepository)
    {
        _reservaRepository = reservaRepository;
    }

    public async Task<Unit> Handle(CancelarReservaCommand request, CancellationToken cancellationToken)
    {
        var reserva = await _reservaRepository.ObterPorIdAsync(request.ReservaId);
        reserva.Cancelar();
        await _reservaRepository.AtualizarAsync(reserva);
        return Unit.Value;
    }
}