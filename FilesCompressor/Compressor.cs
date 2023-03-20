﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Projeto1Criptografia
{
    public class Compressor
    {
        public RSAParameters publicKey { get; set; }
        public RSAParameters privateKey { get; set; }

        private AESEncryption AESEncryption { get; }
        private SHAEncryption SHAEncryption { get; }

        public Compressor()
        {
            AESEncryption = new AESEncryption();
            SHAEncryption = new SHAEncryption();

            RSAParameters publicK, privateK;

            RSA_Encryption rsa = new RSA_Encryption();
            rsa.GenerateKeys(out publicK, out privateK);
            this.privateKey = privateK;
            this.publicKey = publicK;


            // Algumas observações
            /*
             * Para encriptografar, ele lê os bytes dos ficheiros, converte os ficheiros (bytes) pra base 64 e depois encripta a base64
             * Para decriptografar, ele pega o conteúdo dos ficheiros (base64), decripta e depois converte para bytes, e por fim salva
             * 
             * Isso permite que imagens, vídeos e outros arquivos com diferentes encodings
             * possam ser criptografados. Se fosse só em strings, as imagens, vídeos, etc. não abrem
             */
        }

        public void Compress(string targetDirectoryArg = "", string pathToSave = "")
        {
            string rootFolder = targetDirectoryArg == "" ? @"../../../decompression_tests/Pasta de testes" : targetDirectoryArg;

            var compressedFiles = new List<FileCompressed>();
            var compressedDataFile = new CompressedDataFile();

            compressedDataFile.Date = DateTime.Now;
            compressedDataFile.Name = Path.GetFileName(rootFolder);

            string[] allFiles = Directory.GetFiles(rootFolder, "*.*", SearchOption.AllDirectories);

            foreach (string file in allFiles)
            {
                string fileName = file.Substring(rootFolder.Length + 1);
                byte[] fileContentInBytes = File.ReadAllBytes(file);
                string fileContent = Convert.ToBase64String(fileContentInBytes);

                compressedFiles.Add(new FileCompressed()
                {
                    Name = fileName,
                    Data = this.AESEncryption.encrypt(fileContent)
                });
            }

            compressedDataFile.FilesCompressed = compressedFiles;

            // Assinar ficheiro com RSA
            CompressedDataFile cdf = new CompressedDataFile();
            compressedDataFile.Signature = cdf.GenerateSignature(this.privateKey, compressedFiles);

            compressedDataFile.Hash = this.SHAEncryption.encrypt(compressedDataFile.getPayload());

            string json = JsonConvert.SerializeObject(compressedDataFile, Formatting.Indented);
            File.WriteAllText((pathToSave == "" ? "../../../compressions_tests/" : pathToSave) + compressedDataFile.Name + ".hajr", json);

        }

        public void Decompress(string fileWithPath = "", string targetDirectoryArg = "")
        {
            string file = fileWithPath == "" ? @"../../../compressions_tests/Pasta de testes.hajr" : fileWithPath;
            string fileContent = File.ReadAllText(file);

            var compressedDataFile = JsonConvert.DeserializeObject<CompressedDataFile>(fileContent);

            // verificar a assinatura. se for inválida, dizer q é inválida e parar execução
            RSA_Encryption rsa = new RSA_Encryption();

            string signature = compressedDataFile.Signature;
            byte[] sign = Convert.FromBase64String(signature);


            if (sign == null)
            {
                Console.WriteLine("Sem assinatura");
                return;
            }

            if (rsa.VerifySignature(Convert.ToBase64String(compressedDataFile.GetBytes(compressedDataFile.FilesCompressed)), sign, this.publicKey))
                Console.WriteLine("Assinatura Válida");
            else
            {
                Console.WriteLine("Assinatura inválida");
                return;
            }

            var targetDirectory = (targetDirectoryArg == "" ? @"../../../decompression_tests/" : targetDirectoryArg) + compressedDataFile.Name;
            Directory.CreateDirectory(targetDirectory);

            foreach(var fileToWrite in compressedDataFile.FilesCompressed)
            {
                var filePath = targetDirectory + "/" + fileToWrite.Name;
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                byte[] fileBytes = Convert.FromBase64String(this.AESEncryption.decrypt(fileToWrite.Data));
                File.WriteAllBytes(filePath, fileBytes);
            }
        }
    }
}
