using Projeto1Criptografia;

var comp = new Compressor();

void ShowMenu()
{
    Console.Clear();
    Console.WriteLine("***MENU***");
    Console.WriteLine("1- Comprimir Ficheiro");
    Console.WriteLine("2- Descomprimir Ficheiro");
    Console.WriteLine("3- Defenições");
    Console.WriteLine("4- SAIR");
}

void Op1()
{
    Console.WriteLine("Qual é o diretório do(s) ficheiro(s) que pretende comprimir?\n(Para usar o caminho padrão, não escrever nada)");
    string? targetDirectoryArg = Console.ReadLine();
    Console.WriteLine("Qual é o diretório onde pretende guardar o ficheiro comprimido?\n(Para usar o caminho padrão, não escrever nada)");
    string? pathToSave = Console.ReadLine();
    Console.WriteLine("Qual é a palavra pass?\n(Para não usar palavrapass, não escrever nada)");
    string? password = Console.ReadLine();

    comp.Compress(password, targetDirectoryArg, pathToSave);

    Console.WriteLine("Pressione qualquer tecla para avançar");
    Console.ReadKey();
}

void Op2()
{
    Console.WriteLine("Qual é o diretório do ficheiro que pretende descomprimir?\n(Para usar o caminho padrão, não escrever nada)");
    string? fileWithPath = Console.ReadLine();
    Console.WriteLine("Qual é o diretório onde pretende guardar o(s) ficheiro(s) descomprimido(s)?\n(Para usar o caminho padrão, não escrever nada)");
    string? targetDirectoryArg = Console.ReadLine();

    if (targetDirectoryArg != null)
    {
        if (!targetDirectoryArg.EndsWith("\\"))
            targetDirectoryArg += "\\";
    }

    Console.WriteLine("Qual é a palavra pass?");
    string? password = Console.ReadLine();

    comp.Decompress(password, fileWithPath, targetDirectoryArg);

    Console.WriteLine("Pressione qualquer tecla para avançar");
    Console.ReadKey();
}

void Op3()
{
    Console.WriteLine("Em construção!!!");

    Console.WriteLine("Pressione qualquer tecla para avançar");
    Console.ReadKey();
}

while (true)
{
    ShowMenu();
    Console.Write("Opção: ");
    var op = Console.ReadLine();
    Console.Clear();

    switch (op)
    {
        case "1":
            Op1();
            break;

        case "2":
            Op2();
            break;

        case "3":
            Op3();
            break;

        case "4":
            return;
    }
}

// comp.Compress(targetDirectoryArg: @"D:\Users\jptin\Desktop\Programação\só testando utad", pathToSave: @"D:\Users\jptin\Desktop\Programação\", password: "oi");
// comp.Decompress(fileWithPath: @"D:\Users\jptin\Desktop\Programação\só testando utad.hajr", targetDirectoryArg: @"D:\Users\jptin\Desktop\Programação\", password: "oi");

// pasta default: ver raiz do projeto visual studio
// comp.Compress();
// comp.Decompress();