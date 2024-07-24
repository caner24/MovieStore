using Microsoft.AspNetCore.Diagnostics;

namespace MovieStore.Extensions
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not null)
            {

                var statusCodes = exception switch
                {
                    _ => StatusCodes.Status500InternalServerError,
                };
                httpContext.Response.StatusCode = statusCodes;

                var error = new
                {
                    StatusCode = httpContext.Response.StatusCode,
                    ErrorMessage = exception.Message
                };
                await httpContext.Response
                    .WriteAsJsonAsync(error, cancellationToken);
                return true;
            }
            return false;
        }
    }
}
