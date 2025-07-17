using MediatR;

namespace RoomBooking.Application.Commands.Reserva.CriarReserva;

public record CriarReservaCommand(Guid SalaId, DateTime Inicio, DateTime Fim, string Organizador) : IRequest<Guid>;