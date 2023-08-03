using System.Security.Cryptography;
using System.Text;

namespace HR.Common.Identities.Security
{
    /// <summary>
    /// HMACSHA512 Cryption
    /// </summary>
    public class HMACSHA512Cryption
    {
        private readonly string _plainText;
        private readonly byte[] _keys;

        /// <summary>
        /// Initial HMACSHA512 Cryption
        /// </summary>
        /// <param name="plainText">Plain text</param>
        public HMACSHA512Cryption(string plainText) : this(plainText, string.Empty)
        { }

        /// <summary>
        /// Initial HMACSHA512 Cryption with key
        /// </summary>
        /// <param name="plainText">Plain text</param>
        /// <param name="key">Key for encrypt</param>
        public HMACSHA512Cryption(string plainText, string key)
        {
            if (string.IsNullOrWhiteSpace(plainText))
            {
                throw new ArgumentNullException(nameof(plainText));
            }

            _plainText = plainText;
            _keys = string.IsNullOrWhiteSpace(key) ? new byte[0] : Encoding.ASCII.GetBytes(key);
        }

        /// <summary>
        /// Encrypt data
        /// </summary>
        /// <returns></returns>
        public string Encrypt()
        {
            HMACSHA512 hmacSHA512 = _keys.Length == 0 ? new HMACSHA512() : new HMACSHA512(_keys);
            try
            {
                var data = Encoding.ASCII.GetBytes(_plainText);
                var encryptBytes = hmacSHA512.ComputeHash(data);
                return Convert.ToBase64String(encryptBytes);
            }
            finally
            {
                hmacSHA512.Dispose();
            }
        }
    }
}
