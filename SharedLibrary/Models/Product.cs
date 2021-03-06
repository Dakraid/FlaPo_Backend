// --------------------------------------------------------------------------------------------------------------------
// Filename : Product.cs
// Project: SharedLibrary / FlaPo_Backend
// Author : Kristian Schlikow (kristian@schlikow.de)
// Created On : 24.02.2022 00:29
// Last Modified On : 24.02.2022 00:31
// Copyrights : Copyright (c) Kristian Schlikow 2022-2022, All Rights Reserved
// License: License is provided as described within the LICENSE file shipped with the project
// If present, the license takes precedence over the individual notice within this file
// --------------------------------------------------------------------------------------------------------------------

namespace SharedLibrary.Models;

using Newtonsoft.Json;

public class Product
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("brandName")]
    public string BrandName { get; set; } = null!;

    [JsonProperty("name")]
    public string Name { get; set; } = null!;

    [JsonProperty("articles")]
    public List<Article> Articles { get; set; } = null!;

    [JsonProperty("descriptionText")]
    public string? DescriptionText { get; set; }
}

public class Article
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("shortDescription")]
    public string ShortDescription { get; set; } = null!;

    [JsonProperty("price")]
    public double Price { get; set; }

    [JsonProperty("unit")]
    public string Unit { get; set; } = null!;

    [JsonProperty("pricePerUnitText")]
    public string PricePerUnitText { get; set; } = null!;

    [JsonProperty("image")]
    public Uri Image { get; set; } = null!;
}
