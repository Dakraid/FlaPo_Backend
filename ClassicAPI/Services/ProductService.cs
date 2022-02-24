// --------------------------------------------------------------------------------------------------------------------
// Filename : ProductService.cs
// Project: FlaPo_Backend_Classic / FlaPo_Backend
// Author : Kristian Schlikow (kristian@schlikow.de)
// Created On : 24.02.2022 00:40
// Last Modified On : 24.02.2022 00:46
// Copyrights : Copyright (c) Kristian Schlikow 2022-2022, All Rights Reserved
// License: License is provided as described within the LICENSE file shipped with the project
// If present, the license takes precedence over the individual notice within this file
// --------------------------------------------------------------------------------------------------------------------

namespace FlaPo_Backend_Classic.Services;

using SharedLibrary.Models;
using SharedLibrary.Utility;

public class ProductService : IProductService
{
    private readonly ILogger<ProductService> _logger;
    private readonly Utility _utility;

    public ProductService(ILogger<ProductService> logger)
    {
        _logger = logger;
        _utility = new Utility();
    }

    public List<Product> ExtremeItems(List<Product> listings)
    {
        List<PricePerUnit> pricePerUnits = new();

        foreach (var listing in listings)
        {
            foreach (var article in listing.Articles)
            {
                var pricePerUnit = _utility.GetPricePerUnitFromText(listing.Id, article.Id, article.PricePerUnitText);

                if (pricePerUnit == null)
                {
                    continue;
                }

                pricePerUnits.Add(pricePerUnit);
            }
        }

        var highestItemId = pricePerUnits.MaxBy(ppu => ppu.Price)?.ListingId;
        var lowestItemId = pricePerUnits.MinBy(ppu => ppu.Price)?.ListingId;

        return new List<Product>
        {
            listings.Single(listing => listing.Id == highestItemId), listings.Single(listing => listing.Id == lowestItemId)
        };
    }

    public List<Product> ExactPrice(List<Product> listings, double price)
    {
        List<Product> matches = new();
        List<PricePerUnit> pricePerUnits = new();

        foreach (var listing in listings)
        {
            foreach (var article in listing.Articles)
            {
                if (Math.Abs(article.Price - price) < 0.01 && !matches.Contains(listing))
                {
                    matches.Add(listing);
                }

                var pricePerUnit = _utility.GetPricePerUnitFromText(listing.Id, article.Id, article.PricePerUnitText);

                if (pricePerUnit == null)
                {
                    continue;
                }

                pricePerUnits.Add(pricePerUnit);
            }
        }

        var order = pricePerUnits.OrderBy(ppu => ppu.Price).Select(ppu => ppu.ListingId).ToList();

        return matches.OrderBy(product => order.IndexOf(product.Id)).ToList();
    }

    public Product MostBottles(List<Product> listings)
    {
        long highestId = 0, count = 0;

        foreach (var listing in listings)
        {
            foreach (var article in listing.Articles)
            {
                var quantity = _utility.GetQuantityFromShortDesc(listing.Id, article.Id, article.ShortDescription);

                if (quantity == null || quantity.Count <= count)
                {
                    continue;
                }

                count = quantity.Count;
                highestId = listing.Id;
            }
        }

        return listings.Single(listing => listing.Id == highestId);
    }

#if MEASURE
public void StopMeasurement(Stopwatch sw)
{
    sw.Stop();
    Console.WriteLine("Elapsed Ticks for Call: " + sw.ElapsedTicks);
    sw.Reset();
}
#endif
}
