using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Projeto1Criptografia
{
    public class CompressedDataFile
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Signature { get; set; }
        public string CreatorPublicKey { get; set; }
        public string Hash { get; set; }
        public string Password { get; set; }

        public List<FileCompressed> FilesCompressed { get; set; }

        //publicKey do criador
        //assinatua do criador

        public string getPayload()
        {
            string payload = "name:" + this.Name + "date:" + this.Date.ToString();

            foreach(var file in FilesCompressed)
            {
                payload += "file:" + file.Name + "content:" + file.Data;
            }

            return payload;
        }
    }
}
