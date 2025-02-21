using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryApp.Web.Controllers;

public class ProductController : Controller
{
    private readonly IMemoryCache _memoryCache;
    
    public ProductController(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public IActionResult Index()
    {
        if (!_memoryCache.TryGetValue("time", out string? cachedTime))
        {
            MemoryCacheEntryOptions options = new()
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(1),
                SlidingExpiration = TimeSpan.FromSeconds(10),
                Priority = CacheItemPriority.High
            };
            _memoryCache.Set("time", DateTime.Now.ToString(CultureInfo.InvariantCulture), options);
        }
        return View();
    }
    

    public IActionResult Show()
    {
        _memoryCache.TryGetValue("time", out string? cachedTime);
        ViewBag.time = cachedTime;
        
        return View();
    }
}