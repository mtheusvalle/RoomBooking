using System.Text.Json;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using RoomBooking.Application.DTOs;
using RoomBooking.Application.Services;

namespace RoomBooking.Infrastructure.HttpClients;

public class NotificationClient : INotificationService
{
    private readonly HttpClient _httpClient;
    private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;
    private readonly AsyncCircuitBreakerPolicy<HttpResponseMessage> _circuitBreaker;

    public NotificationClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _retryPolicy = Policy
            .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        _circuitBreaker = Policy
            .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
            .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
    }

    public async Task NotificarReservaCriadaAsync(ReservaDto reserva)
    {
        #if DEBUG
                // Em ambiente de desenvolvimento, apenas loga
                Console.WriteLine($"[Mock] Notificação de reserva enviada: {JsonSerializer.Serialize(reserva)}");
                await Task.CompletedTask;
        #else
            var content = new StringContent(JsonSerializer.Serialize(reserva), Encoding.UTF8, "application/json");

            await _retryPolicy.ExecuteAsync(() =>
                _circuitBreaker.ExecuteAsync(() =>
                    _httpClient.PostAsync("/notifications/reserva", content)));
        #endif
    }
}