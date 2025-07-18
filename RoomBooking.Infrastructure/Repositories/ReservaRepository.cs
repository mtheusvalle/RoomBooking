using Microsoft.EntityFrameworkCore;
using RoomBooking.Domain.Entities;
using RoomBooking.Domain.Interfaces;
using RoomBooking.Domain.ValueObjects;
using RoomBooking.Infrastructure.Persistence;

namespace RoomBooking.Infrastructure.Repositories;

public class ReservaRepository : IReservaRepository, IReservaConflictValidator
{
    private readonly RoomBookingDbContext _context;

    public ReservaRepository(RoomBookingDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(Reserva reserva)
    {
        _context.Reservas.Add(reserva);
        await _context.SaveChangesAsync();
    }

    public async Task<Reserva> ObterPorIdAsync(Guid id)
        => await _context.Reservas.FindAsync(id);

    public async Task<IEnumerable<Reserva>> ObterPorSalaAsync(Guid salaId)
        => await _context.Reservas
            .Where(r => r.SalaId == salaId)
            .ToListAsync();

    public async Task<bool> ExisteConflitoAsync(Guid salaId, Periodo periodo)
    {
        return await _context.Reservas
                            .AnyAsync(r => r.SalaId == salaId &&
                                           r.Periodo.Inicio < periodo.Fim &&
                                           periodo.Inicio < r.Periodo.Fim);
    }

    public async Task<bool> ExcedeCapacidadeAsync(Guid salaId, int qntdPessoas)
    {
        return await _context.Salas
            .Where(s => s.Id == salaId)
            .Select(s => s.Capacidade.Quantidade)
            .FirstOrDefaultAsync() < qntdPessoas;
    }

    public async Task AtualizarAsync(Reserva reserva)
    {
        _context.Reservas.Update(reserva);
        await _context.SaveChangesAsync();
    }
}