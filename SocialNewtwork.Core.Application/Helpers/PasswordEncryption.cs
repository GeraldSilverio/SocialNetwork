using System.Security.Cryptography;
using System.Text;

namespace SocialNewtwork.Core.Application.Helpers
{
    public class PasswordEncryption
    {
        public static string ComputeSha256Hash(string password)
        {
            //Create a SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                //Convert Byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
