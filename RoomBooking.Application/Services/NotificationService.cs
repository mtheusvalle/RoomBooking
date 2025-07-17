using Microsoft.Extensions.Logging;
using RoomBooking.Application.DTOs;

namespace RoomBooking.Application.Services;

public class NotificationService : INotificationService
{
    private readonly ILogger<NotificationService> _logger;

    public NotificationService(ILogger<NotificationService> logger)
    {
        _logger = logger;
    }

    public Task NotificarReservaCriadaAsync(ReservaDto reserva)
    {
        _logger.LogInformation("Notificando reserva criada. Id: {ReservaId}, SalaId: {SalaId}, Início: {Inicio}, Fim: {Fim}, Organizador: {Organizador}",
            reserva.Id, reserva.SalaId, reserva.Inicio, reserva.Fim, reserva.Organizador);
        return Task.CompletedTask;
    }
}