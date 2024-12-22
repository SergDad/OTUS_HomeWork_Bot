using System;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

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
        static async Task Main(string[] args)
        {
            // Признак для вывода отладочных сообщений
            bool test = true;
            // Путь для сохранения загруженных изображений
            string saveDirectory = @"D:\Otus";
            // Имя файла с URL-адресами для зарузки
            string fileName = "Images.txt";
            List<string> urls = getImagesUrl(fileName, saveDirectory, test);

            // Создание экземпляра ImageDownloader
            ImageDownloader downloader = new ImageDownloader();
            // Подписка на события
            downloader.ImageStarted += (sender, e) => Console.WriteLine($"Начато скачивание файла '{e.FileName}");
            downloader.ImageCompleted += (sender, e) => Console.WriteLine($"Завершено скачивание файла '{e.FileName}");

            // Скачивание картинок по списку URL синхронно      
            if (test) Console.WriteLine("\n------Скачивание картинок по списку URL синхронно:");
            Stopwatch stopwatch = new();
            stopwatch.Start();
            loadImages(downloader, urls, saveDirectory);
            stopwatch.Stop();
            if (test) Console.WriteLine($"---- Картинки скачаны синхронно за {stopwatch.ElapsedMilliseconds} мс");

            // Скачивание всех картинок по URL асинхронно
            // Список асинхронных задач на скачивание
            var loadTasks = new List<Task>();
            if (test) Console.WriteLine("\n------Скачивание картинок по URL асинхронно:");
            stopwatch.Restart();
            foreach (string url in urls)
            {
                // Получение имени файла из URL
                fileName = Path.GetFileName(new Uri(url).AbsolutePath);
                // Полный путь для сохранения файла.
                string savePath = Path.Combine(saveDirectory, fileName);
                try
                {
                    // Формирование списка запущенных задач на скачивание.
                    loadTasks.Add(downloader.DownloadAsync(url, savePath));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка 1 при скачивании файла: {ex.Message}");
                }
            }
            // Ждем завершения всех задач.
            await Task.WhenAll(loadTasks);
            stopwatch.Stop();
            if (test) Console.WriteLine($"----Скачивание картинок по URL асинхронно за {stopwatch.ElapsedMilliseconds} мс");
        }


        static List<string> getImagesUrl(string fileName, string saveDirectory, bool test)
        {
            List<string> urls = new List<string>();
            string filePath = Path.Combine(saveDirectory, fileName);
            // Проверка, существует ли каталог, если нет — создаем его
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }
            try
            {
                // Чтение строк из файла в List<string>
                urls.AddRange(File.ReadAllLines(filePath));
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Файл с URL-адресами по адресу {filePath} не найден.");
                return urls;
            }
            if (test)
            {
                Console.WriteLine("Адреса для скачивания картинок:");
                foreach (string url in urls) Console.WriteLine($"  {url}");
            }
            return urls;
        }

        static void loadImages(ImageDownloader downloader, List<string> urls, string saveDirectory)
        {
            foreach (string url in urls)
            {
                // Получение имени файла из URL
                string fileName = Path.GetFileName(new Uri(url).AbsolutePath);
                // Полный путь для сохранения файла
                string savePath = Path.Combine(saveDirectory, fileName);
                // Скачивание файла
                try
                {
                    downloader.Download(url, savePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при скачивании файла: {ex.Message}");
                }
            }
        }
        static async List<Task>() loadImagesAsync(ImageDownloader downloader, List<string> urls, string saveDirectory)
        var loadTasks = new ;
            if (test) Console.WriteLine("\n------Скачивание картинок по URL асинхронно:");
        stopwatch.Restart();
            foreach (string url in urls)
            {
                // Получение имени файла из URL
                fileName = Path.GetFileName(new Uri(url).AbsolutePath);
                // Полный путь для сохранения файла.
                string savePath = Path.Combine(saveDirectory, fileName);
                try
                {
                    // Формирование списка запущенных задач на скачивание.
                    loadTasks.Add(downloader.DownloadAsync(url, savePath));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка 1 при скачивании файла: {ex.Message}");
                }
            }
    }





    //class ImageDownloader
    //{
    //    // События для начала и окончания загрузки
    //    public event EventHandler? ImageStarted;
    //    public event EventHandler? ImageCompleted;

    //    // Метод для скачивания картинки
    //    public void Download(string url, string filename)
    //    {
    //        using (WebClient client = new WebClient())
    //        {
    //            // Генерация события начала загрузки
    //            OnImageStarted(EventArgs.Empty);

    //            // Скачивание файла
    //            client.DownloadFile(url, filename);

    //            // Генерация события окончания загрузки
    //            OnImageCompleted(EventArgs.Empty);
    //        }
    //    }

    //    // Метод для вызова события начала
    //    protected virtual void OnImageStarted(EventArgs e)
    //    {
    //        ImageStarted?.Invoke(this, e);
    //    }

    //    // Метод для вызова события окончания
    //    protected virtual void OnImageCompleted(EventArgs e)
    //    {
    //        ImageCompleted?.Invoke(this, e);
    //    }
    //}

    //class Program
    //{
    //    static void Main()
    //    {
    //        // Создание экземпляра ImageDownloader
    //        ImageDownloader downloader = new ImageDownloader();

    //        // Подписка на события
    //        downloader.ImageStarted += (sender, e) =>
    //        {
    //            Console.WriteLine("Скачивание файла началось");
    //        };

    //        downloader.ImageCompleted += (sender, e) =>
    //        {
    //            Console.WriteLine("Скачивание файла закончилось");
    //        };

    //        // Чтение URL-адресов из файла Images.txt
    //        string basePath = @"D:\Otus\";
    //        string fileName = "Images.txt";
    //        string filePath = Path.Combine(basePath, fileName);
    //        // string filePath = @"D:\Otus\Images.txt"; // Файл с URL
    //        List<string> urls = new List<string>();

    //        try
    //        {
    //            urls.AddRange(File.ReadAllLines(filePath)); // Чтение всех строк из файла
    //            foreach (string url in urls)
    //            {
    //                Console.WriteLine(url);
    //            }
    //        }
    //        catch (FileNotFoundException)
    //        {
    //            Console.WriteLine("Файл с URL-адресами не найден.");
    //            return;
    //        }

    //        // Скачивание картинок по каждому URL
    //        foreach (string url in urls)
    //        {
    //            try
    //            {
    //                // Получение имени файла из URL

    //                string fileName = "Images.txt";
    //                string filePath = Path.Combine(basePath, fileName);
    //                string fileName1 = Path.GetFileName(new Uri(url).AbsolutePath);

    //                // Скачивание файла
    //                downloader.Download(url, fileName1);
    //            }
    //            catch (Exception ex)
    //            {
    //                Console.WriteLine($"Ошибка при скачивании файла: {ex.Message}");
    //            }
    //        }
    //    }
    //}

    //internal class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        task1_3();
    //        task4(); //4 задание
    //        task5(); // 5 задание
    //    }

    //    static void Start()
    //    {
    //        Console.WriteLine("Скачивание файла началось");
    //        Console.WriteLine(DateTime.Now.Microsecond);
    //        // Log.Info("Скачивание файла началось");
    //        // Image.GenerateNewImage();
    //    }

    //    static void Start2()
    //    {
    //        Console.WriteLine("Скачивание файла началось2");
    //        // Log.Info("Скачивание файла началось");
    //        // Image.GenerateNewImage();
    //    }

    //    static void Start3()
    //    {
    //        Console.WriteLine("Скачивание файла началось3");
    //        // Log.Info("Скачивание файла началось");
    //        // Image.GenerateNewImage();
    //    }

    //    #region 1 - 3 задание
    //    static void task1_3()
    //    {
    //        ImageDownLoader image = new ImageDownLoader();
    //        image.NotifyStart += Start;
    //        image.NotifyStart += Start2;
    //        image.NotifyStart += Start3;
    //        image.NotifyEnd += image.ImageFinished;

    //        image.NotifyStart -= Start;

    //        image.Download(
    //            remoteUri: @"https://a.d-cd.net/wgAAAgPivuA-1920.jpg",
    //            savePath: @"C:\Users\dejsv\Downloads\C--main\ДЗ\09 HomeWork",
    //            nameFile: "1.jpg"
    //        );
    //    }
    //    #endregion

    //    #region 4 задание
    //    /*
    //     Сделайте метод ImageDownloader.Download асинхронным. Если Вы скачивали картинку с использованием WebClient.DownloadFile,
    //     то используйте теперь WebClient.DownloadFileTaskAsync - он возвращает Task.
    //        В конце работы программы выводите теперь текст "Нажмите клавишу A для выхода или любую другую клавишу для проверки статуса скачивания" 
    //        и ожидайте нажатия любой клавиши. Если нажата клавиша "A" - выходите из программы. В противном случае выводите состояние загрузки картинки 
    //        (True - загружена, False - нет). Проверить состояние можно через вызов Task.IsCompleted.
    //         */
    //    async static Task task4()
    //    {
    //        //вызывается в основном теле main
    //        bool flagStop = false;

    //        var cts = new CancellationTokenSource();
    //        // для принудительной остановки потока
    //        ImageDownLoader imageDownLoader = new ImageDownLoader();

    //        var s = Task.Run(() => imageDownLoader.Download(
    //            remoteUri: "https://a.d-cd.net/wgAAAgPivuA-1920.jpg",
    //            savePath: "C:\\Users\\Ilya\\Desktop\\folderLoadImg\\ansys.jpg"
    //            ), cts.Token);

    //        while (!flagStop)
    //        {
    //            Console.WriteLine("Нажмите клавишу A для выхода или любую другую клавишу для проверки статуса скачивания");
    //            string key = Console.ReadLine().ToUpper();
    //            if (key == "A" || key == "А")
    //            {//A- анг раскладка;  А -рус раскладка
    //                string status = s.IsCompleted ? "загружена" : "нет";
    //                Console.WriteLine($"состояние загрузки картинки: {status}, но я всё равно выйду!");
    //                flagStop = true;
    //                cts.Cancel();
    //                cts.Dispose();
    //            }
    //            else
    //            {
    //                string status = s.IsCompleted ? "загружена" : "нет";
    //                Console.WriteLine($"состояние загрузки картинки: {status}");
    //            }

    //        }

    //    }
    //    #endregion

    //    #region 5 задание
    //    /*
    //     Модифицируйте программу таким образом, чтобы она скачивала сразу 10 картинок одновременно, останавливая одновременно все потоки 
    //    по нажатию кнопки "A". По нажатию других кнопок выводить на экран список загружаемых картинок и их состояния - загружены или нет, 
    //    аналогично выводу для одной картинки.
    //     */
    //    //TODO спросить в понедельник почему требуется ставить задержку!
    //    async static Task task5()
    //    {
    //        string basePath = @"D:\Otus\";
    //        string fileName = "Images.txt";
    //        string filePath = Path.Combine(basePath, fileName);
    //        List<string> pictureURLs = new List<string>();
    //        pictureURLs = [..File.ReadAllLines(filePath)];
    //        bool flagStop = false;
    //        Console.WriteLine("Нажмите клавишу A для выхода или любую другую клавишу для проверки статуса скачивания");
    //        var cts = new CancellationTokenSource();
    //        List<ImageDownLoader> imageList = new List<ImageDownLoader>();
    //        List<CancellationTokenSource> tocenCansel = new List<CancellationTokenSource>();

    //        ImageDownLoader imageDownLoader = new ImageDownLoader();
    //        /// код с циклом
    //        List<Task> tasks = new List<Task>();
    //        int countImg = 0;
    //        for (int i = 0; i < 10; i++)
    //        {
    //            int i1 = i;
    //            // спросить в понедельник почему требуется ставить задержку
    //            tasks.Add(Task.Run(() => imageDownLoader.DownloadAsync
    //            (
    //                    remoteUrl: "https://a.d-cd.net/wgAAAgPivuA-1920.jpg",
    //                    savePath: $"C:\\Users\\Ilya\\Desktop\\folderLoadImg\\Ansys{i1.ToString()}.jpg"
    //            ),
    //            cts.Token));

    //        }

    //        while (!flagStop)
    //        {
    //            Console.WriteLine("Нажмите клавишу A для выхода или любую другую клавишу для проверки статуса скачивания");
    //            string key = Console.ReadLine().ToUpper();
    //            if (key == "A" || key == "А")
    //            {//A- анг раскладка;  А -рус раскладка

    //                cts.Cancel();
    //                cts.Dispose();
    //                Console.WriteLine($"выход!");
    //                flagStop = true;
    //            }
    //            else
    //            {
    //                StringBuilder stringBuilder = new StringBuilder();
    //                countImg = 0;
    //                foreach (var task in tasks)
    //                {

    //                    stringBuilder.AppendLine($"IMG {countImg}, status: {task.IsCompleted}");
    //                    countImg++;

    //                }
    //                Console.WriteLine($"Скачалась ли картинка: {stringBuilder.ToString()}");
    //            }
    //        }

    //    }

    //    public static void ImageStarted()
    //    {
    //        Console.WriteLine("Скачивание файла началось");
    //    }

    //    public static void ImageFinished()
    //    {
    //        Console.WriteLine("Скачивание файла закончилось");

    //    }
    //    #endregion
    //}
}



