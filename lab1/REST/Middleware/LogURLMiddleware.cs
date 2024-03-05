﻿namespace REST.Middleware
{
    public class LogURLMiddleware(RequestDelegate Next, ILogger<LogURLMiddleware> Logger)
    {
        public async Task Invoke(HttpContext context)
        {
            Logger.LogInformation("{Context.Request.Path}", context.Request.Path);
            await Next(context);
        }
    }

    public static class LogExtension
    {
        public static IApplicationBuilder UseURLLog(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogURLMiddleware>();
        }
    }
}
