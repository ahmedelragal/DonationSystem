using Microsoft.Extensions.Caching.Memory;

namespace DonationSystem.Api.Middlewares
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RateLimitingMiddleware> _logger;
        private readonly IMemoryCache _cache;
        private const int LIMIT = 5;
        private readonly TimeSpan PERIOD = TimeSpan.FromMinutes(1);

        public RateLimitingMiddleware(RequestDelegate next, ILogger<RateLimitingMiddleware> logger, IMemoryCache cache)
        {
            _next = next;
            _logger = logger;
            _cache = cache;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var key = $"rl:{context.Connection.RemoteIpAddress}";

            var count = _cache.GetOrCreate<int>(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = PERIOD;
                return 0;
            });

            if (count >= LIMIT)
            {
                _logger.LogWarning("Rate limit exceeded for IP {IP}", context.Connection.RemoteIpAddress);
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync("Rate limit exceeded. Try again later.");
                return;
            }

            _cache.Set(key, count + 1, DateTimeOffset.Now.Add(PERIOD));

            await _next(context);
        }
    }
}
