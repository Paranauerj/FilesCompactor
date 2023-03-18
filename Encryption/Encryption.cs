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

        public virtual string decrypt(string strToDecrypt)
        {
            throw new NotImplementedException();
        }
    }
}
