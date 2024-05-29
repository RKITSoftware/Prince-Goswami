using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Security_Cryptography.BL
{
    /// <summary>
    /// Provides methods for encrypting and decrypting using RSA encryption algorithm.
    /// </summary>
    internal class BLRSA
    {
        #region Encryption

        /// <summary>
        /// Encrypts the given plaintext using RSA encryption.
        /// </summary>
        /// <param name="plainText">The plaintext to encrypt.</param>
        /// <param name="publicKey">The public key used for encryption.</param>
        /// <returns>The encrypted ciphertext.</returns>
        public string Encrypt(string plainText, string publicKey)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(publicKey);

                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] encryptedBytes = rsa.Encrypt(plainBytes, false);

                return Convert.ToBase64String(encryptedBytes);
            }
        }

        #endregion

        #region Decryption

        /// <summary>
        /// Decrypts the given ciphertext using RSA decryption.
        /// </summary>
        /// <param name="encryptedText">The ciphertext to decrypt.</param>
        /// <param name="privateKey">The private key used for decryption.</param>
        /// <returns>The decrypted plaintext.</returns>
        public string Decrypt(string encryptedText, string privateKey)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(privateKey);

                byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
                byte[] decryptedBytes = rsa.Decrypt(encryptedBytes, false);

                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }

        #endregion
    }
}
