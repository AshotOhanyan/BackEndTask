using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.OtherServices
{
    public static class PasswordHelper
    {
        public static (string PasswordHash, string Salt) GeneratePasswordHash(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            string hash = BCrypt.Net.BCrypt.HashPassword(password, salt);
            return (hash, salt);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
