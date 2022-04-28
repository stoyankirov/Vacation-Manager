namespace VacationManager.Core.Utility
{
    using System;
    using Microsoft.AspNetCore.Cryptography.KeyDerivation;
    using System.Text;

    public static class PasswordHasher
    {
        public static string Hash(string password, string passwordSalt)
        {
            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password + passwordSalt,
                salt: Encoding.ASCII.GetBytes(passwordSalt),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hash;
        }

        public static string GeneratePasswordSalt()
        {
            var passwordSalt = Guid.NewGuid().ToString("N");

            return passwordSalt;
        }
    }
}
