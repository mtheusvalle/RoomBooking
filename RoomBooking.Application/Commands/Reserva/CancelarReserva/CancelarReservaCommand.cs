using MediatR;

namespace RoomBooking.Application.Commands.Reserva.CancelarReserva;

public record CancelarReservaCommand(Guid ReservaId) : IRequest<Unit>;