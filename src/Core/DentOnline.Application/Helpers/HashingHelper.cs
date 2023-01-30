using System.Security.Cryptography;
using System.Text;

namespace DentOnline.Application.Helpers;

public static class HashingHelper
{
    private static byte[] Hash(string data)
    {
        var messageBytes = Encoding.UTF8.GetBytes(data);

        var hashValue = SHA256.HashData(messageBytes);

        return hashValue;
    }

    public static string HashPassword(string password)
    {
        return Convert.ToHexString(Hash(password));
    }

    public static bool VerifyPassword(string password, string hash)
    {
        var hashedPasswordText = HashPassword(password);

        return hashedPasswordText.Equals(hash);
    }
}