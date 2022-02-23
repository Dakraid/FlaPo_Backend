// --------------------------------------------------------------------------------------------------------------------
// Filename : CombinedResponse.cs
// Project: FlaPo_Backend_Minimal / FlaPo_Backend
// Author : Kristian Schlikow (kristian@schlikow.de)
// Created On : 23.02.2022 14:09
// Last Modified On : 23.02.2022 14:35
// Copyrights : Copyright (c) Kristian Schlikow 2022-2022, All Rights Reserved
// License: License is provided as described within the LICENSE file shipped with the project
// If present, the license takes precedence over the individual notice within this file
// --------------------------------------------------------------------------------------------------------------------

namespace FlaPo_Backend_Minimal.Model;

using Newtonsoft.Json;

public class CombinedResponse
{
    [JsonProperty("extremeItems")]
    public List<Product>? ExtremeItems { get; set; }

    [JsonProperty("exactPrice")]
    public List<Product>? ExactPrice { get; set; }

    [JsonProperty("mostBottles")]
    public Product? MostBottles { get; set; }
}
