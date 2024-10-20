using System;
using System.IO;
using System.Text;
// using System.Text.Unicode;
using System.Threading.Tasks;
// using static System.Net.WebRequestMethods;
namespace WriteAndReadFile
//   Создание консольного приложение, записывающее и считывающее информацию в\из файл(а).
// Цель:
// компилирующееся без ошибок приложение, файлы по заданному пути, консоль со значениями файлов.
// Описание/Пошаговая инструкция выполнения домашнего задания:
// 1. Создать директории c:\Otus\TestDir1 и c:\Otus\TestDir2 с помощью класса DirectoryInfo.
// 2. В каждой директории создать несколько файлов File1...File10 с помощью класса File.
// 3. В каждый файл записать его имя в кодировке UTF8. Учесть, что файл может быть удален, либо отсутствовать права на запись.
// 4. Каждый файл дополнить текущей датой (значение DateTime.Now) любыми способами: синхронно и\или асинхронно.
// 5. Прочитать все файлы и вывести на консоль: имя_файла: текст + дополнение.
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string basePath = @"D:\Otus";
            string[] directories = { Path.Combine(basePath, "TestDir1"), Path.Combine(basePath, "TestDir2") };
            bool test = false;

            // 1. Создание директорий.
            foreach (var dir in directories)
            {
                var directoryInfo = new DirectoryInfo(dir);
                if (!directoryInfo.Exists)
                {
                    directoryInfo.Create();
                    if (test) Console.WriteLine($"Directory {dir} created.");
                }
                else
                {
                    Console.WriteLine($"Directory {dir} already exists.");
                }
            }

            // 2. Создание файлов и запись имени файла в UTF8 с проверкой на наличие прав.
            foreach (var dir in directories)
            {
                for (int i = 1; i <= 10; i++)
                {
                    string fileName = $"File{i}.txt";
                    string filePath = Path.Combine(dir, fileName);
                    try
                    {
                        File.WriteAllText(filePath, fileName, new UTF8Encoding(false));
                        if (test) Console.WriteLine($"File {fileName} created in {dir}.");
                    }
                    catch (UnauthorizedAccessException)
                    {
                        Console.WriteLine($"Access denied to write {filePath}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error creating file {filePath}: {ex.Message}");
                    }
                }
            }

            // 3. Дополнение файла текущей датой синхронно
            foreach (var dir in directories)
            {
                foreach (var file in Directory.GetFiles(dir))
                {
                    try
                    {
                        File.AppendAllText(file, $"\nDate (sync): {DateTime.Now}", Encoding.UTF8);
                        if (test) Console.WriteLine($"File {Path.GetFileName(file)} updated with date (sync) in {dir}.");
                    }
                    catch (UnauthorizedAccessException)
                    {
                        Console.WriteLine($"Access denied to write {file}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error updating file {file}: {ex.Message}");
                    }
                }
            }

            // 4. Дополнение файла текущей датой асинхронно
            var writeTasks = new List<Task>();
            foreach (var dir in directories)
            {
                foreach (var file in Directory.GetFiles(dir))
                {
                    writeTasks.Add(AppendDateAsync(file));
                    if (test) Console.WriteLine($"Started async update for file {Path.GetFileName(file)} in {dir}.");
                }
            }

            // Ждем завершения всех задач асинхронного обновления
            await Task.WhenAll(writeTasks);
            if (test) Console.WriteLine("All async updates completed.");

            // 5. Асинхронное чтение всех файлов и вывод их содержимого после завершения
            Console.WriteLine("Чтение всех файлов и вывод на консоль: имя_файла: текст + дополнение");
            var readTasks = new List<Task>();
            foreach (var dir in directories)
            {
                foreach (var file in Directory.GetFiles(dir))
                {
                    try
                    {
                        readTasks.Add(ReadAndDisplayFileAsync(file));
                    }
                    catch (UnauthorizedAccessException)
                    {
                        Console.WriteLine($"Access denied to read {file}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error starting async read for file {file}: {ex.Message}");
                    }
                }
            }
            // Ждем завершения всех задач чтения
            await Task.WhenAll(readTasks);
            if (test) Console.WriteLine("All files read and displayed.");
        }

        // Асинхронное добавление даты в файл с обработкой исключений
        static async Task AppendDateAsync(string filePath)
        {
            try
            {
                string content = $"\nDate (async): {DateTime.Now}";
                byte[] encodedText = Encoding.UTF8.GetBytes(content);

                using (FileStream fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    await fileStream.WriteAsync(encodedText, 0, encodedText.Length);
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine($"Access denied to write {filePath}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"I/O error occurred while writing to {filePath}: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while writing to {filePath}: {ex.Message}");
            }
        }
        // Асинхронное чтение файла и вывод его содержимого на консоль
        static async Task ReadAndDisplayFileAsync(string filePath)
        {
            using FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

            byte[] buffer = new byte[fileStream.Length];
            await fileStream.ReadAsync(buffer, 0, buffer.Length);
            string content = Encoding.UTF8.GetString(buffer);
            Console.WriteLine($"{Path.GetFileName(filePath)}: {content}");
        }
    }

}
