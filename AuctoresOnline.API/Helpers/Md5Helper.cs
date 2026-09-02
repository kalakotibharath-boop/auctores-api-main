using System.Security.Cryptography;
using System.Text;

namespace AuctoresOnline.API.Helpers;

public static class Md5Helper
{
    public static string Hash(string input)
    {
        var bytes = MD5.HashData(Encoding.UTF8.GetBytes(input));
        return Convert.ToHexString(bytes).ToLower();
    }

    public static bool Verify(string input, string hash) =>
        Hash(input).Equals(hash, StringComparison.OrdinalIgnoreCase);
}
