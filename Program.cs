using Projeto1Criptografia;
using System.Security.Cryptography;


AESEncryption enc = new AESEncryption();

string plainText = "Oi";
Console.WriteLine("Texto original: " + plainText);

string cipherText = enc.encrypt(plainText);
Console.WriteLine("Texto encriptado: " + cipherText);

string decryptedPlainText = enc.decrypt(cipherText);
Console.WriteLine("Texto decriptado: " + decryptedPlainText);

SHAEncryption shaenc = new SHAEncryption();
Console.WriteLine("Oi encriptado em sha256: " + shaenc.encrypt(decryptedPlainText));