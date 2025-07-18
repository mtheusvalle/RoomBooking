using FluentAssertions;
using RoomBooking.Domain.ValueObjects;

namespace RoomBooking.Tests.Domain.ValueObjects
{
    public class CapacidadeTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(50)]
        [InlineData(100)]
        public void Constructor_WithValidQuantidade_SetsProperty(int qtd)
        {
            var cap = new Capacidade(qtd);
            cap.Quantidade.Should().Be(qtd);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void Constructor_WithNonPositiveQuantidade_ThrowsArgumentException(int qtd)
        {
            Action act = () => new Capacidade(qtd);
            act.Should().Throw<ArgumentException>()
               .WithMessage("A capacidade deve ser maior que zero.*");
        }
    }
}
