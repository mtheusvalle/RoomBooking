using RoomBooking.Domain.Exceptions;
using RoomBooking.Domain.ValueObjects;

namespace RoomBooking.Domain.Entities;

public class Reserva
{
    public Guid Id { get; private set; }
    public Guid SalaId { get; private set; }
    public Periodo Periodo { get; private set; }
    public string Organizador { get; private set; }
    public int QntdPessoas { get; private set; }

    private Reserva() { }

    public Reserva(Guid id, Guid salaId, Periodo periodo, string organizador, int qntdPessoas)
    {
        Id = id;
        SalaId = salaId;
        Periodo = periodo;
        Organizador = organizador;
        QntdPessoas = qntdPessoas;
    }
}