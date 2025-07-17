namespace RoomBooking.Domain.Events;

public record ReservaCriadaEvent(Guid ReservaId, Guid SalaId, DateTime Inicio, DateTime Fim);