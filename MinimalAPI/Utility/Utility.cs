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
using System.Web;

public static class Utility
{
    private const string QuantityPattern = @"(\d*)\s*x\s*(\d*,\d*)(\w*)\s\((\w*)\)";
    private const string PricePerUnitPattern = @"\((\d*,\d*)\s*(€)\/(\w*)\)";

    /// <summary>
    ///     Converts an encoded URL string to an Uri object
    /// </summary>
    /// <param name="url">Encoded URL string</param>
    /// <returns>Input string as Uri</returns>
    public static Uri ToUri(this string url) => new(HttpUtility.UrlDecode(url));

    /// <summary>
    ///     Downloads and parses an incoming JSON from an URL into a List of Listings
    /// </summary>
    /// <param name="uri">The JSON Uri</param>
    /// <returns>Collection of Listing objects</returns>
    public static async Task<List<Listing>?> GetListingsFromUrl(Uri uri)
    {
        using var httpClient = new HttpClient();
        var       jsonString = await httpClient.GetStringAsync(uri);

        return JsonConvert.DeserializeObject<List<Listing>>(jsonString);
    }

    /// <summary>
    ///     Takes in the ShortDescription and converts it into a Quantity object with fields
    /// </summary>
    /// <param name="listingId">The Id of the Listing</param>
    /// <param name="articleId">The Id of the Article</param>
    /// <param name="shortDescription">The ShortDescription</param>
    /// <returns>On Success: A new filled Quantity object</returns>
    /// <remarks>Note: Returns null on failure</remarks>
    public static Quantity? GetQuantityFromShortDesc(long listingId, long articleId, string shortDescription)
    {
        var match = Regex.Match(shortDescription, QuantityPattern, RegexOptions.IgnoreCase);

        if (match.Success)
        {
            return new Quantity
            {
                ListingId = listingId,
                ArticleId = articleId,
                Count     = int.Parse(match.Groups[1].Value),
                Volume    = float.Parse(match.Groups[2].Value),
                Unit      = match.Groups[3].Value,
                Type      = match.Groups[4].Value
            };
        }

        return null;
    }

    /// <summary>
    ///     Takes in the PricePerUnitText and converts it into a Quantity object with fields
    /// </summary>
    /// <param name="listingId">The Id of the Listing</param>
    /// <param name="articleId">The Id of the Article</param>
    /// <param name="pricePerUnitText">The PricePerUnitText</param>
    /// <returns>A new filled PricePerUnit object</returns>
    /// <remarks>Note: Returns null on failure</remarks>
    public static PricePerUnit? GetPricePerUnitFromText(long listingId, long articleId, string pricePerUnitText)
    {
        var match = Regex.Match(pricePerUnitText, PricePerUnitPattern, RegexOptions.IgnoreCase);

        if (match.Success)
        {
            return new PricePerUnit
            {
                ListingId = listingId,
                ArticleId = articleId,
                Price     = float.Parse(match.Groups[1].Value),
                Currency  = match.Groups[2].Value,
                Unit      = match.Groups[3].Value
            };
        }

        return null;
    }
}
