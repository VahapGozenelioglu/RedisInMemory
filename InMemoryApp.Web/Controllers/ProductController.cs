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
                AbsoluteExpiration = DateTime.Now.AddSeconds(10),
                Priority = CacheItemPriority.High,
                PostEvictionCallbacks =
                {
                    new PostEvictionCallbackRegistration()
                    {
                        EvictionCallback = (key, value, reason, state) =>
                        {
                            _memoryCache.Set("callback", $"{key} with value : {value} is deleted. Reason: {reason}");
                        }
                    }
                }
            };
            _memoryCache.Set("time", DateTime.Now.ToString(CultureInfo.InvariantCulture), options);
        }
        return View();
    }
    

    public IActionResult Show()
    {
        _memoryCache.TryGetValue("time", out string? cachedTime);
        _memoryCache.TryGetValue("callback", out string? callback);
        ViewBag.time = cachedTime;
        ViewBag.callback = callback;
        
        return View();
    }
}