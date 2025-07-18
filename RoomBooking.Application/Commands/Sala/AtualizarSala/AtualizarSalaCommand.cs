using MediatR;
using RoomBooking.Application.DTOs;

namespace RoomBooking.Application.Commands.Sala.AtualizarSala;

public record AtualizarSalaCommand(Guid Id, string NovoNome, int NovaCapacidade) : IRequest<SalaDto>;