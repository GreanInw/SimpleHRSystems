using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using HR.Common.Identities.Security;

namespace HR.Common.Identities
{
    /// <summary>
    /// Password helper.
    /// </summary>
    public class PasswordHelper
    {
        internal const string Symbols = @"(!@#$%^&*()_+|~-=\`[]:"";'<>?,./){}";

        /// <summary>
        /// Minimum length of password
        /// </summary>
        public int MinimumLength { get; }

        /// <summary>
        /// Maximum length of password
        /// </summary>
        public int MaximumLength { get; }

        /// <summary>
        /// Initial class password helper.
        /// </summary>
        /// <param name="minimumLength">Minimum length of password for validate</param>
        /// <param name="maximumLength">Maximum length of password for validate</param>
        public PasswordHelper(int minimumLength, int maximumLength)
        {
            MinimumLength = minimumLength;
            MaximumLength = maximumLength;
        }

        /// <summary>
        /// validate password. Return true valid, false invalid.
        /// </summary>
        /// <param name="password">Plain text</param>
        /// <returns></returns>
        public bool ValidatePassword(string password)
        {
            string pattern = GetRegexPattern(MinimumLength, MaximumLength);
            return Regex.IsMatch(password, pattern);
        }

        /// <summary>
        /// Encrypt password with key
        /// </summary>
        /// <param name="password">Plain text password</param>
        /// <returns></returns>
        public string Encrypt(string password) => Encrypt(password, string.Empty);

        /// <summary>
        /// Encrypt password with key
        /// </summary>
        /// <param name="password">Plain text password</param>
        /// <param name="key">Key with enctypt</param>
        /// <returns></returns>
        public string Encrypt(string password, string key)
        {
            HMACSHA512Cryption cryption = new HMACSHA512Cryption(password, key);
            return cryption.Encrypt();
        }

        /// <summary>
        /// Get pattern for Regex
        /// </summary>
        /// <param name="minimumLength">Minimum length password for validate</param>
        /// <param name="maximumLength">Maximum length password for validate</param>
        /// <returns></returns>
        public static string GetRegexPattern(int minimumLength, int maximumLength)
            => @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{" + $"{minimumLength},{maximumLength}" + "}$";

        /// <summary>
        /// Return message for invalid password pattern
        /// </summary>
        /// <param name="isEncodeSymbol">
        /// True encode with url, false not encode with url. 
        /// If false return is symbols : (!@#$%^&amp;*()_+|~-=\`[]:&quot;&quot;;'&lt;&gt;?,./){}
        /// </param>
        /// <returns></returns>
        public IEnumerable<string> GetInvalidMessage(bool isEncodeSymbol)
            => new List<string>
            {
                $"Password must be {MinimumLength}-{MaximumLength} characters.",
                $"Passwords must contain lowercase letters, lowercase letters and symbols : {(isEncodeSymbol ? WebUtility.UrlEncode(Symbols) : @Symbols)}"
            };

    }
}
