using System.IO;
using System.Security.Cryptography;

namespace FinalDemo.Security
{
    public static class RijndaelSecurity
    {
        public static byte[] Encrypt(string plainText, byte[] key, byte[] iv, int blockSize)
        {
            using (var rijndael = new RijndaelManaged())
            {
                rijndael.KeySize = key.Length * 8;
                rijndael.BlockSize = blockSize;
                rijndael.Key = key;
                rijndael.IV = iv;

                using (var encryptor = rijndael.CreateEncryptor(rijndael.Key, rijndael.IV))
                using (var ms = new MemoryStream())
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                using (var writer = new StreamWriter(cs))
                {
                    writer.Write(plainText);
                    writer.Flush();
                    cs.FlushFinalBlock();
                    return ms.ToArray();
                }
            }
        }

        public static string Decrypt(byte[] cipherText, byte[] key, byte[] iv, int blockSize)
        {
            using (var rijndael = new RijndaelManaged())
            {
                rijndael.KeySize = key.Length * 8;
                rijndael.BlockSize = blockSize;
                rijndael.Key = key;
                rijndael.IV = iv;

                using (var decryptor = rijndael.CreateDecryptor(rijndael.Key, rijndael.IV))
                using (var ms = new MemoryStream(cipherText))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var reader = new StreamReader(cs))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
