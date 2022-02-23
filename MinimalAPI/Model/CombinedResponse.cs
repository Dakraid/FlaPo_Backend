// --------------------------------------------------------------------------------------------------------------------
// Filename : CombinedResponse.cs
// Project: FlaPo_Backend_Minimal / FlaPo_Backend_Minimal
// Author : Kristian Schlikow (kristian@schlikow.de)
// Created On : 23.02.2022 14:09
// Last Modified On : 23.02.2022 14:23
// Copyrights : Copyright (c) Kristian Schlikow 2022-2022, All Rights Reserved
// License: License is provided as described within the LICENSE file shipped with the project
// If present, the license takes precedence over the individual notice within this file
// --------------------------------------------------------------------------------------------------------------------

namespace FlaPo_Backend_Minimal.Model;

using Newtonsoft.Json;

public class CombinedResponse
{
    [JsonProperty("extremeItems")]
    public List<Listing> ExtremeItems { get; set; }

    [JsonProperty("exactPrice")]
    public List<Listing> ExactPrice { get; set; }

    [JsonProperty("mostBottles")]
    public Listing MostBottles { get; set; }
}
