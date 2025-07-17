using RoomBooking.Domain.Entities;

namespace RoomBooking.Domain.Interfaces;

public interface IReservaRepository
{
    Task<Reserva> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Reserva>> ObterPorSalaAsync(Guid salaId);
    Task AdicionarAsync(Reserva reserva);
    Task AtualizarAsync(Reserva reserva);
}