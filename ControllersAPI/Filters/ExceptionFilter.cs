using Microsoft.AspNetCore.Mvc.Filters;

namespace ControllersAPI.Filters;

public sealed class ExceptionFilter : ExceptionFilterAttribute
{
    private readonly ILogger<ExceptionFilter> _logger;

    public ExceptionFilter(ILogger<ExceptionFilter> logger)
    {
        _logger = logger;
    }

    public override void OnException(ExceptionContext context)
    {
        // _logger.LogError(context.Exception, context.Exception.Message);
        _logger.LogError("Exception thrown: {Exception}", context.Exception.Message);
        base.OnException(context);
    }
}
