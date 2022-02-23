// --------------------------------------------------------------------------------------------------------------------
// Filename : PricePerUnit.cs
// Project: FlaPo_Backend_Minimal / FlaPo_Backend
// Author : Kristian Schlikow (kristian@schlikow.de)
// Created On : 22.02.2022 22:13
// Last Modified On : 23.02.2022 14:35
// Copyrights : Copyright (c) Kristian Schlikow 2022-2022, All Rights Reserved
// License: License is provided as described within the LICENSE file shipped with the project
// If present, the license takes precedence over the individual notice within this file
// --------------------------------------------------------------------------------------------------------------------

namespace FlaPo_Backend_Minimal.Model;

public class PricePerUnit
{
    public long ListingId { get; set; }
    public long ArticleId { get; set; }
    public double Price { get; set; }
    public string Currency { get; set; } = null!;
    public string Unit { get; set; } = null!;
}
