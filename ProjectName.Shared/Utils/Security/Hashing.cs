using System;
using System.Security.Cryptography;
using System.Text;
using ProjectName.Shared.AppSettings;
using Microsoft.Extensions.Options;

namespace ProjectName.Shared.Utils.Security {
    public class Hashing {

        private SecurityConfig _securityConfig;

        public Hashing (IOptions<SecurityConfig> securityConfig) {
            _securityConfig = securityConfig.Value;

        }
        public string GetSha256HashString (string inputString) {

            string value = GetSaltedValue (inputString);

            using (SHA256 sha256 = SHA256.Create ()) {

                byte[] hashedBytes = sha256.ComputeHash (Encoding.UTF8.GetBytes (value));

                return BitConverter.ToString (hashedBytes).Replace ("-", "").ToLower ();
            }

        }

        private string GetSaltedValue (string value) {
            return value + _securityConfig.Salt;
        }

    }
}