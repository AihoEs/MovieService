using Microsoft.AspNetCore.Http;
using System;

namespace MoviesOnline.Middlewares
{
    public class TimingMiddleware
    {
        private readonly RequestDelegate _next;
        public readonly ILogger<TimingMiddleware> _logger;

        public TimingMiddleware(RequestDelegate next, ILogger<TimingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            await _next(context);
            watch.Stop();

            var time = watch.ElapsedMilliseconds;

            _logger.LogInformation(
                "New request {method} in path {url} executed in {time} ms with status {statusCode}",
                context.Request.Method,
                context.Request.Path + context.Request.QueryString,
                time,
                context.Response.StatusCode);


        }

    }
    public static class TimingMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogTiming(this  IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TimingMiddleware>();
        }
    }
}
