using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MethodForLinq
{

    public static class Program
    {
        public static void Main()
        {
            Console.WriteLine("Метод расширения с названием \"Top\" для коллекции IEnumerable");
            // Пример для int[]
            Console.WriteLine("\nПроверка для массива int по заданию");
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Console.WriteLine("numbers: " + string.Join(", ", numbers));
            var topNumbers = numbers.Top(30);
            Console.WriteLine("  Top 30% numbers: " + string.Join(", ", topNumbers));
            topNumbers = numbers.Top(20);
            Console.WriteLine("  Top 20% numbers: " + string.Join(", ", topNumbers));

            Console.WriteLine("\nЕще одна проверка для массива int");
            numbers = [ 19, 20, 13, 4, 15, 26, 17, 8, 9 ];
            Console.WriteLine("numbers: " + string.Join(", ", numbers));
            topNumbers = numbers.Top(30);
            Console.WriteLine("  Top 30% numbers: " + string.Join(", ", topNumbers));
            topNumbers = numbers.Top(20);
            Console.WriteLine("  Top 20% numbers: " + string.Join(", ", topNumbers));

            // Пример для List<Person>
            Console.WriteLine("\nПроверка для List<Person>");
            var people = new List<Person>
            {
                new() { Name = "Sergey", Age = 55 },
                new() { Name = "Boris", Age = 30 },
                new() { Name = "Pavel", Age = 35 },
                new() { Name = "Aleksey", Age = 40 },
                new() { Name = "Dmitriy", Age = 45 },
                new() { Name = "Mikhail", Age = 25 }
            };
            foreach (var person in people)
            {
                Console.WriteLine(person);
            }
            var topPeopleByAge = people.Top(40, person => person.Age);
            Console.WriteLine("Top 40% people by age (.Top(40, person => person.Age):");
            foreach (var person in topPeopleByAge)
            {
                Console.WriteLine(person);
            }

            topPeopleByAge = people.Top(60, person => person.Age);
            Console.WriteLine("\nTop 60% people by age (.Top(40, person => person.Age):");
            foreach (var person in topPeopleByAge)
            {
                Console.WriteLine(person);
            }


            // Пример для массива int
            Console.WriteLine("\nПроверка для Random массива int");
            var array = new int[30];
            var rnd = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rnd.Next(0, 100);
            };
            Console.WriteLine("array: " + string.Join(", ", array));
            var topArray = array.Top(20);
            Console.WriteLine("  Top 20% array: " + string.Join(", ", topArray));

            try
            {
                Console.WriteLine("\nПроверка для .Top(0)");
                topArray = array.Top(0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"    Ошибка: {ex.Message}");
            }
            try
            {
                Console.WriteLine("\nПроверка для .Top(120)");
                topArray = array.Top(120);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"    Ошибка: {ex.Message}");
            }
        }
    }
}
