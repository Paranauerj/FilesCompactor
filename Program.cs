using Projeto1Criptografia;
using System.Security.Cryptography;

string plainText = "Oi";
/*
AESEncryption enc = new AESEncryption();

Console.WriteLine("Texto original: " + plainText);

string cipherText = enc.encrypt(plainText);
Console.WriteLine("Texto encriptado: " + cipherText);

string decryptedPlainText = enc.decrypt(cipherText);
Console.WriteLine("Texto decriptado: " + decryptedPlainText);

SHAEncryption shaenc = new SHAEncryption();
Console.WriteLine("Oi encriptado em sha256: " + shaenc.encrypt(decryptedPlainText));
*/
/*
Console.WriteLine("### RSA Encryption ###");
Console.WriteLine("Frase Original: " + plainText);

RSA_Encryption rsa = new RSA_Encryption();
RSAParameters publicKey, privateKey;
rsa.GenerateKeys(out publicKey, out privateKey);

string rsaEncripted = rsa.encrypt_b(plainText, publicKey);
Console.WriteLine("Encripted: " + rsaEncripted);
byte[] signature = rsa.CreateSignature(rsaEncripted, privateKey);
Console.WriteLine("Signature: " + Convert.ToBase64String(signature));

Console.WriteLine("Decrypted: " + rsa.decrypt_b(rsaEncripted, privateKey));
bool signatureVeracity = rsa.VerifySignature(rsaEncripted, signature, publicKey);
Console.WriteLine(signatureVeracity);*/

var comp = new Compressor();

// comp.Compress(targetDirectoryArg: @"D:\Users\jptin\Desktop\Programação\só testando utad", pathToSave: @"D:\Users\jptin\Desktop\Programação\", password: "oi");
// comp.Decompress(fileWithPath: @"D:\Users\jptin\Desktop\Programação\só testando utad.hajr", targetDirectoryArg: @"D:\Users\jptin\Desktop\Programação\", password: "oi");

// pasta default: ver raiz do projeto visual studio
comp.Compress();
comp.Decompress();