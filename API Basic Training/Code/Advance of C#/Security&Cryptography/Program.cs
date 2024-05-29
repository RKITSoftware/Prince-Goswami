// Add necessary using directives
using Security_Cryptography.BL;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Security_Cryptography
{
    class Program
    {
        static void Main(string[] args)
        {
            // Example usage
            string originalText = "Hello, this is a secret message!";

            #region AES
            Console.WriteLine("\nAES:");

            // Create an instance of BLAES
            BLAES blAES = new BLAES();

            // Encrypt the text
            string encryptedTextAES = blAES.Encrypt(originalText);
            Console.WriteLine($"Encrypted Text: {encryptedTextAES}");

            // Decrypt the text
            string decryptedTextAES = blAES.Decrypt(encryptedTextAES);
            Console.WriteLine($"Decrypted Text: {decryptedTextAES}");
            #endregion

            #region Rijndael
            Console.WriteLine("\nRijndael:");

            // Create an instance of BLRijndael
            BLRijndael blRijndael = new BLRijndael();

            // Encrypt the text
            string encryptedTextRijndael = blRijndael.Encrypt(originalText);
            Console.WriteLine($"Encrypted Text: {encryptedTextRijndael}");

            // Decrypt the text
            string decryptedTextRijndael = blRijndael.Decrypt(encryptedTextRijndael);
            Console.WriteLine($"Decrypted Text: {decryptedTextRijndael}");
            #endregion

            #region RSA
            Console.WriteLine("\nRSA:");

            // Create an instance of BLRSA
            BLRSA blRSA = new BLRSA();

            // Generate RSA key pair (public and private keys)
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                string publicKey = rsa.ToXmlString(false); // Export public key
                string privateKey = rsa.ToXmlString(true); // Export private key

                // Encrypt the text using the public key
                string encryptedTextRSA = blRSA.Encrypt(originalText, publicKey);
                Console.WriteLine($"Encrypted Text: {encryptedTextRSA}");

                // Decrypt the text using the private key
                string decryptedTextRSA = blRSA.Decrypt(encryptedTextRSA, privateKey);
                Console.WriteLine($"Decrypted Text: {decryptedTextRSA}");
            }
            #endregion

            Console.ReadLine();
        }
    }
}
