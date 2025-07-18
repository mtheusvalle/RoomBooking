using AutoMapper;
using FluentAssertions;
using MediatR;
using Moq;
using RoomBooking.Application.Commands.Reserva.CriarReserva;
using RoomBooking.Application.DTOs;
using RoomBooking.Application.Services;
using RoomBooking.Domain.Entities;
using RoomBooking.Domain.Exceptions;
using RoomBooking.Domain.Interfaces;
using RoomBooking.Domain.ValueObjects;

namespace RoomBooking.Tests.Application.Commands.CriarReserva
{
    public class CriarReservaCommandHandlerTests
    {
        private readonly Mock<IReservaRepository> _resRepo = new();
        private readonly Mock<ISalaRepository> _salaRepo = new();
        private readonly Mock<IReservaConflictValidator> _conflictValidator = new();
        private readonly Mock<IMediator> _mediator = new();
        private readonly Mock<INotificationService> _notification = new();
        private readonly Mock<IMapper> _mapper = new();

        [Fact]
        public async Task Handle_WithValidRequest_CreatesReservaAndPublishesEventAndNotification()
        {
            // Arrange
            var salaId = Guid.NewGuid();
            var command = new CriarReservaCommand(salaId, DateTime.Today.AddHours(9), DateTime.Today.AddHours(10), "User", 10);
            var sala = new Sala(salaId, "Sala A", new Capacidade(10));
            var reserva = new Reserva(Guid.NewGuid(), salaId, new Periodo(command.Inicio, command.Fim), command.Organizador, command.QntdPessoas);
            var reservaDto = new ReservaDto
            {
                Id = reserva.Id,
                SalaId = salaId,
                Inicio = command.Inicio,
                Fim = command.Fim,
                Organizador = command.Organizador
            };

            _salaRepo.Setup(x => x.ObterPorIdAsync(salaId)).ReturnsAsync(sala);
            _conflictValidator.Setup(x => x.ExisteConflitoAsync(salaId, It.IsAny<Periodo>())).ReturnsAsync(false);
            _mapper.Setup(x => x.Map<ReservaDto>(It.IsAny<Reserva>())).Returns(reservaDto);

            var handler = new CriarReservaCommandHandler(
                _resRepo.Object, _salaRepo.Object,
                _conflictValidator.Object,
                _notification.Object, _mapper.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(reservaDto.Id);
            result.SalaId.Should().Be(salaId);
            result.Inicio.Should().Be(command.Inicio);
            result.Fim.Should().Be(command.Fim);
            result.Organizador.Should().Be(command.Organizador);

            _resRepo.Verify(x => x.AdicionarAsync(It.IsAny<Reserva>()), Times.Once);
            _notification.Verify(x => x.NotificarReservaCriadaAsync(It.Is<ReservaDto>(d => d.Id == reservaDto.Id && d.SalaId == salaId)), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenConflict_ThrowsReservaInvalidaException()
        {
            // Arrange
            var salaId = Guid.NewGuid();
            var command = new CriarReservaCommand(salaId, DateTime.Today.AddHours(9), DateTime.Today.AddHours(10), "User", 10);
            var sala = new Sala(salaId, "Sala A", new Capacidade(10));

            _salaRepo.Setup(x => x.ObterPorIdAsync(salaId)).ReturnsAsync(sala);

            _conflictValidator.Setup(x => x.ExisteConflitoAsync(salaId, It.IsAny<Periodo>())).ReturnsAsync(true);

            var handler = new CriarReservaCommandHandler(
                _resRepo.Object, _salaRepo.Object,
                _conflictValidator.Object,
                _notification.Object, _mapper.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ReservaInvalidaException>(() => handler.Handle(command, default));
        }

        [Fact]
        public async Task Handle_WhenExceedsCapacity_ThrowsReservaInvalidaException()
        {
            // Arrange
            var salaId = Guid.NewGuid();
            var command = new CriarReservaCommand(salaId, DateTime.Today.AddHours(9), DateTime.Today.AddHours(10), "User", 20);
            var sala = new Sala(salaId, "Sala A", new Capacidade(10));
            _salaRepo.Setup(x => x.ObterPorIdAsync(salaId)).ReturnsAsync(sala);
            _conflictValidator.Setup(x => x.ExisteConflitoAsync(salaId, It.IsAny<Periodo>())).ReturnsAsync(false);
            _resRepo.Setup(x => x.ExcedeCapacidadeAsync(salaId, command.QntdPessoas)).ReturnsAsync(true);
            var handler = new CriarReservaCommandHandler(
                _resRepo.Object, _salaRepo.Object,
                _conflictValidator.Object,
                _notification.Object, _mapper.Object);
            // Act & Assert
            await Assert.ThrowsAsync<ReservaInvalidaException>(() => handler.Handle(command, default));
        }
    }
}
