// --------------------------------------------------------------------------------------------------------------------
// Filename : Utility.cs
// Project: FlaPo_Backend_Minimal / FlaPo_Backend
// Author : Kristian Schlikow (kristian@schlikow.de)
// Created On : 22.02.2022 19:47
// Last Modified On : 23.02.2022 14:35
// Copyrights : Copyright (c) Kristian Schlikow 2022-2022, All Rights Reserved
// License: License is provided as described within the LICENSE file shipped with the project
// If present, the license takes precedence over the individual notice within this file
// --------------------------------------------------------------------------------------------------------------------

namespace FlaPo_Backend_Minimal.Utility;

using Model;

using Newtonsoft.Json;

using System.Text.RegularExpressions;

public class Utility
{
    private readonly Regex _quantityRegex;
    private readonly Regex _pricePerUnitRegex;

    public Utility()
    {
        _quantityRegex = new Regex(@"(\d*)\s*x\s*(\d*,\d*)(\w*)\s\((\w*)\)",
            RegexOptions.Compiled | RegexOptions.IgnoreCase, TimeSpan.FromSeconds(60));

        _pricePerUnitRegex = new Regex(@"\((\d*,\d*)\s*(€)\/(\w*)\)",
            RegexOptions.Compiled | RegexOptions.IgnoreCase, TimeSpan.FromSeconds(60));
    }

    /// <summary>
    ///     Downloads and parses an incoming JSON from an URL into a List of Listings
    /// </summary>
    /// <param name="uri">The JSON Uri</param>
    /// <returns>Collection of Product objects</returns>
    public static async Task<List<Product>?> GetListingsFromUrl(Uri uri)
    {
        using var httpClient = new HttpClient();
        var jsonString = await httpClient.GetStringAsync(uri);

        return JsonConvert.DeserializeObject<List<Product>>(jsonString);
    }

    /// <summary>
    ///     Takes in the ShortDescription and converts it into a Quantity object with fields
    /// </summary>
    /// <param name="listingId">The Id of the Product</param>
    /// <param name="articleId">The Id of the Article</param>
    /// <param name="shortDescription">The ShortDescription</param>
    /// <returns>On Success: A new filled Quantity object</returns>
    /// <remarks>Note: Returns null on failure</remarks>
    public Quantity? GetQuantityFromShortDesc(long listingId, long articleId, string shortDescription)
    {
        var match = _quantityRegex.Match(shortDescription);

        if (match.Success)
        {
            return new Quantity
            {
                ListingId = listingId,
                ArticleId = articleId,
                Count = int.Parse(match.Groups[1].Value),
                Volume = float.Parse(match.Groups[2].Value),
                Unit = match.Groups[3].Value,
                Type = match.Groups[4].Value
            };
        }

        return null;
    }

    /// <summary>
    ///     Takes in the PricePerUnitText and converts it into a Quantity object with fields
    /// </summary>
    /// <param name="listingId">The Id of the Product</param>
    /// <param name="articleId">The Id of the Article</param>
    /// <param name="pricePerUnitText">The PricePerUnitText</param>
    /// <returns>A new filled PricePerUnit object</returns>
    /// <remarks>Note: Returns null on failure</remarks>
    public PricePerUnit? GetPricePerUnitFromText(long listingId, long articleId, string pricePerUnitText)
    {
        var match = _pricePerUnitRegex.Match(pricePerUnitText);

        if (match.Success)
        {
            return new PricePerUnit
            {
                ListingId = listingId,
                ArticleId = articleId,
                Price = float.Parse(match.Groups[1].Value),
                Currency = match.Groups[2].Value,
                Unit = match.Groups[3].Value
            };
        }

        return null;
    }
}
