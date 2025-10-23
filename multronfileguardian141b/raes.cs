using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        public static string getrandomstring(int size)
        {
            string ascchr = "qwertyuiopasdfghjklzxcvbnm";
            string rndmstr = "";
            Random pseudo = new Random();
            for(int i = 0; i <= size;)
            {
                rndmstr = rndmstr + ascchr[pseudo.Next(-0, ascchr.Length)];
                ++i;
            }
            return rndmstr;
        }
        public static byte[] generaterandomkey(int keysize)
        {
            byte[] rndkey = new byte[keysize];
            RandomNumberGenerator.Create().GetNonZeroBytes(rndkey);
            return rndkey;
        }
    }
}
