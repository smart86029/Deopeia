using System.Security.Cryptography;

namespace Deopeia.Common.Extensions;

public static class CryptographyExtensions
{
    private const int IVLength = 16;

    public static string AesEncrypt(this string plaintext, string key)
    {
        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(key ?? string.Empty);
        aes.GenerateIV();

        using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        var bytes = Encoding.UTF8.GetBytes(plaintext ?? string.Empty);
        var computed = encryptor.TransformFinalBlock(bytes, 0, bytes.Length);

        var ciphertext = new byte[computed.Length + aes.IV.Length];
        Buffer.BlockCopy(computed, 0, ciphertext, 0, computed.Length);
        Buffer.BlockCopy(aes.IV, 0, ciphertext, computed.Length, aes.IV.Length);

        return ciphertext.ToHexString();
    }

    public static string AesDecrypt(this string ciphertext, string key)
    {
        var bytes = ciphertext.FromHexString();

        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(key ?? string.Empty);
        aes.IV = bytes[..IVLength];

        using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        var buffer = bytes[IVLength..];
        var plainText = decryptor.TransformFinalBlock(buffer, 0, buffer.Length);

        return Encoding.UTF8.GetString(plainText);
    }

    public static byte[] Sha256(this byte[] input)
    {
        return SHA256.HashData(input);
    }

    public static string Sha256(this string input)
    {
        var bytes = Encoding.UTF8.GetBytes(input);

        return SHA256.HashData(bytes).ToHexString();
    }
}
