using System.Security.Cryptography;
using System.Text;

namespace Deopeia.Common.Extensions;

public static class HashExtensions
{
    public static byte[] Sha256(this byte[] input)
    {
        return SHA256.HashData(input);
    }

    public static string Sha256(this string input)
    {
        var bytes = Encoding.UTF8.GetBytes(input);

        return SHA256.HashData(bytes).ToBase64String();
    }
}
