using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace CompanyInventory.WebApi.Configuration
{
    public static class ExceptionConfig
    {
        public static async Task SetExceptionResponseAsync(this HttpContext context)
        {
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            var exceptionHandler = context.Features.Get<IExceptionHandlerFeature>();

            if (exceptionHandler != null)
            {
                await SetResponseAsync(context, exceptionHandler);
            }
        }

        private static async Task SetResponseAsync(HttpContext context, IExceptionHandlerFeature exceptionHandler)
        {
            var messageItems = exceptionHandler.Error.Message.Split(";");

            if (messageItems.Length > 1 && int.TryParse(messageItems[0], out int statusCode))
            {
                context.Response.StatusCode = statusCode;
                await context.Response.WriteAsync(messageItems[1]);
            }
            else
            {
                await context.Response.WriteAsync(messageItems[0]);
            }
        }
    }
}