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
            _memoryCache.Set("time", DateTime.Now.ToString(CultureInfo.InvariantCulture));
        }
        return View();
    }
    

    public IActionResult Show()
    {
        _memoryCache.GetOrCreate<string>("time", _ =>
        {
            _.Priority = CacheItemPriority.High;
            return DateTime.Now.ToString(CultureInfo.InvariantCulture);
        });
        
        ViewBag.time = _memoryCache.Get<string>("time");
        return View();
    }
}