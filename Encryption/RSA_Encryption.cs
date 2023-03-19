using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Projeto1Criptografia
{
    public class RSA_Encryption : Encryption
    {
        public RSA rsaAlgorithm { get; set; }
        public RSA_Encryption()
        {
            rsaAlgorithm = RSA.Create();
        }

        public void GenerateKeys(out RSAParameters publicKey, out RSAParameters privateKey)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                publicKey = rsa.ExportParameters(false);
                privateKey = rsa.ExportParameters(true);
            }
        }

        public override string decrypt_b(string strToDecrypt, RSAParameters privateKey)
        {
            byte[] cipher = Convert.FromBase64String(strToDecrypt);

            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(privateKey);

                byte[] decryptedBytes = rsa.Decrypt(cipher, false);
                string decryptedMessage = Encoding.UTF8.GetString(decryptedBytes);

                return decryptedMessage;
            }
        }

        public override string encrypt_b(string strToEncrypt, RSAParameters publicKey)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(publicKey);

                byte[] messageBytes = Encoding.UTF8.GetBytes(strToEncrypt);
                byte[] encryptedBytes = rsa.Encrypt(messageBytes, false);

                return Convert.ToBase64String(encryptedBytes);
            }
        }
    }
}
