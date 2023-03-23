using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Xml.Serialization;

namespace Projeto1Criptografia
{
    public class RSA_Encryption : Encryption
    {
        public RSAParameters publicKey { get; set; }
        public RSAParameters privateKey { get; set; }

        public string PublicKeyString { get; set; } = "9iOtmfM3kXsg+Hp9FDu1kbFCQW1bJjuNUw2KrGR5bELsB60emaRPAjfPPxCZMrLy9/oEj2Q3Veir6417YbtGT1NzRV6j9rX06Uk3qPWAIUoJNqweWUB+G7qgHWjBNhj5j3EwzYlY3rmC4K6j0P2oNVX8/97/USaMcZt2e20IXyE=,AQAB";

        public RSA_Encryption()
        {
            RSAParameters publicK, privateK;

            GenerateKeys(out _, out privateK);
            this.privateKey = privateK;

            string[] publicKeyParts = PublicKeyString.Split(',');
            RSAParameters publicKeyFromStr = new RSAParameters();
            publicKeyFromStr.Modulus = Convert.FromBase64String(publicKeyParts[0]);
            publicKeyFromStr.Exponent = Convert.FromBase64String(publicKeyParts[1]);

            this.publicKey = publicKeyFromStr;
            /*
            XmlSerializer serializer = new XmlSerializer(typeof(RSAParameters));
            StringReader sr = new StringReader(PublicKeyString);
            this.publicKey = (RSAParameters)serializer.Deserialize(sr);*/

            // Cria uma instância do provedor de criptografia RSA e define seus parâmetros
           // RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            //rsa.ImportParameters(rsaParams);
        }

        public void GenerateKeys(out RSAParameters publicKey, out RSAParameters privateKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

            publicKey = rsa.ExportParameters(false);
            privateKey = rsa.ExportParameters(true);


            //string publicKeyString = Convert.ToBase64String(publicKey.Modulus) + "," + Convert.ToBase64String(publicKey.Exponent);
            this.PublicKeyString = Convert.ToBase64String(publicKey.Modulus) + "," + Convert.ToBase64String(publicKey.Exponent);
            Console.WriteLine("Public key: {0}", PublicKeyString);

            /*
            RSAParameters publicKey = rsa.ImportFromPem(PublicKeyString);
            rsa.ExportPkcs8PrivateKey
           // byte[] pubrikcey = rsa.ExportRSAPublicKey();
            //Console.WriteLine(Convert.ToBase64String(pubrikcey));
            /*
            // Serializa os parâmetros RSA em um formato XML
            XmlSerializer serializer = new XmlSerializer(typeof(RSAParameters));
            StringWriter sw = new StringWriter();
            serializer.Serialize(sw, publicKey);
            string publicKeyString = sw.ToString();
            Console.WriteLine(publicKeyString);*/
        }

        public override string decrypt(string strToDecrypt)
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

        public override string encrypt(string strToEncrypt)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(publicKey);

            byte[] messageBytes = Encoding.UTF8.GetBytes(strToEncrypt);
            byte[] encryptedBytes = rsa.Encrypt(messageBytes, false);

            return Convert.ToBase64String(encryptedBytes);
        }

        public byte[] CreateSignature(string dataToSign)
        {
            byte[] data = Convert.FromBase64String(dataToSign);

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(privateKey);

            return rsa.SignData(data, SHA256.Create());
        }

        public bool VerifySignature(string dataToVerify, byte[] signedData)//+PublicKey do criador
        {
            byte[] data = Convert.FromBase64String(dataToVerify);

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(this.publicKey);

            return rsa.VerifyData(data, SHA256.Create(), signedData);
        }

        public string GenerateSignature(string str)
        {
            byte[] signature = CreateSignature(str);
            return Convert.ToBase64String(signature);
        }
    }
}
