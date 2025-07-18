using MediatR;
using RoomBooking.Application.DTOs;

namespace RoomBooking.Application.Commands.Sala.CriarSala;

public record CriarSalaCommand(string Nome, int Capacidade) : IRequest<SalaDto>;