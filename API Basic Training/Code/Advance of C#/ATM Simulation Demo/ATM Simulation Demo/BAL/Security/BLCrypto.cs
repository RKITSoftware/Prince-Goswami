using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Simulation_Demo.BAL.Security
{
    public static class BLCrypto
    {
        static byte[] key =
           {
                0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
            };
        // Encryption method
        public static string Encrypt(string plainText)
        {
            using (RijndaelManaged rijndaelAlg = new RijndaelManaged())
            {
                rijndaelAlg.Key = key;
                rijndaelAlg.IV = new byte[rijndaelAlg.BlockSize / 8];

                ICryptoTransform encryptor = rijndaelAlg.CreateEncryptor(rijndaelAlg.Key, rijndaelAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }

                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        // Decryption method
        public static string Decrypt(string cipherText)
        {
            using (Aes rijndaelAlg = Aes.Create())
            {
                rijndaelAlg.Key = key;
                rijndaelAlg.IV = new byte[rijndaelAlg.BlockSize / 8];

                ICryptoTransform decryptor = rijndaelAlg.CreateDecryptor(rijndaelAlg.Key, rijndaelAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
