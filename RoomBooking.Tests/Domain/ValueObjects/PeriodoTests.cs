using FluentAssertions;
using RoomBooking.Domain.ValueObjects;

namespace RoomBooking.Tests.Domain.ValueObjects
{
    public class PeriodoTests
    {
        [Fact]
        public void Constructor_WithValidPeriod_SetsProperties()
        {
            var inicio = DateTime.Now;
            var fim = inicio.AddHours(2);

            var periodo = new Periodo(inicio, fim);

            periodo.Inicio.Should().Be(inicio);
            periodo.Fim.Should().Be(fim);
        }

        [Fact]
        public void Constructor_WhenFimBeforeInicio_ThrowsArgumentException()
        {
            var inicio = DateTime.Now;
            var fim = inicio.AddHours(-1);

            Action act = () => new Periodo(inicio, fim);

            act.Should().Throw<ArgumentException>()
               .WithMessage("O fim do período deve ser posterior ao início.*");
        }
    }
}
