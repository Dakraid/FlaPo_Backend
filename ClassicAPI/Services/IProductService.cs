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
    /// <summary>
    /// Extracts the Products with the highest and lowest price point per unit
    /// </summary>
    /// <param name="listings">A list of Products</param>
    /// <returns>List of Products with the highest and lowest price</returns>
    List<Product> ExtremeItems(List<Product> listings);

    /// <summary>
    /// Returns all products matching the exact price given
    /// </summary>
    /// <param name="listings">A list of Products</param>
    /// <param name="price">The price to be checked against</param>
    /// <returns>List of Products matching the asked price</returns>
    List<Product> ExactPrice(List<Product> listings, double price);

    /// <summary>
    /// Gives a single product with the most bottles offered
    /// </summary>
    /// <param name="listings">A list of Products</param>
    /// <returns>Single Product with the most bottles</returns>
    Product MostBottles(List<Product> listings);
}
