using RoomBooking.Application.DTOs;

namespace RoomBooking.Application.Services;

public interface INotificationService
{
    Task NotificarReservaCriadaAsync(ReservaDto reserva);
}