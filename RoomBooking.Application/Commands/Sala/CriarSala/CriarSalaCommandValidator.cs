using FluentValidation;

namespace RoomBooking.Application.Commands.Sala.CriarSala;

public class CriarSalaCommandValidator : AbstractValidator<CriarSalaCommand>
{
    public CriarSalaCommandValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome da sala é obrigatório.");

        RuleFor(x => x.Capacidade)
            .GreaterThan(0).WithMessage("A capacidade deve ser maior que zero.")
            .LessThanOrEqualTo(100).WithMessage("A capacidade não pode exceder 100.");
    }
}