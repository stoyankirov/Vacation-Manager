namespace VacationManager.Core.Utility
{
    using Microsoft.AspNetCore.Cryptography.KeyDerivation;
    using System;
    using System.Text;

    public static class PasswordHasher
    {
        public static string Hash(string password, out string passwordSalt)
        {
            passwordSalt = Guid.NewGuid().ToString("N");

            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password + passwordSalt,
                salt: Encoding.ASCII.GetBytes(passwordSalt),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hash;
        }
    }
}
