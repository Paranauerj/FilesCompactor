using Projeto1Criptografia;
using System.Security.Cryptography;


AESEncryption enc = new AESEncryption();
string plainText = "Oi";
string cipherText = enc.encrypt(plainText);
Console.WriteLine(cipherText);
string decryptedPlainText = enc.decrypt(cipherText);
Console.WriteLine(plainText);

SHAEncryption shaenc = new SHAEncryption();
Console.WriteLine(shaenc.encrypt(decryptedPlainText));