using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using RoomBooking.Application.Services;
using RoomBooking.Domain.Interfaces;
using RoomBooking.Infrastructure.HttpClients;
using RoomBooking.Infrastructure.Persistence;
using RoomBooking.Infrastructure.Repositories;

namespace RoomBooking.Infrastructure.DependencyInjection;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // DbContext
        services.AddDbContext<RoomBookingDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Repositories
        services.AddScoped<ISalaRepository, SalaRepository>();
        services.AddScoped<IReservaRepository, ReservaRepository>();
        services.AddScoped<IReservaConflictValidator, ReservaRepository>();

        // HTTP Client & Polly
        services.AddHttpClient<INotificationService, NotificationClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["NotificationService:BaseUrl"]);
            })
            .AddPolicyHandler(GetRetryPolicy())
            .AddPolicyHandler(GetCircuitBreakerPolicy());

        return services;
    }

    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        => HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

    private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        => HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
}