namespace ApiGateway
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        private const string HeaderKey = "X-Correlation-ID";

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // 1. Check if request already has Correlation ID
            if (!context.Request.Headers.TryGetValue(HeaderKey, out var correlationId))
            {
                correlationId = Guid.NewGuid().ToString();
                context.Request.Headers[HeaderKey] = correlationId;
            }

            // 2. Add it to response too
            context.Response.Headers[HeaderKey] = correlationId;

            // 3. Store it for downstream access
            context.Items["CorrelationId"] = correlationId.ToString();

            await _next(context);
        }
    }
}