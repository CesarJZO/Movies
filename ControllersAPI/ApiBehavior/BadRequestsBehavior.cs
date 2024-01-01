using Microsoft.AspNetCore.Mvc;

namespace ControllersAPI.ApiBehavior;

public static class BadRequestsBehavior
{
    public static void Parse(ApiBehaviorOptions options)
    {
        options.InvalidModelStateResponseFactory = actionContext =>
        {
            var response = actionContext.ModelState.Values
                .SelectMany(state => state.Errors)
                .Select(error => error.ErrorMessage)
                .ToArray();

            return new BadRequestObjectResult(response);
        };
    }
}
