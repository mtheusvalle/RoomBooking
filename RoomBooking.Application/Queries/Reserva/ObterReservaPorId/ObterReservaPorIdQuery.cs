using MediatR;
using RoomBooking.Application.DTOs;

namespace RoomBooking.Application.Queries.Reserva.ObterReservaPorId;

public record ObterReservaPorIdQuery(Guid Id) : IRequest<ReservaDto>;