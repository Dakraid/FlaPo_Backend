namespace FlaPo_Backend_Classic.Controllers;

using Microsoft.AspNetCore.Mvc;

using Services;

using SharedLibrary.Models;
using SharedLibrary.Utility;

[ApiController]
[Route("/api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductService _productService;

    public ProductController(ILogger<ProductController> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    [HttpGet("ExtremeItems{url}")]
    public async Task<ActionResult<List<Product>>> GetExtremeItems(string url)
    {
#if MEASURE
    stopwatch.Start();
#endif

        var listings = await Utility.GetListingsFromUrl(url.ToUri());

        if (listings == null || listings.Count == 0)
        {
            return UnprocessableEntity("JSON could not be parsed or returned an empty collection.");
        }

        var result = _productService.ExtremeItems(listings);

#if MEASURE
    StopMeasurement(stopwatch);
#endif

        return Ok(result);
    }

    [HttpGet("ExactPrice{url},{price:double}")]
    public async Task<ActionResult<List<Product>>> GetExactPrice(string url, double price)
    {
#if MEASURE
    stopwatch.Start();
#endif

        var listings = await Utility.GetListingsFromUrl(url.ToUri());

        if (listings == null || listings.Count == 0)
        {
            return UnprocessableEntity("JSON could not be parsed or returned an empty collection.");
        }

        var result = _productService.ExactPrice(listings, price);

#if MEASURE
    StopMeasurement(stopwatch);
#endif

        return Ok(result);
    }

    [HttpGet("MostBottles{url}")]
    public async Task<ActionResult<Product>> GetMostBottles(string url)
    {
#if MEASURE
    stopwatch.Start();
#endif

        var listings = await Utility.GetListingsFromUrl(url.ToUri());

        if (listings == null || listings.Count == 0)
        {
            return UnprocessableEntity("JSON could not be parsed or returned an empty collection.");
        }

        var result = _productService.MostBottles(listings);

#if MEASURE
    StopMeasurement(stopwatch);
#endif

        return Ok(result);
    }

    [HttpGet("GetAll{url},{price:double}")]
    public async Task<ActionResult<CombinedResponse>> GetAll(string url, double price)
    {
#if MEASURE
    stopwatch.Start();
#endif

        var listings = await Utility.GetListingsFromUrl(url.ToUri());

        if (listings == null || listings.Count == 0)
        {
            return UnprocessableEntity("JSON could not be parsed or returned an empty collection.");
        }

        var result = new CombinedResponse
        {
            ExactPrice = _productService.ExactPrice(listings, price),
            ExtremeItems = _productService.ExtremeItems(listings),
            MostBottles = _productService.MostBottles(listings)
        };

#if MEASURE
    StopMeasurement(stopwatch);
#endif

        return Ok(result);
    }
}
