using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Projeto1Criptografia
{
    public class RSA_Encryption : Encryption
    {/*
        public RSA rsaAlgorithm { get; set; }
        public RSA_Encryption()
        {
            rsaAlgorithm = RSA.Create();
        }*/

        public void GenerateKeys(out RSAParameters publicKey, out RSAParameters privateKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

            publicKey = rsa.ExportParameters(false);
            privateKey = rsa.ExportParameters(true);

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
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(publicKey);

            byte[] messageBytes = Encoding.UTF8.GetBytes(strToEncrypt);
            byte[] encryptedBytes = rsa.Encrypt(messageBytes, false);

            return Convert.ToBase64String(encryptedBytes);
        }

        //public static byte[] CreateSignature(byte[] dataToSign, RSAParameters privateKey)
        public byte[] CreateSignature(string dataToSign, RSAParameters privateKey)
        {
            byte[] data = Convert.FromBase64String(dataToSign);

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(privateKey);
            /*
            byte[] hashBytes = new SHA256Managed().ComputeHash(data);
            byte[] signatureBytes = rsa.SignHash(hashBytes, CryptoConfig.MapNameToOID("SHA256"));

            return signatureBytes;*/

            return rsa.SignData(data, SHA256.Create());
        }

        //public bool VerifySignature(byte[] dataToVerify, byte[] signedData, RSAParameters publicKey)
        public bool VerifySignature(string dataToVerify, byte[] signedData, RSAParameters publicKey)
        {
            byte[] data = Convert.FromBase64String(dataToVerify);

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(publicKey);
            /*
            byte[] hashBytes = new SHA256Managed().ComputeHash(data);

            bool signatureIsValid = rsa.VerifyHash(hashBytes, CryptoConfig.MapNameToOID("SHA256"), signedData);

            return signatureIsValid;
            */
            return rsa.VerifyData(data, SHA256.Create(), signedData);
        }
    }
}
