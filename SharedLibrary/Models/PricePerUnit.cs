// --------------------------------------------------------------------------------------------------------------------
// Filename : PricePerUnit.cs
// Project: SharedLibrary / FlaPo_Backend
// Author : Kristian Schlikow (kristian@schlikow.de)
// Created On : 24.02.2022 00:29
// Last Modified On : 24.02.2022 00:31
// Copyrights : Copyright (c) Kristian Schlikow 2022-2022, All Rights Reserved
// License: License is provided as described within the LICENSE file shipped with the project
// If present, the license takes precedence over the individual notice within this file
// --------------------------------------------------------------------------------------------------------------------

namespace SharedLibrary.Models;

public class PricePerUnit
{
    public long ListingId { get; set; }
    public long ArticleId { get; set; }
    public double Price { get; set; }
    public string Currency { get; set; } = null!;
    public string Unit { get; set; } = null!;
}
