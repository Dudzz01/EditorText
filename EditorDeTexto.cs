using System;
using System.IO;
using System.Text;

namespace EditorDeTextoProject.EditorDeTextoMain
{
    class EditorDeTexto
    {
        public static void Menu()
        {
            short? option = null;

            while (option != 0)
            {
                Console.WriteLine($"Digite 1 para abrir o arquivo{Environment.NewLine}Digite 2 para editar e/ou criar um arquivo{Environment.NewLine}Digite 0 para sair do programa{Environment.NewLine}");

                option = short.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        OpenFile();
                        break;
                    case 2:
                        EditFile();
                        break;
                    case 0:
                        System.Environment.Exit(0);
                        break;
                    default:
                        System.Console.WriteLine("\nComando inválido, tente novamente.\n");
                        break;
                }
            }
        }

        public static void OpenFile()
        {
            System.Console.WriteLine($"{Environment.NewLine}Digite o caminho do arquivo que você quer abrir.{Environment.NewLine}");
            string path = Console.ReadLine();

            using (StreamReader file = new StreamReader(path))
            {
                string fileText = file.ReadToEnd();
                System.Console.WriteLine($"{Environment.NewLine}{fileText}{Environment.NewLine}");
            }
        }

        public static string OpenFile(string path)
        {
            string fileText;

            using (StreamReader file = new StreamReader(path))
            {
                fileText = file.ReadToEnd();

            }

            return fileText;
        }

        public static void EditFile()
        {

            //Se o arquivo não existir, ele será criado e editado
            Console.WriteLine("Digite o texto do arquivo abaixo!\n");
            StringBuilder textBuilder = new StringBuilder();

            while (true)
            {
                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    textBuilder.AppendLine();
                    Console.WriteLine(); // Pular linha no console

                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (textBuilder.Length > 0)
                    {
                        // Remover o último caractere digitado
                        textBuilder.Remove(textBuilder.Length - 1, 1);
                        // Apagar o caractere no console
                        Console.Write("\b \b");
                    }
                }
                else
                {
                    textBuilder.Append(key.KeyChar);
                    Console.Write(key.KeyChar); // Mostrar caractere digitado 

                }
            }

            string text = textBuilder.ToString();
            SaveFile(text, textBuilder);
        }

        public static void SaveFile(string text, StringBuilder textBuilder)
        {
            System.Console.WriteLine($"{Environment.NewLine}Qual o caminho/diretório para salvar o seu arquivo? (Ex: C:|dev){Environment.NewLine}");
            string path = Console.ReadLine(); // caminho/ diretório onde ficará salvo o arquivo. (Ex = Nosso arquivo ficará salvo no caminho: C:\Dev\Desktop)

            if (File.Exists(path))
            {
                string textOld = OpenFile(path);
                if (OpenFile(path) != null || OpenFile(path) != "")
                {
                    textBuilder.Remove(0, text.Length);
                    textBuilder.Append(textOld);
                    textBuilder.AppendLine();
                    textBuilder.Append(text);
                    text = textBuilder.ToString();
                }
            }


            using (StreamWriter file = new StreamWriter(path))
            {
                file.Write(text);

            }


            System.Console.WriteLine($"Arquivo salvo no {path} com sucesso!");
            Console.ReadLine();
        }
    }
}