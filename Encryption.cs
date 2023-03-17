using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto1Criptografia
{
    public abstract class Encryption
    {
        public abstract string encrypt(string strToEncrypt);

        public abstract string decrypt(string strToDecrypt);
    }
}
