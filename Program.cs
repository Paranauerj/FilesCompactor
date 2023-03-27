using Projeto1Criptografia;

var comp = new Compressor();
var aesE = new AESEncryption();

void ShowMenu()
{
    Console.Clear();
    Console.WriteLine("***MENU***");
    Console.WriteLine("1- Comprimir Ficheiro");
    Console.WriteLine("2- Descomprimir Ficheiro");
    Console.WriteLine("3- Defenições");
    Console.WriteLine("4- SAIR");
}

void ShowMenuOP3()
{
    Console.Clear();
    Console.WriteLine("***DEFENIÇÕES***");
    Console.WriteLine("1- Ver Private Key");
    Console.WriteLine("2- Ver Public Key");
    Console.WriteLine("3- Criar novo par de Keys");
    Console.WriteLine("4- VOLTAR");
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
    int erro = -1, tentativa = 0;

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

    erro = comp.Decompress(password, fileWithPath, targetDirectoryArg); //0: Tudo correu bem
                                                                        //1: Nao foi possivel garantir a integridade do ficheiro!
                                                                        //2: Palavra-passe invalida [Aumentar o numero de tentativa, às 3 sai]
                                                                        //3: Assinatura inválida

    if (erro == 1 && erro == 3)
        return;

    while (erro == 2)
    {
        if (tentativa == 3)
        {
            Console.WriteLine("Número máximo de tentativas alcançado.");
            break;
        }

        Console.WriteLine("Qual é a palavra pass?");
        password = Console.ReadLine();

        erro = comp.Decompress(password, fileWithPath, targetDirectoryArg);

        tentativa++;
    }
    

    Console.WriteLine("Pressione qualquer tecla para avançar");
    Console.ReadKey();
}

void Op3_3(string publicKeyFile, string privateKeyFile)
{
    Console.WriteLine("Tem a certeza que quer criar um novo par de Keys?");
    Console.WriteLine("Escreva \"Sim\" para confirmar");
    Console.Write("->");
    string? conf = Console.ReadLine();

    if(conf.ToLower() != "sim")
    {
        Console.WriteLine("Confirmação negada!");
        Console.WriteLine("Pressione qualquer tecla para avançar");
        Console.ReadKey();
    }
    else
    {
        Console.WriteLine("Confirmação aceite!");

        if (File.Exists(publicKeyFile))
            File.Delete(publicKeyFile);

        if (File.Exists(privateKeyFile))
            File.Delete(privateKeyFile);

        comp = new Compressor();

        Console.WriteLine("Par de Keys alterado com sucesso!");
    }
}

void Op3()
{
    while (true)
    {
        ShowMenuOP3();
        string publicKeyFile = "./public.txt";
        string privateKeyFile = "./private.txt";

        Console.Write("Opção: ");
        var op = Console.ReadLine();
        Console.Clear();

        switch (op)
        {
            case "1": //Ver Private Key
                var privateKeyXml = aesE.decrypt(File.ReadAllText(privateKeyFile));
                Console.WriteLine("A sua chave privada:\n" + privateKeyXml);
                break;

            case "2": //Ver Public Key
                var publicKeyXml = File.ReadAllText(publicKeyFile);
                Console.WriteLine("A sua chave publica:\n" + publicKeyXml);
                break;

            case "3": //Gerar novas Keys
                Op3_3(publicKeyFile, privateKeyFile);
                break;

            case "4": //VOLTAR
                return;
        }

        Console.WriteLine("\nPressione qualquer tecla para avançar");
        Console.ReadKey();
    }
}

while (true)
{
    ShowMenu();
    Console.Write("Opção: ");
    var op = Console.ReadLine();
    Console.Clear();

    switch (op)
    {
        case "1": //Comprimir Ficheiro
            Op1();
            break;

        case "2": //Descomprimir Ficheiro
            Op2();
            break;

        case "3": //Defenições
            Op3();
            break;

        case "4": //Dar o fora daqui
            return;
    }
}

// comp.Compress(targetDirectoryArg: @"D:\Users\jptin\Desktop\Programação\só testando utad", pathToSave: @"D:\Users\jptin\Desktop\Programação\", password: "oi");
// comp.Decompress(fileWithPath: @"D:\Users\jptin\Desktop\Programação\só testando utad.hajr", targetDirectoryArg: @"D:\Users\jptin\Desktop\Programação\", password: "oi");

// pasta default: ver raiz do projeto visual studio
// comp.Compress();
// comp.Decompress();