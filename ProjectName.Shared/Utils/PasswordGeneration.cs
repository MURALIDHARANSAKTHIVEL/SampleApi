using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace ProjectName.Shared.Utils {

    public static class PasswordGeneration {
        

        private static readonly Dictionary<PasswordCharacters, string> AllowedPasswordCharacters =
            new Dictionary<PasswordCharacters, string> (4) { { PasswordCharacters.LowercaseLetters, @"abcdefghijklmnopqrstuvwxyz" }, { PasswordCharacters.UppercaseLetters, @"ABCDEFGHIJKLMNOPQRSTUVWXYZ" }, { PasswordCharacters.Numbers, @"0123456789" }, { PasswordCharacters.Punctuations, @"!@#$_?" }
            };

         
        /// <summary>
        /// Generates Random Password
        /// </summary>
        /// <param name="length"></param>
        /// <param name="allowedCharacters"></param>
        /// <param name="excludeCharacters"></param>
        /// <returns></returns>
        public static string Generate (int length = 10, PasswordCharacters allowedCharacters = PasswordCharacters.All, IEnumerable<char> excludeCharacters = null) {
            if (length <= 0)
                throw new ArgumentOutOfRangeException ("length", "Password length must be greater than zero");

            var randomBytes = new byte[length];
            var characters = new char[length];

            var randomNumberGenerator = new RNGCryptoServiceProvider (randomBytes);
            randomNumberGenerator.GetBytes (randomBytes);

            string allowedCharactersString = GenerateAllowedCharactersString (allowedCharacters, excludeCharacters);

            for (int i = 0; i < length; i++) {
                var index = randomBytes[i] % allowedCharactersString.Length;
                characters[i] = allowedCharactersString[index];
            }

            string password = new string (characters);
            if ((allowedCharacters == PasswordCharacters.All) &&
                (!Regex.Match (password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{" + length + ",}").Success)) {
                return Generate (length, allowedCharacters, excludeCharacters);
            }
            return password;
        }

       
        /// <summary>
        /// Gets the required characters String
        /// </summary>
        /// <param name="characters"></param>
        /// <param name="excludeCharacters"></param>
        /// <returns></returns>
        private static string GenerateAllowedCharactersString (PasswordCharacters characters, IEnumerable<char> excludeCharacters) {
            var allowedCharactersString = new StringBuilder ();

            foreach (KeyValuePair<PasswordCharacters, string> type in AllowedPasswordCharacters) {
                if ((characters & type.Key) != type.Key)
                    continue;
                if (excludeCharacters == null)
                    allowedCharactersString.Append (type.Value);
                else
                    allowedCharactersString.Append (type.Value.Where (c => !excludeCharacters.Contains (c)).ToArray ());
            }
            return allowedCharactersString.ToString ();
        }

           }

    /// <summary>
    /// Allowed Password Characters to Generate Password
    /// </summary>
    public enum PasswordCharacters {
        LowercaseLetters = 0x01,
        UppercaseLetters = 0x02,
        Numbers = 0x04,
        Punctuations = 0x08,
        Space = 0x10,
        AllLetters = LowercaseLetters | UppercaseLetters,
        AlphaNumeric = AllLetters | Numbers,
        All = AllLetters | Numbers | Punctuations,
    }
}