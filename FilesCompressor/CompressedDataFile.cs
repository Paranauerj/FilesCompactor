using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto1Criptografia
{
    public class CompressedDataFile
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Signature { get; set; }
        public string Hash { get; set; }
        public List<FileCompressed> FilesCompressed { get; set; }

        public string getPayload()
        {
            string payload = "name:" + this.Name + "date:" + this.Date.ToString();
            payload += "signature:" + this.Signature;

            foreach(var file in FilesCompressed)
            {
                payload += "file:" + file.Name + "content:" + file.Data;
            }

            return payload;

        }
    }
}
