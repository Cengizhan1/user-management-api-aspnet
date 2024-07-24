using System.Diagnostics;
using System.Net;
using System.Text.Json;
using Service.Exceptions;

namespace API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            // Hataları türlerine göre yakalama
            switch (exception)
            {
                case NotFoundException notFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return context.Response.WriteAsync(JsonSerializer.Serialize(new
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = notFoundException.Message
                    }));

                case BadRequestException badRequestException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return context.Response.WriteAsync(JsonSerializer.Serialize(new
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = badRequestException.Message
                    }));

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return context.Response.WriteAsync(JsonSerializer.Serialize(new
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = "Internal Server Error from the custom middleware.",
                        Detailed = exception.Message
                    }));
            }
        }
    }

}
