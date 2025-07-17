using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomBooking.Domain.Entities;
using RoomBooking.Domain.ValueObjects;

namespace RoomBooking.Infrastructure.Persistence.Configurations;

internal class SalaConfiguration : IEntityTypeConfiguration<Sala>
{
    public void Configure(EntityTypeBuilder<Sala> builder)
    {
        builder.ToTable("Salas");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Nome)
            .IsRequired()
            .HasMaxLength(200);

        builder.OwnsOne(typeof(Capacidade), "Capacidade", vo =>
        {
            vo.Property<int>("Quantidade").HasColumnName("Capacidade").IsRequired();
        });
    }
}