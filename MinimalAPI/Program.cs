// --------------------------------------------------------------------------------------------------------------------
// Filename : Program.cs
// Project: FlaPo_Backend_Minimal / FlaPo_Backend_Minimal
// Author : Kristian Schlikow (kristian@schlikow.de)
// Created On : 22.02.2022 17:20
// Last Modified On : 23.02.2022 14:24
// Copyrights : Copyright (c) Kristian Schlikow 2022-2022, All Rights Reserved
// License: License is provided as described within the LICENSE file shipped with the project
// If present, the license takes precedence over the individual notice within this file
// --------------------------------------------------------------------------------------------------------------------

using FlaPo_Backend_Minimal.Model;
using FlaPo_Backend_Minimal.Utility;

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

List<Listing> ExtremeItems(List<Listing> listings)
{
    List<PricePerUnit> pricePerUnits = new();

    foreach (var listing in listings)
    {
        foreach (var article in listing.Articles)
        {
            var pricePerUnit = Utility.GetPricePerUnitFromText(listing.Id, article.Id, article.PricePerUnitText);

            if (pricePerUnit == null)
            {
                continue;
            }

            pricePerUnits.Add(pricePerUnit);
        }
    }

    var highestItemId = pricePerUnits.MaxBy(ppu => ppu.Price)?.ListingId;
    var lowestItemId = pricePerUnits.MinBy(ppu => ppu.Price)?.ListingId;

    return new List<Listing>
    {
        listings.Single(listing => listing.Id == highestItemId), listings.Single(listing => listing.Id == lowestItemId)
    };
}

List<Listing> ExactPrice(List<Listing> listings, double price)
{
    List<Listing> matches = new();
    List<PricePerUnit> pricePerUnits = new();

    foreach (var listing in listings)
    {
        foreach (var article in listing.Articles)
        {
            if (Math.Abs(article.Price - price) < 0.01 && !matches.Contains(listing))
            {
                matches.Add(listing);
            }

            var pricePerUnit = Utility.GetPricePerUnitFromText(listing.Id, article.Id, article.PricePerUnitText);

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

Listing MostBottles(List<Listing> listings)
{
    long highestId = 0, count = 0;

    foreach (var listing in listings)
    {
        foreach (var article in listing.Articles)
        {
            var quantity = Utility.GetQuantityFromShortDesc(listing.Id, article.Id, article.ShortDescription);

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

;

app.MapGet("/api/extremeItems", async (string url) =>
{
    var listings = await Utility.GetListingsFromUrl(url.ToUri());

    if (listings == null || listings.Count == 0)
    {
        return Results.UnprocessableEntity("JSON could not be parsed or returned an empty collection.");
    }

    return Results.Ok(ExtremeItems(listings));
});

app.MapGet("/api/exactPrice", async (string url, double price) =>
{
    var listings = await Utility.GetListingsFromUrl(url.ToUri());

    if (listings == null || listings.Count == 0)
    {
        return Results.UnprocessableEntity("JSON could not be parsed or returned an empty collection.");
    }

    return Results.Ok(ExactPrice(listings, price));
});

app.MapGet("/api/mostBottles", async (string url) =>
{
    var listings = await Utility.GetListingsFromUrl(url.ToUri());

    if (listings == null || listings.Count == 0)
    {
        return Results.UnprocessableEntity("JSON could not be parsed or returned an empty collection.");
    }

    return Results.Ok(MostBottles(listings));
});

app.MapGet("/api/all", async (string url, double price) =>
{
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

    return Results.Ok(result);
});

app.Run();
