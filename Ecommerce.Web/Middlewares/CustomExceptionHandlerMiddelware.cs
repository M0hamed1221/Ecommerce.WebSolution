using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared.ErrorModles;
using System.Net;
using System.Text.Json;

namespace Ecommerce.Web.Middlewares
{
    public class CustomExceptionHandlerMiddelware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddelware> _logger;
        public CustomExceptionHandlerMiddelware(RequestDelegate next,ILogger<CustomExceptionHandlerMiddelware>logger)
        {
            _next = next;
            _logger = logger;
            
        }
        public async Task InvokeAsync(HttpContext context)

        {
            try
            {

                await _next.Invoke(context);
                // if End Point Is Not Found 
                await HandelNotFoundEndPoint(context);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sonthing Went Wrong");
                //Response object with content Type , status code
                await HandelCatchException(context, ex);
            }
        }

        private static async Task HandelCatchException(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            var response = new ErrorDetailes()
            {
                //StatusCode = (int)HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };

            response.StatusCode = ex switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError,

            };

            context.Response.StatusCode = response.StatusCode;


            var jsonReult = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(jsonReult);
        }

        private static async Task HandelNotFoundEndPoint(HttpContext context)
        {
            if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
            {
                context.Response.ContentType = "application/json";
                var reponse = new ErrorDetailes()
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    ErrorMessage = $"End Point With This Path :{context.Request.Path} is Not Found"

                };
                await context.Response.WriteAsJsonAsync
                    (reponse);
            }
        }
    }
}
