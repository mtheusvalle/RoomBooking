using FluentValidation;

namespace RoomBooking.Application.Commands.Reserva.CriarReserva;

public class CriarReservaCommandValidator : AbstractValidator<CriarReservaCommand>
{
    public CriarReservaCommandValidator()
    {
        RuleFor(x => x.SalaId).NotEmpty();
        RuleFor(x => x.Inicio).LessThan(x => x.Fim);
        RuleFor(x => x.Organizador).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Fim)
            .GreaterThan(x => x.Inicio)
            .WithMessage("A data de fim deve ser maior que a data de início.");
        RuleFor(x => x.Inicio)
            .GreaterThanOrEqualTo(DateTime.Now)
            .WithMessage("A data de início não pode ser no passado.");
    }
}