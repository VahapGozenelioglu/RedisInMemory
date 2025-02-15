using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryApp.Web.Controllers;

public class ProductController
{
    private readonly IMemoryCache _memoryCache;
    
    public ProductController(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }
}