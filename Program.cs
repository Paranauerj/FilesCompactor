using Projeto1Criptografia;
using System.Security.Cryptography;

var comp = new Compressor();

// comp.Compress(targetDirectoryArg: @"D:\Users\jptin\Desktop\Programação\só testando utad", pathToSave: @"D:\Users\jptin\Desktop\Programação\", password: "oi");
// comp.Decompress(fileWithPath: @"D:\Users\jptin\Desktop\Programação\só testando utad.hajr", targetDirectoryArg: @"D:\Users\jptin\Desktop\Programação\", password: "oi");

// pasta default: ver raiz do projeto visual studio
// comp.Compress();
comp.Decompress();