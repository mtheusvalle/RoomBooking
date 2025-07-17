using FluentValidation;

namespace RoomBooking.Application.Commands.Sala.AtualizarSala;

public class AtualizarSalaCommandValidator : AbstractValidator<AtualizarSalaCommand>
{
    public AtualizarSalaCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.NovoNome).NotEmpty();
        RuleFor(x => x.NovaCapacidade)
            .GreaterThan(0)
            .LessThanOrEqualTo(100);
    }
}