using Basket.Host.Services.Abstractions;

namespace Basket.Host.Services
{
    public class CookieService : ICookieService
    {
        public async Task<string> GetUniqueIdFromCookieAsync(HttpContext httpContext)
        {
            return await Task.FromResult(httpContext.Connection.Id!);            
        }
    }
}
