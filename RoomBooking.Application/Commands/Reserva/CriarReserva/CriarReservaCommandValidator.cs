using FluentValidation;

namespace RoomBooking.Application.Commands.Reserva.CriarReserva;

public class CriarReservaCommandValidator : AbstractValidator<CriarReservaCommand>
{
    public CriarReservaCommandValidator()
    {
        RuleFor(x => x.SalaId).NotEmpty();
        RuleFor(x => x.Inicio).LessThan(x => x.Fim);
        RuleFor(x => x.Organizador).NotEmpty().MaximumLength(100);
    }
}