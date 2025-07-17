using MediatR;
using RoomBooking.Application.DTOs;

namespace RoomBooking.Application.Queries.Sala.ObterSalas;

public record ObterSalasQuery : IRequest<IEnumerable<SalaDto>>;