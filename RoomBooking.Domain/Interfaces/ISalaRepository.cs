using RoomBooking.Domain.Entities;

namespace RoomBooking.Domain.Interfaces;

public interface ISalaRepository
{
    Task<Sala> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Sala>> ListarTodasAsync();
    Task AdicionarAsync(Sala sala);
}