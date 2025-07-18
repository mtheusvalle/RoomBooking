using AutoMapper;
using FluentAssertions;
using Moq;
using RoomBooking.Application.Commands.Sala.CriarSala;
using RoomBooking.Application.DTOs;
using RoomBooking.Domain.Entities;
using RoomBooking.Domain.Interfaces;
using RoomBooking.Domain.ValueObjects;

namespace RoomBooking.Tests.Application.Commands.CriarSala
{
    public class CriarSalaCommandHandlerTests
    {
        [Fact]
        public async Task Handle_WithValidRequest_CreatesSala()
        {
            var repo = new Mock<ISalaRepository>();
            var mapper = new Mock<IMapper>();
            var command = new CriarSalaCommand("Sala B", 20);
            var salaId = Guid.NewGuid();
            var sala = new Sala(salaId, command.Nome, new Capacidade(command.Capacidade));
            var salaDto = new SalaDto { Id = salaId, Nome = command.Nome, Capacidade = command.Capacidade };
        
            mapper.Setup(m => m.Map<SalaDto>(It.IsAny<Sala>())).Returns(salaDto);

            var handler = new CriarSalaCommandHandler(repo.Object, mapper.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.Id.Should().Be(salaId);
            result.Nome.Should().Be("Sala B");
            result.Capacidade.Should().Be(20);

            repo.Verify(x => x.AdicionarAsync(
                It.Is<Sala>(s => s.Id == salaId && s.Nome == "Sala B" && s.Capacidade.Quantidade == 20)), Times.Once);
        }

        [Fact]
        public void Validator_InvalidNome_ResultsInFailure()
        {
            var validator = new CriarSalaCommandValidator();
            var result = validator.Validate(new CriarSalaCommand("", -1));

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "Nome");
            result.Errors.Should().Contain(e => e.PropertyName == "Capacidade");
        }
    }
}
