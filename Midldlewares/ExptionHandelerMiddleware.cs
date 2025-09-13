using System.Net;

namespace NZwalks.API.Midldlewares
{
    public class ExptionHandelerMiddleware
    {
        private readonly ILogger<ExptionHandelerMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExptionHandelerMiddleware(ILogger<ExptionHandelerMiddleware> logger, RequestDelegate next)
        {
            this._logger = logger;
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var ErrorId = Guid.NewGuid();

                // log this expection 
                _logger.LogError(ex, $"{ErrorId}  : {ex.Message}");

                // Return a cusotom error massege 
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var Error = new
                {
                    Id = ErrorId,
                    ErroreMassege = "Somthing went Wrong!! we are loking into resolving it"
                };

                await context.Response.WriteAsJsonAsync(Error);
            }


        }
    }
}
