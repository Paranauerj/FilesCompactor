using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Xml.Serialization;

namespace Projeto1Criptografia
{
    public class RSAEncryption : Signing, Encryption
    {
        public RSAParameters publicKey { get; set; }
        public RSAParameters privateKey { get; set; }
        private AESEncryption AESEncryption { get; set; }

        public RSAEncryption()
        {
            this.AESEncryption = new AESEncryption();

            // Produção
            this.loadKeys();

            // Testes gerando sempre chaves novas, sem salvar priv e pub keys
            // this.generateNewKeys();
        }

        public string GetPublicKeyXML()
        {
            var rsa = RSA.Create();
            rsa.ImportParameters(this.publicKey);
            var publicKeyXml = rsa.ToXmlString(false);

            return publicKeyXml;
        }

        public bool VerifySignature(string signedString, string publicKey)
        {
            try
            {
                var rsa = RSA.Create();
                rsa.FromXmlString(publicKey);

                var signedData = Convert.FromBase64String(signedString);
                var signature = new byte[rsa.KeySize / 8];
                Array.Copy(signedData, 0, signature, 0, signature.Length);

                var data = new byte[signedData.Length - signature.Length];
                Array.Copy(signedData, signature.Length, data, 0, data.Length);
                var verified = rsa.VerifyData(data, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

                return verified;
            }
            catch
            {
                return false;
            }
        }

        public string GenerateSignature(string str)
        {
            var rsa = RSA.Create();
            rsa.ImportParameters(this.privateKey);

            var data = Encoding.UTF8.GetBytes(str);
            var signature = rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

            var signedData = new byte[signature.Length + data.Length];
            Array.Copy(signature, signedData, signature.Length);
            Array.Copy(data, 0, signedData, signature.Length, data.Length);
            var signedString = Convert.ToBase64String(signedData);

            return signedString;
        }

        public string decrypt(string strToDecrypt)
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

        public string encrypt(string strToEncrypt)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(publicKey);

            byte[] messageBytes = Encoding.UTF8.GetBytes(strToEncrypt);
            byte[] encryptedBytes = rsa.Encrypt(messageBytes, false);

            return Convert.ToBase64String(encryptedBytes);
        }

        private void loadKeys()
        {
            var publicKeyFile = "./public.txt";
            var privateKeyFile = "./private.txt";

            if (File.Exists(publicKeyFile) && File.Exists(privateKeyFile))
            {
                // Carrega as chaves dos arquivos
                var publicKeyXml = File.ReadAllText(publicKeyFile);
                var privateKeyXml = this.AESEncryption.decrypt(File.ReadAllText(privateKeyFile));
                publicKey = ImportPublicKeyFromXmlString(publicKeyXml);
                privateKey = ImportPrivateKeyFromXmlString(privateKeyXml);
            }
            else
            {
                // Cria um objeto RSA e gera um par de chaves privada/pública
                var rsa = RSA.Create();
                publicKey = rsa.ExportParameters(false);
                privateKey = rsa.ExportParameters(true);

                // Salva as chaves nos arquivos
                File.WriteAllText(publicKeyFile, ExportPublicKeyToXmlString(publicKey));
                File.WriteAllText(privateKeyFile, this.AESEncryption.encrypt(ExportPrivateKeyToXmlString(privateKey)));
            }
        }

        /// <summary>
        ///     Tests only!!!
        /// </summary>
        private void generateNewKeys()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

            this.publicKey = rsa.ExportParameters(false);
            this.privateKey = rsa.ExportParameters(true);
        }

        private static RSAParameters ImportPublicKeyFromXmlString(string publicKeyXml)
        {
            var rsa = RSA.Create();
            rsa.FromXmlString(publicKeyXml);
            return rsa.ExportParameters(false);
        }

        private static RSAParameters ImportPrivateKeyFromXmlString(string privateKeyXml)
        {
            var rsa = RSA.Create();
            rsa.FromXmlString(privateKeyXml);
            return rsa.ExportParameters(true);
        }

        private static string ExportPublicKeyToXmlString(RSAParameters publicKey)
        {
            var rsa = RSA.Create();
            rsa.ImportParameters(publicKey);
            return rsa.ToXmlString(false);
        }

        private static string ExportPrivateKeyToXmlString(RSAParameters privateKey)
        {
            var rsa = RSA.Create();
            rsa.ImportParameters(privateKey);
            return rsa.ToXmlString(true);
        }

    }
}
