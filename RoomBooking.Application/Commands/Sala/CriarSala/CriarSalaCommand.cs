using MediatR;

namespace RoomBooking.Application.Commands.Sala.CriarSala;

public record CriarSalaCommand(Guid Id, string Nome, int Capacidade) : IRequest<Guid>;