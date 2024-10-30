using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DelegateEventsAsync
{
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
    //   Console.WriteLine("Успешно скачал \"{0}\" из \"{1}\"", fileName, remoteUri);

    // 3. Добавьте события: в классе ImageDownloader в начале скачивания картинки
    // и в конце скачивания картинки выкидывайте события(event)
    // ImageStarted и ImageCompleted соответственно.

    // 4. Сделайте метод ImageDownloader.Download асинхронным.
    // Если Вы скачивали картинку с использованием WebClient.DownloadFile,
    // то используйте теперь WebClient.DownloadFileTaskAsync - он возвращает Task.

    public class ImageDownloader:IDisposable
    {
        // Описание событий (event).
        public event Action<string>? ImageStarted;
        public event Action<string>? ImageCompleted;
        
        private readonly HttpClient _httpClient;

        public ImageDownloader() 
        { 
         _httpClient = new HttpClient();
        }
        
        // Метод для синхронного скачивания.
        public void Download(List<string> urls, string directory, CancellationToken cancellationToken)
        {
            foreach (var url in urls)
            {
                string fileName = Path.Combine(directory, Path.GetFileName(new Uri(url).LocalPath));
                ImageStarted?.Invoke(fileName);
                try
                {
                    using (var response = _httpClient.GetAsync(url, cancellationToken).Result)
                    {
                        response.EnsureSuccessStatusCode();


                        var imageBytes = response.Content.ReadAsByteArrayAsync().Result;
                        if (cancellationToken.IsCancellationRequested)
                            break;

                        using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                        {
                            fileStream.Write(imageBytes, 0, imageBytes.Length);
                        }
                        ImageCompleted?.Invoke(fileName);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при скачивании файла {fileName}: {ex.Message}");
                }
            }
        }

        //  Метод для асинхронного скачивания
        public async Task<List<Task>> DownloadAsync(List<string> urls, string directory, CancellationToken cancellationToken)
        {
            List<Task> tasks = new List<Task>();
            foreach (var url in urls)
            {
                string fileName = Path.Combine(directory, Path.GetFileName(new Uri(url).LocalPath));
                tasks.Add(Task.Run(async () =>
                {
                    ImageStarted?.Invoke(fileName);
                    try
                    {
                        var imageBytes = await _httpClient.GetByteArrayAsync(url);
                        if (cancellationToken.IsCancellationRequested)
                            return;
                        await File.WriteAllBytesAsync(fileName, imageBytes);
                        ImageCompleted?.Invoke(fileName);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка при скачивании файла {fileName}: {ex.Message}");
                    }
                }, cancellationToken));
            }
            return tasks;
        }

        // Реализация IDisposable для правильного завершения работы с HttpClient
        public void Dispose()
        {
            Console.WriteLine($"Вызов Dispose {this.ToString}");
            //_httpClient.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}