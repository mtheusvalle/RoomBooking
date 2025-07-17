using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomBooking.Domain.Entities;
using RoomBooking.Domain.ValueObjects;

namespace RoomBooking.Infrastructure.Persistence.Configurations;

internal class ReservaConfiguration : IEntityTypeConfiguration<Reserva>
{
    public void Configure(EntityTypeBuilder<Reserva> builder)
    {
        builder.ToTable("Reservas");
        builder.HasKey(r => r.Id);

        builder.Property(r => r.SalaId).IsRequired();

        builder.OwnsOne(typeof(Periodo), "Periodo", vo =>
        {
            vo.Property<DateTime>("Inicio").HasColumnName("Inicio").IsRequired();
            vo.Property<DateTime>("Fim").HasColumnName("Fim").IsRequired();
        });

        builder.Property(r => r.Organizador)
            .IsRequired()
            .HasMaxLength(100);
    }
}