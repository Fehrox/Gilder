using System.Security.Cryptography;
using System.Text;

namespace Reconciler.Domain
{
    public class Hash
    {
        private readonly string _innerStr;

        internal Hash(string innerStr) => _innerStr = innerStr;

        public override string ToString()
        {
            using HashAlgorithm algorithm = SHA256.Create();
            var buffer = Encoding.UTF8.GetBytes(_innerStr);
            byte[] hashBytes = algorithm.ComputeHash(buffer);
                
            var sb = new StringBuilder();
            foreach (byte b in hashBytes)
                sb.Append(b.ToString("X2"));

            var hashStr = sb.ToString();
            return hashStr;
        }
    }
}