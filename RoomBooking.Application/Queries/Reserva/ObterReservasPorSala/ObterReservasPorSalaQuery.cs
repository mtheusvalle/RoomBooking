using MediatR;
using RoomBooking.Application.DTOs;

namespace RoomBooking.Application.Queries.Reserva.ObterReservasPorSala;

public record ObterReservasPorSalaQuery(Guid SalaId) : IRequest<IEnumerable<ReservaDto>>;