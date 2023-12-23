using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

//  Объедините две предыдущих работы (практические работы 2 и 3): поиск файла и поиск текста в файле написав утилиту которая
//  ищет файлы определенного расширения с указанным текстом. Рекурсивно. Пример вызова утилиты: utility.exe txt текст.
// протестированные запросы   dotnet run txt !,  dotnet run cs static

internal class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <расширение файла без точки>  <текст который хотим найти в файле>");
            return;
        }

        string extension = args[0];
        string searchText = args[1];
        string currentDirectory = Directory.GetCurrentDirectory();

        var files = FindFiles(currentDirectory, extension, searchText);
        foreach (var file in files)
        {
            Console.WriteLine(file);
        }
    }

    static IEnumerable<string> FindFiles(string directory, string extension, string searchText)
    {
        // Получить все файлы с указанным расширением в текущем каталоге.
        var filesWithExtension = Directory.EnumerateFiles(directory, $"*.{extension}", SearchOption.TopDirectoryOnly);
        var matchedFiles = new List<string>();

        foreach (var file in filesWithExtension)
        {
            //Прочитать все содержимое файла и проверяем, существует ли текст
            var text = File.ReadAllText(file);
            if (text.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                matchedFiles.Add(file);
            }
        }

        // рекурсивно ищем в подкаталогах
        var subDirs = Directory.GetDirectories(directory);
        foreach (var dir in subDirs)
        {
            matchedFiles.AddRange(FindFiles(dir, extension, searchText));
        }

        return matchedFiles;
    }

   
}