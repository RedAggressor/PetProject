namespace Basket.Host.Services.Abstractions
{
    public interface ICookieService
    {
        Task<string> GetUniqueIdFromCookieAsync(HttpContext httpContext);
    }
}
