using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Projeto1Criptografia
{
    public class CompactedDataFile
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Signature { get; set; }
        public string CreatorPublicKey { get; set; }
        public string Hash { get; set; }
        public string Password { get; set; }

        public List<FileCompacted> FilesCompressed { get; set; }

        public string getPayload()
        {
            string payload = "name:" + this.Name + "date:" + this.Date.ToString() + "password:" + this.Password;

            foreach(var file in FilesCompressed)
            {
                payload += "file:" + file.Name + "content:" + file.Data;
            }

            return payload;
        }

        public void ListFiles()
        {
            Console.WriteLine("\n######## Ficheiros #########");
            foreach(var item in this.FilesCompressed)
            {
                Console.WriteLine("- " + item.Name);
            }
            Console.WriteLine("");
        }

    }
}
