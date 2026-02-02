using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

public static class PasswordHasher
{
    public static string Hash(string senha)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);

        var hash = Convert.ToBase64String(
            KeyDerivation.Pbkdf2(
                password: senha,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32));

        return $"{Convert.ToBase64String(salt)}.{hash}";
    }

    public static bool Verify(string senhaDigitada, string senhaHash)
    {
        var parts = senhaHash.Split('.');
        var salt = Convert.FromBase64String(parts[0]);
        var hashOriginal = parts[1];

        var hashTeste = Convert.ToBase64String(
            KeyDerivation.Pbkdf2(
                password: senhaDigitada,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32));

        return hashTeste == hashOriginal;
    }
}
