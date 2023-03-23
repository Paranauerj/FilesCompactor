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
        public string Hash { get; set; }
        public List<FileCompressed> FilesCompressed { get; set; }

        //publicKey do criador
        //assinatua do criador

        public byte[] GetBytes(List<FileCompressed> FilesCompressed)
        {
            List<byte> byteList = new List<byte>();
            foreach (FileCompressed obj in FilesCompressed)
            {
                byteList.AddRange(Encoding.ASCII.GetBytes(obj.Name));
                byteList.AddRange(Encoding.ASCII.GetBytes(obj.Data));
            }

            byte[] byteArray = byteList.ToArray();

            return byteArray;
        }

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
