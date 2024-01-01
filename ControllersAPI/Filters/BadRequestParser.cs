using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ControllersAPI.Filters;

public sealed class BadRequestParser : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is not IStatusCodeActionResult castResult) return;

        var statusCode = castResult.StatusCode;

        if (statusCode is not 400) return;

        var response = new List<string>();
        var currentResponse = context.Result as BadRequestObjectResult;

        if (currentResponse?.Value is string message)
        {
            response.Add(message);
        }
        else
        {
            foreach (var key in context.ModelState.Keys)
            {
                foreach (var error in context.ModelState[key]!.Errors)
                {
                    response.Add($"{key}: {error.ErrorMessage}");
                }
            }
        }

        context.Result = new BadRequestObjectResult(response);
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        
    }
}
