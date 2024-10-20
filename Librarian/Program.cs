using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Librarian
//   Напишите программу "Библиотекарь". Суть:
// Пользователю в консоли показывают меню: "1 - добавить книгу; 2 - вывести список непрочитанного; 3 - выйти"
//   Если он вводит 1, то далее ему пишут "Введите название книги:". Пользователь вводит название - книга запоминается в коллекции.
// В качестве коллекции стоит использовать ConcurrentDictionary<string, int> (для чего нужен int - см.далее).
// Если книга с таким названием уже была добавлена ранее - не добавлять и не обновлять ее.
// Автоматически возвращаемся в меню (снова выводим его в консоль).
//   Если вводит 2 - на экран выводится список всех ранее введенных книг и в конце - опять меню
//   Если вводит 3 - выходим из программы.
//   В выводимом списке книг надо выводить не только их названия, но и вычисленный процент,
// насколько она прочитана.Например: "Остров сокровищ - 15%".
// Для расчета процентов создаем второй поток, который в фоне постоянно перевычисляет проценты.
// Между каждой итерацией перевычисления он спит 1 секунду.Во время итерации перевычисления
// он берет коллекцию всех книг и по каждой вычисляет новый процент путем прибавления 1%
// к предыдущему значению(изначально 0%). Если дошли до 100% - удаляем эту книгу из списка.
// Таким образом, когда пользователь вызовет вывод списка он может получить что-то вроде:
// Любовь к жизни - 45%
// Приключения Мюнхгаузена - 17%
// Незнайка в Солнечном городе - 4%.
{
    class Librarian
    {
        // Коллекция книг с процентами прочтения.
        static ConcurrentDictionary<string, int> readingBook = new();

        // Токен отмены фонового потока.
        static CancellationTokenSource cancellationTokenSource = new();

        static void Main(string[] args)
        {
            // Запуск фонового потока чтения книг.
            Task.Run(() => UpdateBookReading(cancellationTokenSource.Token));

            // Главное меню программы.
            while (true)
            {
                Console.WriteLine("Меню библиотеки:  1 - Добавить книгу; 2 - Вывести список непрочитанного; 3 - Выйти");
                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddBook();
                        break;
                    case "2":
                        DisplayBooks();
                        break;
                    case "3":
                        // Остановка фонового потока и выход из программы.
                        cancellationTokenSource.Cancel();
                        return;
                    default:
                        Console.WriteLine("Команда не распознана, попробуйте снова.");
                        break;
                }
                // Защита от нажатия.
                Thread.Sleep(300);
            }
        }

        static void AddBook()
        // Метод добавления книги.
        {
            Console.WriteLine("Введите название книги:");
            string? bookTitle = Console.ReadLine();

            // Попытка добавить книгу.
            if (!readingBook.ContainsKey(bookTitle))
            {
                if (readingBook.TryAdd(bookTitle, 0))
                {
                    Console.WriteLine($"Книга \"{bookTitle}\" успешно добавлена.");
                }
                else
                {
                    Console.WriteLine($"Не удалось добавить книгу \"{bookTitle}\".");
                }
            }
            else
            {
                Console.WriteLine($"Книга с названием \"{bookTitle}\" уже существует.");
            }
        }

        static void DisplayBooks()
        // Метод вывода списка книг с процентом прочтения.
        {
            if (readingBook.IsEmpty)
            {
                Console.WriteLine("Список непрочитанных книг пуст.");
            }
            else
            {
                Console.WriteLine("Список книг с процентом прочтения:");
                foreach (var book in readingBook)
                {
                    Console.WriteLine($"{book.Key} - прочитана на {book.Value}%");
                }
            }
        }

        static void UpdateBookReading(CancellationToken token)
        // Фоновый метод чтения и удаления книг.
        {
            while (!token.IsCancellationRequested)
            {
                // Задержка на 1 секунду перед итерацией.
                Thread.Sleep(1000);

                // Перебор коллекции книг.
                foreach (var book in readingBook)
                {
                    // Чтение 1% книги.
                    readingBook.AddOrUpdate(book.Key, 0, (key, oldValue) => oldValue + 1);

                    // Удаление прочитанной книги.
                    if (readingBook[book.Key] >= 100)
                    {
                        readingBook.TryRemove(book.Key, out _);
                        Console.WriteLine($"--> Книга \"{book.Key}\" прочитана.");
                    }
                }
            }
        }
    }

}
