using System;
using System.Collections.Generic;
using System.IO;
namespace Poem
{
    //   Напишите программу "Дом, который построил Джек".
    // В программе должна быть коллекция строк. Каждая строка - строка стихотворения "Дом, который построил Джек".
    // Изначально коллекция пустая.
    // Также в программе есть 9 классов - Part1, Part2, Part3, ..., Part9
    // В каждом классе PartN есть метод AddPart, который на вход принимает коллекцию строк,
    // добавляет в нее новые строки и сохраняет получившуюся коллекцию в свойство "Poem".
    // Требуется это делать так, чтобы исходная коллекция не изменилась.
    // Какие именно строки добавляет каждый класс посмотрите здесь
    // - https://russkaja-skazka.ru/dom-kotoryiy-postroil-dzhek-stihi-samuil-marshak/
    // (например Part3 добавляет третий параграф стихотворения)
    // Надо создать экземпляры этих классов, а затем последовательно вызвать
    // каждый из методов AddPart, передавай в него результат вызова предыдущего метода,
    // примерно так: 'MyPart3(MyPart2.Poem)'
    // В конце работы программы надо вывести значение свойства "Poem" у каждого из классов
    // и убедиться, что изменяя коллекцию в одном классе Вы не затрагивали коллекцию в предыдущем.

    class Part1
    {
        public List<string> Poem { get; private set; }

        public Part1()
        {
            Poem = new List<string>();
        }

        public List<string> AddPart(List<string> poem)
        {
            var newPoem = new List<string>(poem);
            newPoem.Add("Вот дом, который построил Джек.");
            Poem = newPoem;
            return Poem;
        }
    }

    class Part2
    {
        public List<string> Poem { get; private set; }

        public Part2()
        {
            Poem = new List<string>();
        }

        public List<string> AddPart(List<string> poem)
        {
            var newPoem = new List<string>(poem);
            newPoem.Add("А это пшеница, которая в тёмном чулане хранится в доме, который построил Джек.");
            Poem = newPoem;
            return Poem;
        }
    }

    class Part3
    {
        public List<string> Poem { get; private set; }

        public Part3()
        {
            Poem = new List<string>();
        }

        public List<string> AddPart(List<string> poem)
        {
            var newPoem = new List<string>(poem);
            newPoem.Add("А это весёлая птица-синица, которая часто ворует пшеницу,");
            newPoem.Add("Которая в тёмном чулане хранится в доме, который построил Джек.");
            Poem = newPoem;
            return Poem;
        }
    }

    class Part4
    {
        public List<string> Poem { get; private set; }

        public Part4()
        {
            Poem = new List<string>();
        }

        public List<string> AddPart(List<string> poem)
        {
            var newPoem = new List<string>(poem);
            newPoem.Add("А это кот, который пугает и ловит синицу,");
            newPoem.Add("Которая часто ворует пшеницу, которая в тёмном чулане хранится в доме, который построил Джек.");
            Poem = newPoem;
            return Poem;
        }
    }

    class Part5
    {
        public List<string> Poem { get; private set; }

        public Part5()
        {
            Poem = new List<string>();
        }

        public List<string> AddPart(List<string> poem)
        {
            var newPoem = new List<string>(poem);
            newPoem.Add("Вот пёс без хвоста, который за шиворот треплет кота,");
            newPoem.Add("Который пугает и ловит синицу, которая часто ворует пшеницу,");
            newPoem.Add("Которая в тёмном чулане хранится в доме, который построил Джек.");
            Poem = newPoem;
            return Poem;
        }
    }

    class Part6
    {
        public List<string> Poem { get; private set; }

        public Part6()
        {
            Poem = new List<string>();
        }

        public List<string> AddPart(List<string> poem)
        {
            var newPoem = new List<string>(poem);
            newPoem.Add("А это корова безрогая, лягнувшая старого пса без хвоста,");
            newPoem.Add("Который за шиворот треплет кота, который пугает и ловит синицу,");
            newPoem.Add("Которая часто ворует пшеницу, которая в тёмном чулане хранится в доме, который построил Джек.");
            Poem = newPoem;
            return Poem;
        }
    }

    class Part7
    {
        public List<string> Poem { get; private set; }

        public Part7()
        {
            Poem = new List<string>();
        }

        public List<string> AddPart(List<string> poem)
        {
            var newPoem = new List<string>(poem);
            newPoem.Add("А это старушка, седая и строгая, которая доит корову безрогую,");
            newPoem.Add("Лягнувшую старого пса без хвоста, который за шиворот треплет кота,");
            newPoem.Add("Который пугает и ловит синицу, которая часто ворует пшеницу,");
            newPoem.Add("Которая в тёмном чулане хранится в доме, который построил Джек.");
            Poem = newPoem;
            return Poem;
        }
    }

    class Part8
    {
        public List<string> Poem { get; private set; }

        public Part8()
        {
            Poem = new List<string>();
        }

        public List<string> AddPart(List<string> poem)
        {
            var newPoem = new List<string>(poem);
            newPoem.Add("А это ленивый и толстый пастух, который бранится с коровницей строгою,");
            newPoem.Add("Которая доит корову безрогую, лягнувшую старого пса без хвоста,");
            newPoem.Add("Который за шиворот треплет кота, который пугает и ловит синицу,");
            newPoem.Add("Которая часто ворует пшеницу, которая в тёмном чулане хранится в доме, который построил Джек.");
            Poem = newPoem;
            return Poem;
        }
    }

    class Part9
    {
        public List<string> Poem { get; private set; }

        public Part9()
        {
            Poem = new List<string>();
        }

        public List<string> AddPart(List<string> poem)
        {
            var newPoem = new List<string>(poem);
            newPoem.Add("Вот два петуха, которые будят того пастуха,");
            newPoem.Add("Который бранится с коровницей строгою, которая доит корову безрогую,");
            newPoem.Add("Лягнувшую старого пса без хвоста, который за шиворот треплет кота,");
            newPoem.Add("Который пугает и ловит синицу, которая часто ворует пшеницу,");
            newPoem.Add("Которая в тёмном чулане хранится в доме, который построил Джек.");
            Poem = newPoem;
            return Poem;
        }
    }

    class Program
    {
        static void Main()
        {
            // Изначальная пустая коллекция
            var initialPoem = new List<string>();

            // Создание экземпляров классов
            var myPart1 = new Part1();
            var myPart2 = new Part2();
            var myPart3 = new Part3();
            var myPart4 = new Part4();
            var myPart5 = new Part5();
            var myPart6 = new Part6();
            var myPart7 = new Part7();
            var myPart8 = new Part8();
            var myPart9 = new Part9();

            // Последовательное добавление частей
            myPart1.AddPart(initialPoem);
            myPart2.AddPart(myPart1.Poem);
            myPart3.AddPart(myPart2.Poem);
            myPart4.AddPart(myPart3.Poem);
            myPart5.AddPart(myPart4.Poem);
            myPart6.AddPart(myPart5.Poem);
            myPart7.AddPart(myPart6.Poem);
            myPart8.AddPart(myPart7.Poem);
            myPart9.AddPart(myPart8.Poem);

            // Вывод  каждой коллекции.
            Console.WriteLine("initialPoem:");
            foreach (var line in initialPoem) Console.WriteLine(line);

            Console.WriteLine("\nPart 1:");
            foreach (var line in myPart1.Poem) Console.WriteLine(line);

            Console.WriteLine("\nPart 2:");
            foreach (var line in myPart2.Poem) Console.WriteLine(line);

            Console.WriteLine("\nPart 3:");
            foreach (var line in myPart3.Poem) Console.WriteLine(line);

            Console.WriteLine("\nPart 4:");
            foreach (var line in myPart4.Poem) Console.WriteLine(line);
            Console.WriteLine("\nPart 5:");
            foreach (var line in myPart5.Poem) Console.WriteLine(line);

            Console.WriteLine("\nPart 6:");
            foreach (var line in myPart6.Poem) Console.WriteLine(line);

            Console.WriteLine("\nPart 7:");
            foreach (var line in myPart7.Poem) Console.WriteLine(line);

            Console.WriteLine("\nPart 8:");
            foreach (var line in myPart8.Poem) Console.WriteLine(line);

            Console.WriteLine("\nPart 9:");
            foreach (var line in myPart9.Poem) Console.WriteLine(line);
        }
    }
}
