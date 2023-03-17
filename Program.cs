using System.Security.Cryptography;

Console.WriteLine("Creating Aes Encryption 256 bit key");

Aes aesAlgorithm = Aes.Create();
aesAlgorithm.KeySize = 256;
aesAlgorithm.GenerateKey();
string keyBase64 = Convert.ToBase64String(aesAlgorithm.Key);
Console.WriteLine($"Aes Key Size : {aesAlgorithm.KeySize}");
Console.WriteLine("Here is the Aes key in Base64:");
Console.WriteLine(keyBase64);

string plainText = "Oi";
string cipherText = EncryptDataWithAes(plainText, keyBase64, out string vectorBase64);

Console.WriteLine("--------------------------------------------------------------");
Console.WriteLine("Here is the cipher text:");
Console.WriteLine(cipherText);

Console.WriteLine("--------------------------------------------------------------");
Console.WriteLine("Here is the Aes IV in Base64:");
Console.WriteLine(vectorBase64);

string decryptedPlainText = DecryptDataWithAes(cipherText, keyBase64, vectorBase64);

Console.WriteLine("--------------------------------------------------------------");
Console.WriteLine("Here is the decrypted data:");
Console.WriteLine(plainText);


static string EncryptDataWithAes(string plainText, string keyBase64, out string vectorBase64)
{
    using (Aes aesAlgorithm = Aes.Create())
    {
        aesAlgorithm.Key = Convert.FromBase64String(keyBase64);
        aesAlgorithm.GenerateIV();
        Console.WriteLine($"Aes Cipher Mode : {aesAlgorithm.Mode}");
        Console.WriteLine($"Aes Padding Mode: {aesAlgorithm.Padding}");
        Console.WriteLine($"Aes Key Size : {aesAlgorithm.KeySize}");

        //set the parameters with out keyword
        vectorBase64 = Convert.ToBase64String(aesAlgorithm.IV);

        // Create encryptor object
        ICryptoTransform encryptor = aesAlgorithm.CreateEncryptor();

        byte[] encryptedData;

        //Encryption will be done in a memory stream through a CryptoStream object
        using (MemoryStream ms = new MemoryStream())
        {
            using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            {
                using (StreamWriter sw = new StreamWriter(cs))
                {
                    sw.Write(plainText);
                }
                encryptedData = ms.ToArray();
            }
        }

        return Convert.ToBase64String(encryptedData);
    }
}

private static string DecryptDataWithAes(string cipherText, string keyBase64, string vectorBase64)
{
    using (Aes aesAlgorithm = Aes.Create())
    {
        aesAlgorithm.Key = Convert.FromBase64String(keyBase64);
        aesAlgorithm.IV = Convert.FromBase64String(vectorBase64);

        Console.WriteLine($"Aes Cipher Mode : {aesAlgorithm.Mode}");
        Console.WriteLine($"Aes Padding Mode: {aesAlgorithm.Padding}");
        Console.WriteLine($"Aes Key Size : {aesAlgorithm.KeySize}");
        Console.WriteLine($"Aes Block Size : {aesAlgorithm.BlockSize}");


        // Create decryptor object
        ICryptoTransform decryptor = aesAlgorithm.CreateDecryptor();

        byte[] cipher = Convert.FromBase64String(cipherText);

        //Decryption will be done in a memory stream through a CryptoStream object
        using (MemoryStream ms = new MemoryStream(cipher))
        {
            using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            {
                using (StreamReader sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}