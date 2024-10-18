using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace multronfileguardian
{
    class raes
    {
        public static byte[] rsadecrypt(byte[] veri, string xmlkey, int keysize)
        {
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider(keysize);
            rsa.FromXmlString(xmlkey);
            return rsa.Decrypt(veri, true);
        }
        public static byte[] rsaencrypt(byte[] veri, string xmlkey, int keysize)
        {
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider(keysize);
            rsa.FromXmlString(xmlkey);
            return rsa.Encrypt(veri, true);
        }
        public static string rsageneratekey(int keysize)
        {
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider(keysize);
            return rsa.ToXmlString(false) + "#" + rsa.ToXmlString(true);
        }
        public static byte[] generaterandomaeskey(int keysize)
        {
            string asciichrs = "qwertyuiopasdfghjklzxcvbnm1234567890";
            string randomkey = "";
            Random rndm = new Random();
            for (int i = 0; i <= keysize;)
            {
                ++i;
                randomkey = randomkey + asciichrs[rndm.Next(0, asciichrs.Length - 1)].ToString();
            }
            return System.Security.Cryptography.SHA256Cng.Create().ComputeHash(System.Text.Encoding.ASCII.GetBytes(randomkey));
        }
    }
}
