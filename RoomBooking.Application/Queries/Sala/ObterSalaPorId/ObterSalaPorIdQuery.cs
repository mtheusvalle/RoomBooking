using MediatR;
using RoomBooking.Application.DTOs;

namespace RoomBooking.Application.Queries.Sala.ObterSalaPorId;

public record ObterSalaPorIdQuery(Guid Id) : IRequest<SalaDto>;