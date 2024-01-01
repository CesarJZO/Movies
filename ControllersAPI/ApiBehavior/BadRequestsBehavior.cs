using Microsoft.AspNetCore.Mvc;

namespace ControllersAPI.ApiBehavior;

public static class BadRequestsBehavior
{
    public static void Parse(ApiBehaviorOptions options)
    {
        options.InvalidModelStateResponseFactory = actionContext =>
        {
            var response = actionContext.ModelState
                .SelectMany(pair => pair.Value!.Errors
                    .Select(error => $"{pair.Key}: {error.ErrorMessage}"))
                .ToArray();

            return new BadRequestObjectResult(response);
        };
    }
}
