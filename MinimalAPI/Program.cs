// --------------------------------------------------------------------------------------------------------------------
// Filename : Program.cs
// Project: FlaPo_Backend_Minimal / FlaPo_Backend
// Author : Kristian Schlikow (kristian@schlikow.de)
// Created On : 22.02.2022 17:20
// Last Modified On : 23.02.2022 14:35
// Copyrights : Copyright (c) Kristian Schlikow 2022-2022, All Rights Reserved
// License: License is provided as described within the LICENSE file shipped with the project
// If present, the license takes precedence over the individual notice within this file
// --------------------------------------------------------------------------------------------------------------------

using FlaPo_Backend_Minimal.Model;
using FlaPo_Backend_Minimal.Utility;

#if MEASURE
using System.Diagnostics;
#endif

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var utility = new Utility();

#if MEASURE
var stopwatch = new Stopwatch();
#endif

List<Product> ExtremeItems(List<Product> listings)
{
    List<PricePerUnit> pricePerUnits = new();

    foreach (var listing in listings)
    {
        foreach (var article in listing.Articles)
        {
            var pricePerUnit = utility.GetPricePerUnitFromText(listing.Id, article.Id, article.PricePerUnitText);

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

List<Product> ExactPrice(List<Product> listings, double price)
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

            var pricePerUnit = utility.GetPricePerUnitFromText(listing.Id, article.Id, article.PricePerUnitText);

            if (pricePerUnit == null)
            {
                continue;
            }

            pricePerUnits.Add(pricePerUnit);
        }
    }

    // TODO: Order by PPU

    return matches;
}

Product MostBottles(List<Product> listings)
{
    long highestId = 0, count = 0;

    foreach (var listing in listings)
    {
        foreach (var article in listing.Articles)
        {
            var quantity = utility.GetQuantityFromShortDesc(listing.Id, article.Id, article.ShortDescription);

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
void StopMeasurement(Stopwatch sw)
{
    sw.Stop();
    Console.WriteLine("Elapsed Ticks for Call: " + sw.ElapsedTicks);
    sw.Reset();
}
#endif

app.MapGet("/api/extremeItems", async (string url) =>
{
#if MEASURE
    stopwatch.Start();
#endif

    var listings = await Utility.GetListingsFromUrl(url.ToUri());

    if (listings == null || listings.Count == 0)
    {
        return Results.UnprocessableEntity("JSON could not be parsed or returned an empty collection.");
    }

    var result = ExtremeItems(listings);

#if MEASURE
    StopMeasurement(stopwatch);
#endif

    return Results.Ok(result);
});

app.MapGet("/api/exactPrice", async (string url, double price) =>
{
#if MEASURE
    stopwatch.Start();
#endif

    var listings = await Utility.GetListingsFromUrl(url.ToUri());

    if (listings == null || listings.Count == 0)
    {
        return Results.UnprocessableEntity("JSON could not be parsed or returned an empty collection.");
    }

    var result = ExactPrice(listings, price);

#if MEASURE
    StopMeasurement(stopwatch);
#endif

    return Results.Ok(result);
});

app.MapGet("/api/mostBottles", async (string url) =>
{
#if MEASURE
    stopwatch.Start();
#endif

    var listings = await Utility.GetListingsFromUrl(url.ToUri());

    if (listings == null || listings.Count == 0)
    {
        return Results.UnprocessableEntity("JSON could not be parsed or returned an empty collection.");
    }

    var result = MostBottles(listings);

#if MEASURE
    StopMeasurement(stopwatch);
#endif

    return Results.Ok(result);
});

app.MapGet("/api/all", async (string url, double price) =>
{
#if MEASURE
    stopwatch.Start();
#endif

    var listings = await Utility.GetListingsFromUrl(url.ToUri());

    if (listings == null || listings.Count == 0)
    {
        return Results.UnprocessableEntity("JSON could not be parsed or returned an empty collection.");
    }

    var result = new CombinedResponse
    {
        ExactPrice = ExactPrice(listings, price),
        ExtremeItems = ExtremeItems(listings),
        MostBottles = MostBottles(listings)
    };

#if MEASURE
    StopMeasurement(stopwatch);
#endif

    return Results.Ok(result);
});

app.Run();
