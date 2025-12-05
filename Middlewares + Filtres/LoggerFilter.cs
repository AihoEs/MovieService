using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _logger;

    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "Exception caught by filter");

        context.Result = new JsonResult(new
        {
            message = "An error occurred",
            details = context.Exception.Message
        })
        {
            StatusCode = 500
        };

        context.ExceptionHandled = true; 
    }
}