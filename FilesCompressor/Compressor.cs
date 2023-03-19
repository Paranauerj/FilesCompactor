using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto1Criptografia
{
    public class Compressor
    {

        private AESEncryption AESEncryption { get; }
        private SHAEncryption SHAEncryption { get; }

        public Compressor()
        {
            AESEncryption = new AESEncryption();
            SHAEncryption = new SHAEncryption();
        }

        public void Compress()
        {
            // string rootFolder = @"./";
            string rootFolder = @"./Pasta de testes";

            var compressed = new List<FileCompressed>();
            var compressedDataFile = new CompressedDataFile();

            compressedDataFile.Date = DateTime.Now;
            compressedDataFile.Name = Path.GetFileName(rootFolder);

            string[] allFiles = Directory.GetFiles(rootFolder, "*.*", SearchOption.AllDirectories);
            string payload = compressedDataFile.Name + compressedDataFile.Date.ToString();

            foreach (string file in allFiles)
            {
                string fileName = file.Substring(rootFolder.Length + 1);
                Console.WriteLine(fileName);
                string fileContent = File.ReadAllText(file);
                payload += fileName + fileContent;

                compressed.Add(new FileCompressed()
                {
                    Name = fileName,
                    Data = this.AESEncryption.encrypt(fileContent)
                });
            }

            compressedDataFile.Hash = this.SHAEncryption.encrypt(payload);
            compressedDataFile.FilesCompressed = compressed;

            string json = JsonConvert.SerializeObject(compressedDataFile, Formatting.Indented);
            File.WriteAllText("../../../compressions_tests/" + compressedDataFile.Name + ".hajr", json);

        }
    }
}
