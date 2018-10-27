using System.Security.Cryptography;
using System.Text;

namespace Evernote.Helper
{
    public class MD5
    {
        public static string Calculate(string password)
        {
            //calculate MD5 hash from input
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] hash = md5.ComputeHash(passwordBytes);

            //convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            foreach (byte item in hash)
            {
                sb.Append(item.ToString("x2").ToLower());
            }

            return sb.ToString();
        }
    }
}