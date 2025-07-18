using RoomBooking.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace RoomBooking.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro durante o processamento da requisição");

            context.Response.ContentType = "application/json";

            context.Response.StatusCode = ex switch
            {
                ReservaInvalidaException or SalaInvalidaException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var response = new
            {
                status = context.Response.StatusCode,
                title = "Erro ao processar a requisição",
                detail = ex.Message
            };

            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
}
