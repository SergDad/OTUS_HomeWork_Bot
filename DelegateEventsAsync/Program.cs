using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DelegateEventsAsync
{
    //     Домашнее задание.  Делегаты, Event-ы, добавляем асинхронное выполнение
    //
    // 1. Напишите класс ImageDownloader.
    // В этом классе должен быть метод Download, который скачивает картинку из интернета.
    // Для загрузки картинки можно использовать примерно такой код: https://dotnetfiddle.net/5oT1Hi
    // Откуда будем качать
    //   string remoteUri = "https://webneel.com/daily/sites/default/files/images/daily/08-2018/1-nature-photography-spring-season-mumtazshamsee.jpg";
    // Как назовем файл на диске:
    //   string fileName = "bigimage.jpg";
    // Качаем картинку в текущую директорию
    //   var myWebClient = new WebClient();
    //   Console.WriteLine("Качаю \"{0}\" из \"{1}\" .......\n\n", fileName, remoteUri);
    //   myWebClient.DownloadFile(remoteUri, fileName);

    //             Внимание!  Microsoft: 
    //      WebClient устарел, и его не следует использовать для новой разработки.
    //      !! Вместо этого используйте HttpClient. !!

    //   Console.WriteLine("Успешно скачал \"{0}\" из \"{1}\"", fileName, remoteUri);
    // 2. Создайте экземпляр этого класса и вызовите скачивание большой картинки,
    // например, https://effigis.com/wp-content/uploads/2015/02/Iunctus_SPOT5_5m_8bit_RGB_DRA_torngat_mountains_national_park_8bits_1.jpg
    //             Внимание! Ссылка не рабочая!   
    // В конце работы программы выведите в консоль "Нажмите любую клавишу для выхода" и ожидайте нажатия любой клавиши.
    // 3. Добавьте события: в классе ImageDownloader в начале скачивания картинки
    // и в конце скачивания картинки выкидывайте события(event)
    // ImageStarted и ImageCompleted соответственно.
    // В основном коде программы подпишитесь на эти события, а в обработчиках их срабатываний
    // выводите соответствующие уведомления в консоль:
    // "Скачивание файла началось" и "Скачивание файла закончилось".
    // 4. Сделайте метод ImageDownloader.Download асинхронным.
    // Если Вы скачивали картинку с использованием WebClient.DownloadFile,
    // то используйте теперь WebClient.DownloadFileTaskAsync - он возвращает Task.
    // В конце работы программы выводите теперь текст
    // "Нажмите клавишу A для выхода или любую другую клавишу для проверки статуса скачивания"
    // и ожидайте нажатия любой клавиши. Если нажата клавиша "A" - выходите из программы.
    // В противном случае выводите состояние загрузки картинки (True - загружена, False - нет).
    // Проверить состояние можно через вызов Task.IsCompleted.
    // Поздравляю! Ваша загрузка картинки работает асинхронно с основным потоком консоли.
    // 5. Модифицируйте программу таким образом, чтобы она скачивала сразу 10 картинок одновременно,
    // останавливая одновременно все потоки по нажатию кнопки "A".
    // По нажатию других кнопок выводить на экран список загружаемых картинок
    // и их состояния - загружены или нет, аналогично выводу для одной картинки.

    class Program
    {
        // Состояние загрузки картинок.
        private static Dictionary<string, bool> downloadStatus = [];

        static async Task Main(string[] args)
        {
            // Каталог для сохранения картинок.
            string saveDirectory = @"D:\Image";
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }

            // Чтение URL-адресов картинок из файла.
            string fileName = "Images.txt";
            string filePath = Path.Combine(saveDirectory, fileName);
            List<string> urls = new List<string>();
            try
            {
                urls.AddRange(File.ReadAllLines(filePath));
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Файл с URL-адресами по адресу {filePath} не найден.");
                return;
            }

            using (ImageDownloader? downloader = new())
            {
                // Подписка на события.
                downloader.ImageStarted += (fileName) =>
                {
                    Console.WriteLine($"Скачивание файла {fileName} началось");
                    downloadStatus[fileName] = false; // Устанавливаем состояние "не загружено".
                };

                downloader.ImageCompleted += (fileName) =>
                {
                    Console.WriteLine($"Скачивание файла {fileName} закончилось");
                    downloadStatus[fileName] = true; // Устанавливаем состояние "не загружено".
                };

                CancellationTokenSource cts = new CancellationTokenSource();

                // Синхронное скачивание файлов.
                Console.WriteLine("===> Начинается синхронное скачивание файлов...");
                downloader.Download(urls, saveDirectory, cts.Token);
                Console.WriteLine("Синхронное скачивание завершено.");

                //// Асинхронное скачивание файлов.
                Console.WriteLine("===> Начинается асинхронное скачивание файлов...");
                var downloadTasks = await downloader.DownloadAsync(urls, saveDirectory, cts.Token);

                // Запуск задачи для ожидания нажатия клавиш.
                Task.Run(() =>
                {
                    Console.WriteLine("--->Нажмите клавишу А для остановки загрузки или любую другую для вывода статуса");
                    while (true)
                    {
                        var key = Console.ReadKey(true).Key;
                        if (key == ConsoleKey.A || key == ConsoleKey.F)
                        {
                            Console.WriteLine("Остановка загрузки...");
                            cts.Cancel();
                            Environment.Exit(0);
                        }
                        else
                        {
                            Console.WriteLine("Текущее состояние загрузки картинок:");
                            foreach (var ds in downloadStatus)
                            {
                                Console.WriteLine($"   cостояние загрузки {ds.Key}: {ds.Value}");
                            }
                        }
                        Console.WriteLine("--->Нажмите клавишу А для остановки загрузки или любую другую для вывода статуса");
                    }
                });

                // Ожидание завершения всех асинхронных задач.
                await Task.WhenAll(downloadTasks);
                Console.WriteLine("Асинхронное скачивание завершено.");
                downloader.Dispose();
            }
        }
    }
}
