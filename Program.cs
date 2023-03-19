using Projeto1Criptografia;
using System.Security.Cryptography;

string plainText = "Oiáç@";

/*
AESEncryption enc = new AESEncryption();
Console.WriteLine("String a encriptar: " + plainText);
string cipherText = enc.encrypt(plainText);
Console.WriteLine("String encriptada: " + cipherText);
string decryptedPlainText = enc.decrypt(cipherText);
Console.WriteLine("String desencriptada: " + decryptedPlainText);

SHAEncryption shaenc = new SHAEncryption();
Console.WriteLine(shaenc.encrypt(plainText));*/

Console.WriteLine("### RSA Encryption ###");
Console.WriteLine("Frase Original: " + plainText);
RSA_Encyption rsa = new RSA_Encyption();
RSAParameters publicKey, privateKey;
rsa.GenerateKeys(out publicKey, out privateKey);
string rsaEncripted = rsa.encrypt_b(plainText, publicKey);
Console.WriteLine("Encripted: " + rsaEncripted);
Console.WriteLine("Decrypted: " + rsa.decrypt_b(rsaEncripted, privateKey));
