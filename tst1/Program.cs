using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

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
namespace CollectionPerfomance
{
    class Program
    {
        static void Main()
        {
            // Создание коллекций
            List<int> list = new List<int>();
            ArrayList arrayList = new ArrayList();
            LinkedList<int> linkedList = new LinkedList<int>();

            Random random = new Random();
            Stopwatch stopwatch = new Stopwatch();

            // Заполнение List
            stopwatch.Start();
            for (int i = 0; i < 1000000; i++)
            {
                list.Add(random.Next());
            }
            stopwatch.Stop();
            Console.WriteLine($"Время заполнения List: {stopwatch.ElapsedMilliseconds} мс");

            // Заполнение ArrayList
            stopwatch.Restart();
            for (int i = 0; i < 1000000; i++)
            {
                arrayList.Add(random.Next());
            }
            stopwatch.Stop();
            Console.WriteLine($"Время заполнения ArrayList: {stopwatch.ElapsedMilliseconds} мс");

            // Заполнение LinkedList
            stopwatch.Restart();
            for (int i = 0; i < 1000000; i++)
            {
                linkedList.AddLast(random.Next());
            }
            stopwatch.Stop();
            Console.WriteLine($"Время заполнения LinkedList: {stopwatch.ElapsedMilliseconds} мс");

            // Поиск 496753-го элемента в List
            stopwatch.Restart();
            int listElement = list[496752];
            stopwatch.Stop();
            Console.WriteLine($"Время поиска 496753-го элемента в List: {stopwatch.ElapsedTicks} тиков");

            // Поиск 496753-го элемента в ArrayList
            stopwatch.Restart();
            int arrayListElement = (int)arrayList[496752];
            stopwatch.Stop();
            Console.WriteLine($"Время поиска 496753-го элемента в ArrayList: {stopwatch.ElapsedTicks} тиков");

            // Поиск 496753-го элемента в LinkedList
            stopwatch.Restart();
            int linkedListElement = linkedList.ElementAt(496752);
            stopwatch.Stop();
            Console.WriteLine($"Время поиска 496753-го элемента в LinkedList: {stopwatch.ElapsedTicks} тиков");

            // Вывод элементов List, которые делятся на 777
            stopwatch.Restart();
            foreach (var item in list)
            {
                int count = 0;
                if (item % 777 == 0)
                {
                    // Console.WriteLine(item);
                    count++;
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"Время вывода элементов из List, которые делятся на 777: {stopwatch.ElapsedMilliseconds} мс");

            // Вывод элементов ArrayList, которые делятся на 777
            stopwatch.Restart();
            foreach (int item in arrayList)
            {
                int count = 0;
                if (item % 777 == 0)
                {
                    // Console.WriteLine(item);
                    count++;
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"Время вывода элементов из ArrayList, которые делятся на 777: {stopwatch.ElapsedMilliseconds} мс");

            // Вывод элементов LinkedList, которые делятся на 777
            stopwatch.Restart();
            foreach (var item in linkedList)
            {
                int count = 0;
                if (item % 777 == 0)
                {
                    // Console.WriteLine(item);
                    count++;
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"Время вывода элементов из LinkedList, которые делятся на 777: {stopwatch.ElapsedMilliseconds} мс");
        }
    }
}
