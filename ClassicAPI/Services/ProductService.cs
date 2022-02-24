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

    /// <summary>
    /// Extracts the Products with the highest and lowest price point per unit
    /// </summary>
    /// <param name="listings">A list of Products</param>
    /// <returns>List of Products with the highest and lowest price</returns>
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

    /// <summary>
    /// Returns all products matching the exact price given
    /// </summary>
    /// <param name="listings">A list of Products</param>
    /// <param name="price">The price to be checked against</param>
    /// <returns>List of Products matching the asked price</returns>
    public List<Product> ExactPrice(List<Product> listings, double price)
    {
        List<Product> matches = new();
        List<PricePerUnit> pricePerUnits = new();

        foreach (var listing in listings)
        {
            foreach (var article in listing.Articles)
            {
                // Instead of using == to check for equality, we subtract each other and check against a threshold
                // This avoids failed checks due to floating point inaccuracy
                // See https://www.jetbrains.com/help/resharper/CompareOfFloatsByEqualityOperator.html for more information
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

    /// <summary>
    /// Gives a single product with the most bottles offered
    /// </summary>
    /// <param name="listings">A list of Products</param>
    /// <returns>Single Product with the most bottles</returns>
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
/// <summary>
/// Stops the passed in Stopwatch, prints out the elapsed ticks, and resets the Stopwatch
/// </summary>
/// <param name="sw">The Stopwatch</param>
public void StopMeasurement(Stopwatch sw)
{
    sw.Stop();
    Console.WriteLine("Elapsed Ticks for Call: " + sw.ElapsedTicks);
    sw.Reset();
}
#endif
}
