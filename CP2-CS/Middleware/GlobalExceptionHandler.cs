using CP2_CS.Domain.Exceptions;
using CP2_CS.Domain.Exceptions;

namespace CP2_CS.Middleware;

public class RespostaDeErro
{
    public int Status { get; set; }
    public string Mensagem { get; set; } = string.Empty;

    public RespostaDeErro(int status, string mensagem)
    {
        Status = status;
        Mensagem = mensagem;
    }
}

public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            context.Response.StatusCode = ex switch
            {
                DomainExceptionValidation => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new RespostaDeErro(context.Response.StatusCode, ex.Message));
        }
    }
}