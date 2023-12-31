using Microsoft.AspNetCore.Mvc.Filters;

namespace ControllersAPI.Filters;

public sealed class ActionFilter : IActionFilter
{
    private readonly ILogger<ActionFilter> _logger;

    public ActionFilter(ILogger<ActionFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _logger.LogInformation("Action executed");
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogInformation("Action executing");
    }
}
