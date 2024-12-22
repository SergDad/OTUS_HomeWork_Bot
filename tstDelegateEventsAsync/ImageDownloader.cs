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

    internal class ImageDownLoader
    {
        #region 1 - 3 задание
        /* Добавьте события: в классе ImageDownloader в начале скачивания картинки и в конце скачивания картинки выкидывайте события (event) 
           ImageStarted и ImageCompleted соответственно.
           В основном коде программы подпишитесь на эти события, а в обработчиках их срабатываний выводите соответствующие уведомления в консоль: 
           "Скачивание файла началось" и "Скачивание файла закончилось".
         */
        public event Action? NotifyStart;
        public event Action? NotifyEnd;

        /// <summary>
        /// загрузка картинки
        /// </summary>
        /// <param name="remoteUri">URL адрес, для скачивания картинка</param>
        /// <param name="savePath">Каталог для сохранения картинки на ПК</param>
        /// <param name="savePath">Имя файла картинки на ПК</param>
        public static void Download(string remoteUri, string savePath, string nameFile)
        {
            string nameLoadFile = Path.Combine(savePath, nameFile);
            using (var myWebClient = new WebClient())
            {
                // Console.WriteLine("Качаю \"{0}\" из \"{1}\" .......\n\n", fileNameLoad, remoteUri);
                //NotifyStart?.Invoke();
                //myWebClient.DownloadFile(remoteUri, nameLoadFile);
                //NotifyEnd?.Invoke();
                //Console.WriteLine("Успешно скачал \"{0}\" из \"{1}\"", fileNameLoad, remoteUri);
            }

        }
        #endregion

        #region 4 задание
        /* Сделайте метод ImageDownloader.Download асинхронным. Если Вы скачивали картинку с использованием WebClient.DownloadFile,
          то используйте теперь WebClient.DownloadFileTaskAsync - он возвращает Task. */
        public async Task DownloadAsync(string remoteUrl, string savePath)
        {
            Thread.Sleep(3000);
            // не нашёл картинку большого размера будет имитация большой картинки
            using var myWebClient = new WebClient();
            await myWebClient.DownloadFileTaskAsync(remoteUrl, savePath);
        }
        #endregion
    }

}

