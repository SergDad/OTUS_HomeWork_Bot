using System.Net;

namespace DelegateEventsAsync
{
    class ImageDownloadEventArgs : EventArgs
    {
        public string FileName { get; }

        public ImageDownloadEventArgs(string fileName)
        {
            FileName = fileName;
        }
    }

    class ImageDownloader
    {
        // События для начала и окончания загрузки с информацией о файле
        public event EventHandler<ImageDownloadEventArgs>? ImageStarted;
        public event EventHandler<ImageDownloadEventArgs>? ImageCompleted;

        // Метод для скачивания картинки
        public void Download(string url, string savePath)
        {
            using (WebClient client = new WebClient())
            {
                // Генерация события начала загрузки с именем файла
                OnImageStarted(new ImageDownloadEventArgs(url));
                // Скачивание файла
                client.DownloadFile(url, savePath);
                // Генерация события окончания загрузки с именем файла
                OnImageCompleted(new ImageDownloadEventArgs(url));
            }
        }
        // Метод для скачивания картинки асинхронно
        public async Task DownloadAsync(string url, string savePath, CancellationToken token)
        {
            using (WebClient client = new WebClient())
            {
                // Генерация события начала загрузки с именем файла
                OnImageStarted(new ImageDownloadEventArgs(url));
                // Скачивание файла
                try
                {
                    await client.DownloadFileTaskAsync(url, savePath);
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine($"Access denied to read {url}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error starting async read for file {url}: {ex.Message}");
                }
                // Проверяем, отменен ли запрос
            if (token.IsCancellationRequested)
            {
                return;
            }
// Генерация события окончания загрузки с именем файла
                OnImageCompleted(new ImageDownloadEventArgs(url));
            }
        }

        // Метод для вызова события начала
        protected virtual void OnImageStarted(ImageDownloadEventArgs e)
        {
            ImageStarted?.Invoke(this, e);
        }

        // Метод для вызова события окончания
        protected virtual void OnImageCompleted(ImageDownloadEventArgs e)
        {
            ImageCompleted?.Invoke(this, e);
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



