using MediatR;
using RoomBooking.Application.DTOs;

namespace RoomBooking.Application.Commands.Reserva.CriarReserva;

public record CriarReservaCommand(Guid SalaId, DateTime Inicio, DateTime Fim, string Organizador, int QntdPessoas) : IRequest<ReservaDto>;