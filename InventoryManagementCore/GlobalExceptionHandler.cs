using Newtonsoft.Json;

namespace InventoryManagementCore
{

    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var errorDetails = new
                {
                    context.Response.StatusCode,
                    ex.Message,
                };

                var jsonError = JsonConvert.SerializeObject(errorDetails);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(jsonError);
            }
        }

    }

}
