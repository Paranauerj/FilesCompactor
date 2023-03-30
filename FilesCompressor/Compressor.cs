using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Projeto1Criptografia
{
    public enum OperationOutput
    {
        OK,
        IntegrityError,
        InvalidPassword,
        InvalidSignature,
        FileNotFound,
        PathNotFound
    }

    public class Compressor
    {

        private AESEncryption AESEncryption { get; }
        private SHAEncryption SHAEncryption { get; }
        private RSAEncryption RSAEncryption { get; }

        public Compressor()
        {
            AESEncryption = new AESEncryption();
            SHAEncryption = new SHAEncryption();
            RSAEncryption = new RSAEncryption();
            
            // Algumas observações
            /*
             * Para encriptografar, ele lê os bytes dos ficheiros, converte os ficheiros (bytes) pra base 64 e depois encripta a base64
             * Para decriptografar, ele pega o conteúdo dos ficheiros (base64), decripta e depois converte para bytes, e por fim salva
             * 
             * Isso permite que imagens, vídeos e outros arquivos com diferentes encodings
             * possam ser criptografados. Se fosse só em strings, as imagens, vídeos, etc. não abrem
             */
        }

        public OperationOutput Compress(string password = "", string targetDirectoryArg = "", string pathToSave = "")
        {
            string rootFolder = targetDirectoryArg == "" ? @"../../../decompression_tests/Pasta de testes" : targetDirectoryArg;

            if (!CheckFileOrFolderExists(rootFolder))
            {
                Console.WriteLine("Nao foi possivel encontrar o caminho");
                return OperationOutput.PathNotFound;
            }

            var compressedDataFile = new CompressedDataFile()
            {
                Date = DateTime.Now,
                Name = Path.GetFileName(rootFolder),
                Password = this.SHAEncryption.encrypt(password)
            };

            compressedDataFile.FilesCompressed = this.GetAndEncryptAllFilesInFolderAndSubfolders(rootFolder);

            compressedDataFile.Hash = this.SHAEncryption.encrypt(compressedDataFile.getPayload());

            compressedDataFile.Signature = RSAEncryption.GenerateSignature(compressedDataFile.Hash);
            compressedDataFile.CreatorPublicKey = RSAEncryption.GetPublicKeyXML();

            string json = JsonConvert.SerializeObject(compressedDataFile, Formatting.Indented);
            string jsonEncrypted = this.AESEncryption.encrypt(json);
            File.WriteAllText((pathToSave == "" ? "../../../compressions_tests/" : pathToSave) + compressedDataFile.Name + ".hajr", jsonEncrypted);
            Console.WriteLine("Compressão terminada");

            return OperationOutput.OK;
        }

        public OperationOutput Decompress(string password = "", string fileWithPath = "", string targetDirectoryArg = "")
        {
            string file = fileWithPath == "" ? @"../../../compressions_tests/Pasta de testes.hajr" : fileWithPath;

            if (!CheckFileOrFolderExists(file))
            {
                Console.WriteLine("Nao foi possivel encontrar o ficheiro");
                return OperationOutput.PathNotFound;
            }

            string fileContentEncrypted = File.ReadAllText(file);
            string fileContent = this.AESEncryption.decrypt(fileContentEncrypted);

            var compressedDataFile = JsonConvert.DeserializeObject<CompressedDataFile>(fileContent);

            if (compressedDataFile.Hash != this.SHAEncryption.encrypt(compressedDataFile.getPayload()))
            {
                Console.WriteLine("Nao foi possivel garantir a integridade do ficheiro!");
                return OperationOutput.IntegrityError;
            }

            if (compressedDataFile.Password != this.SHAEncryption.encrypt(password))
            {
                Console.WriteLine("Palavra-passe invalida");
                return OperationOutput.InvalidPassword;
            }

            if (!RSAEncryption.VerifySignature(compressedDataFile.Signature, compressedDataFile.CreatorPublicKey))
            {
                Console.WriteLine("Assinatura inválida");
                return OperationOutput.InvalidSignature;
            }

            var targetDirectory = (targetDirectoryArg == "" ? @"../../../decompression_tests/" : targetDirectoryArg) + compressedDataFile.Name;
            Directory.CreateDirectory(targetDirectory);

            this.WriteAllEncryptedDataInFiles(compressedDataFile.FilesCompressed, targetDirectory);
            compressedDataFile.ListFiles();

            Console.WriteLine("Descompressão terminada");
            return OperationOutput.OK;
        }

        private List<FileCompressed> GetAndEncryptAllFilesInFolderAndSubfolders(string rootFolder)
        {
            var compressedFiles = new List<FileCompressed>();

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

            return compressedFiles;

        }

        private void WriteAllEncryptedDataInFiles(List<FileCompressed> filesCompressed, string targetDirectory)
        {
            foreach (var fileToWrite in filesCompressed)
            {
                var filePath = targetDirectory + "/" + fileToWrite.Name;
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                byte[] fileBytes = Convert.FromBase64String(this.AESEncryption.decrypt(fileToWrite.Data));

                try
                {
                    File.WriteAllBytes(filePath, fileBytes);
                }
                catch
                {
                    Console.WriteLine("Ocorreu um problema ao salvar o ficheiro {0}. Ele pode ter sido corrompido ou decomprimido com sucesso. Abra-o e verifique", filePath);
                }
            }
        }

        private bool CheckFileOrFolderExists(string path)
        {
            if (File.Exists(path) || Directory.Exists(path))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
