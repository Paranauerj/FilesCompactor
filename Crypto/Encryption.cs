using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Projeto1Criptografia
{
    public interface Encryption
    {
        public string encrypt(string strToEncrypt);

        public string decrypt(string strToDecrypt);
    }
}
