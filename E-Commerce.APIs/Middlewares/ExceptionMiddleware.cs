
using E_Commerce.APIs.Errors;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace E_Commerce.APIs.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger,IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }
        public async Task InvokeAsync(HttpContext httpContext, RequestDelegate _next)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message); // dev env
                                              //prod env Log in Database | file

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var response = _env.IsDevelopment() ? new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    :
                    new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);

                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var json = JsonSerializer.Serialize(response, options);
                await httpContext.Response.WriteAsync(json);
            }
        }
    }
}
