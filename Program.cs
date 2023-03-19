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

Console.WriteLine("### RSA Encryption ###");
Console.WriteLine("Frase Original: " + plainText);
RSA_Encryption rsa = new RSA_Encryption();
RSAParameters publicKey, privateKey;
rsa.GenerateKeys(out publicKey, out privateKey);
string rsaEncripted = rsa.encrypt_b(plainText, publicKey);
Console.WriteLine("Encripted: " + rsaEncripted);
Console.WriteLine("Decrypted: " + rsa.decrypt_b(rsaEncripted, privateKey));
Console.WriteLine("Oi encriptado em sha256: " + shaenc.encrypt(decryptedPlainText));

var comp = new Compressor();

// comp.Compress(@"D:\Users\jptin\Desktop\Programação\só testando utad", @"D:\Users\jptin\Desktop\Programação\");
// comp.Decompress(@"D:\Users\jptin\Desktop\Programação\só testando utad.hajr", @"D:\Users\jptin\Desktop\Programação\");

// pasta default: ver raiz do projeto visual studio
// comp.Compress();
// comp.Decompress();