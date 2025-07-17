using RoomBooking.Domain.ValueObjects;

namespace RoomBooking.Domain.Interfaces;

public interface IReservaConflictValidator
{
    Task<bool> ExisteConflitoAsync(Guid salaId, Periodo periodo);
}