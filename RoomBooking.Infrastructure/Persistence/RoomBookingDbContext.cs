using Microsoft.EntityFrameworkCore;
using RoomBooking.Domain.Entities;
using RoomBooking.Infrastructure.Persistence.Configurations;

namespace RoomBooking.Infrastructure.Persistence;

public class RoomBookingDbContext : DbContext
{
    public DbSet<Sala> Salas { get; set; }
    public DbSet<Reserva> Reservas { get; set; }

    public RoomBookingDbContext(DbContextOptions<RoomBookingDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new SalaConfiguration());
        modelBuilder.ApplyConfiguration(new ReservaConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}