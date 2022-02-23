// --------------------------------------------------------------------------------------------------------------------
// Filename : Quantity.cs
// Project: FlaPo_Backend_Minimal / FlaPo_Backend_Minimal
// Author : Kristian Schlikow (kristian@schlikow.de)
// Created On : 22.02.2022 20:10
// Last Modified On : 23.02.2022 14:23
// Copyrights : Copyright (c) Kristian Schlikow 2022-2022, All Rights Reserved
// License: License is provided as described within the LICENSE file shipped with the project
// If present, the license takes precedence over the individual notice within this file
// --------------------------------------------------------------------------------------------------------------------

namespace FlaPo_Backend_Minimal.Model;

public class Quantity
{
    public long ListingId { get; set; }
    public long ArticleId { get; set; }
    public int Count { get; set; }
    public float Volume { get; set; }
    public string Unit { get; set; }
    public string Type { get; set; } = null!;
}
