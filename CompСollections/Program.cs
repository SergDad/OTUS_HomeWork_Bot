// Создать коллекции List, ArrayList и LinkedList.
// С помощью цикла for добавить в каждую 1 000 000 случайных значений с помощью класса Random.
// С помощью Stopwatch.Start() и Stopwatch.Stop() замерить длительность заполнения каждой коллекции и вывести значения на экран.
// Найти 496753-ий элемент, замерить длительность этого поиска и вывести на экран.
// Вывести на экран каждый элемент коллекции, который без остатка делится на 777. Вывести длительность этой операции для каждой коллекции.
// Укажите сколько времени вам понадобилось, чтобы выполнить это задание.

// Вопросы:
// 1. "1 000 000 случайных значений с помощью класса Random" - int, long, double?
// Далее идет - "элемент коллекции, который без остатка делится на 777". Т.е. элемент коллекции целое больше 777.
// Пусть член колекции будет типа int (или int < 1 000 000?).

using System.Collections.Generic;
using System.Diagnostics;

namespace CompСollections
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var lenArr = 1000;
            string elapsedTime;
            DateTime startTime, endTime;
            Stopwatch stopWatch = new Stopwatch();
            TimeSpan tsWatch, duration;
            Random random = new Random();
            // заготовка 1
            startTime = DateTime.Now;            
            stopWatch.Restart();

            Thread.Sleep(3000);

            stopWatch.Stop();
            endTime = DateTime.Now;
            tsWatch = stopWatch.Elapsed;
            duration = endTime - startTime;
            Console.WriteLine($"Операция заняла {duration.TotalMilliseconds} миллисекунд");
            Console.WriteLine($"RunTime {tsWatch.Milliseconds} миллисекунд");
            // заготовка 2
            stopWatch.Restart();

            Thread.Sleep(3000);

            stopWatch.Stop();
            tsWatch = stopWatch.Elapsed;
            Console.WriteLine($"Операция заняла {tsWatch.Milliseconds} миллисекунд");

            // Инициализация и заполнение Random Array 1
            stopWatch.Restart();
            int[] intArr1 = new int[lenArr];
            for (int i = 0; i < intArr1.Length; i++)
            {
                intArr1[i] = random.Next();
            }
            stopWatch.Stop();
            tsWatch = stopWatch.Elapsed;
            Console.WriteLine($"Операция заняла {tsWatch.Milliseconds} миллисекунд");
            // Инициализация и заполнение Random Array 2
            stopWatch.Restart();
            int[] intArr2 = new int[lenArr];
            random.Next(1000);
            stopWatch.Stop();
            tsWatch = stopWatch.Elapsed;
            Console.WriteLine($"Операция заняла {tsWatch.Milliseconds} миллисекунд");


        }
    }
}
