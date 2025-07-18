using Microsoft.EntityFrameworkCore;
using RoomBooking.Domain.Entities;
using RoomBooking.Domain.Interfaces;
using RoomBooking.Infrastructure.Persistence;

namespace RoomBooking.Infrastructure.Repositories;

public class SalaRepository : ISalaRepository
{
    private readonly RoomBookingDbContext _context;

    public SalaRepository(RoomBookingDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(Sala sala)
    {
        _context.Salas.Add(sala);
        await _context.SaveChangesAsync();
    }

    public async Task<Sala> ObterPorIdAsync(Guid id)
        => await _context.Salas.FindAsync(id);

    public async Task<IEnumerable<Sala>> ListarTodasAsync()
        => await _context.Salas.ToListAsync();

    public async Task AtualizarAsync(Sala sala)
    {
        _context.Salas.Update(sala);
        await _context.SaveChangesAsync();
    }
}