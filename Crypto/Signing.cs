using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto1Criptografia
{
    public interface Signing
    {
        public bool VerifySignature(string signedString, string publicKey);
        public string GenerateSignature(string str);

    }
}
