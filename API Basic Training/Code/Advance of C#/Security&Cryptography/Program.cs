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

            BLAES blAES = new BLAES();


            // Encrypt the text
            string encryptedText = blAES.Encrypt(originalText);
            Console.WriteLine($"Encrypted Text: {encryptedText}");

            // Decrypt the text
            string decryptedText = blAES.Decrypt(encryptedText);
            Console.WriteLine($"Decrypted Text: {decryptedText}");
            #endregion

            #region Rijndael
            Console.WriteLine("\nRijndael :");
            BLRijndael blRijndael = new BLRijndael();


            // Encrypt the text
            encryptedText = blRijndael.Encrypt(originalText);
            Console.WriteLine($"Encrypted Text: {encryptedText}");

            // Decrypt the text
            encryptedText = blRijndael.Decrypt(encryptedText);
            Console.WriteLine($"Decrypted Text: {decryptedText}");
            #endregion

            #region RSA
            Console.WriteLine("\nRSA:");
            // Generate RSA key pair (public and private keys)
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                BLRSA blRSA = new BLRSA();
                string publicKey = rsa.ToXmlString(false); // Export public key
                string privateKey = rsa.ToXmlString(true); // Export private key

                //Console.WriteLine($"Public Key: {publicKey}");

                // Encrypt the text using the public key
                encryptedText = blRSA.Encrypt(originalText, publicKey);
                Console.WriteLine($"Encrypted Text: {encryptedText}");

                // Decrypt the text using the private key
                decryptedText = blRSA.Decrypt(encryptedText, privateKey);
                Console.WriteLine($"Decrypted Text: {decryptedText}");
            }

            #endregion


            Console.ReadLine();
        }

    }
}
