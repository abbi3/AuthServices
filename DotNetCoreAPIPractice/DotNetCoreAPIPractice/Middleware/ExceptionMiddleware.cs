using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Text.Json;
using Serilog;

namespace AuthService.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Something went wrong");
                await HandleExceptionAsync(context, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            if (ex.Message.Contains("not found"))
                statusCode = (int)HttpStatusCode.NotFound;
            else if (ex.Message.Contains("Invalid"))
                statusCode = (int)HttpStatusCode.BadRequest;
            var response = new
            {
                message = ex.Message,
                statusCode = statusCode
            };
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
