// --------------------------------------------------------------------------------------------------------------------
// Filename : Extensions.cs
// Project: SharedLibrary / FlaPo_Backend
// Author : Kristian Schlikow (kristian@schlikow.de)
// Created On : 24.02.2022 00:29
// Last Modified On : 24.02.2022 00:31
// Copyrights : Copyright (c) Kristian Schlikow 2022-2022, All Rights Reserved
// License: License is provided as described within the LICENSE file shipped with the project
// If present, the license takes precedence over the individual notice within this file
// --------------------------------------------------------------------------------------------------------------------

namespace SharedLibrary.Utility;

using System.Web;

public static class Extensions
{
    /// <summary>
    ///     Converts an encoded URL string to an Uri object
    /// </summary>
    /// <param name="url">Encoded URL string</param>
    /// <returns>Input string as Uri</returns>
    public static Uri ToUri(this string url) => new(HttpUtility.UrlDecode(url));
}
