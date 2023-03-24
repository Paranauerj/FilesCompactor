using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Projeto1Criptografia
{
    public class SHAEncryption : Encryption
    {

        public SHA256 shaAlgorithm { get; set; }

        public SHAEncryption()
        {
            this.shaAlgorithm = SHA256.Create();
        }

        public string encrypt(string strToEncrypt)
        {
            byte[] bytes = shaAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(strToEncrypt));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }

        public string decrypt(string strToDecrypt)
        {
            throw new NotImplementedException();
        }
    }
}
