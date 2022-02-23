namespace FlaPo_Backend_Minimal.Utility;

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
