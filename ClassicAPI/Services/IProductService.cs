// --------------------------------------------------------------------------------------------------------------------
// Filename : IProductService.cs
// Project: FlaPo_Backend_Classic / FlaPo_Backend
// Author : Kristian Schlikow (kristian@schlikow.de)
// Created On : 24.02.2022 00:54
// Last Modified On : 24.02.2022 00:54
// Copyrights : Copyright (c) Kristian Schlikow 2022-2022, All Rights Reserved
// License: License is provided as described within the LICENSE file shipped with the project
// If present, the license takes precedence over the individual notice within this file
// --------------------------------------------------------------------------------------------------------------------

namespace FlaPo_Backend_Classic.Services;

using SharedLibrary.Models;

public interface IProductService
{
    List<Product> ExtremeItems(List<Product> listings);
    List<Product> ExactPrice(List<Product> listings, double price);
    Product MostBottles(List<Product> listings);
}
