using MediatR;
using RoomBooking.Application.DTOs;

namespace RoomBooking.Application.Events
{
    public record ReservaCriadaEvent(ReservaDto ReservaDto) : INotification;
}
