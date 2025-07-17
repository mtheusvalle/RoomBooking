namespace RoomBooking.Application.DTOs;

public class ReservaDto
{
    public Guid Id { get; set; }
    public Guid SalaId { get; set; }
    public DateTime Inicio { get; set; }
    public DateTime Fim { get; set; }
    public string Organizador { get; set; }
}