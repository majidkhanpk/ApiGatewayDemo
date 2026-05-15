namespace OrderService.Middleware
{
    public class CorrelationIdReaderMiddleware
    {
        private readonly RequestDelegate _next;
        private const string HeaderKey = "X-Correlation-ID";

        public CorrelationIdReaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var correlationId = context.Request.Headers[HeaderKey].ToString();
            Console.WriteLine($"[{correlationId}] Getting orders...");
            if (string.IsNullOrEmpty(correlationId))
            {
                correlationId = "NO-CORRELATION-ID";
            }

            context.Items["CorrelationId"] = correlationId;

            await _next(context);
        }
    }
}
