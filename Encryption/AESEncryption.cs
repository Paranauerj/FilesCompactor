using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Projeto1Criptografia
{
    public class AESEncryption : Encryption
    {

        public Aes aesAlgorithm { get; set; }  
        private readonly string keyBase64 = "/vD2mxah4q0t4geE0GFuNqo1QqG5w+aGK3G0/8Gm3CI=";
        private readonly string ivBase64 = "OfgHAh/ygexZhp2KARmTGA==";

        public AESEncryption()
        {
            this.aesAlgorithm = Aes.Create();
            aesAlgorithm.KeySize = 256;

            // aesAlgorithm.GenerateKey();
            aesAlgorithm.Key = Convert.FromBase64String(keyBase64);
           
            // aesAlgorithm.GenerateIV();
            aesAlgorithm.IV = Convert.FromBase64String(ivBase64);
        }

        public override string decrypt(string strToDecrypt)
        {
            ICryptoTransform decryptor = aesAlgorithm.CreateDecryptor();
            byte[] cipher = Convert.FromBase64String(strToDecrypt);

            using (MemoryStream ms = new MemoryStream(cipher))
            {
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }

        public override string encrypt(string strToEncrypt)
        {
            ICryptoTransform encryptor = aesAlgorithm.CreateEncryptor();
            byte[] encryptedData;

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(strToEncrypt);
                    }
                    encryptedData = ms.ToArray();
                }
            }

            return Convert.ToBase64String(encryptedData);
            
        }
    }
}
