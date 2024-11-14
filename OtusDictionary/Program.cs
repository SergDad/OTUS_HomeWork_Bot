# region Условие
using System;
namespace OtusDictionary
//  Реализуем свой собственный словарь

//  Цель:
//  Реализуйте класс OtusDictionary, который позволяет оперировать int-овыми значениями в качестве ключей
//  и строками в качестве значений. Для добавления используйте метод void Add(int key, string value),
//  а для получения элементов - string Get(int key).
//  Внутреннее хранилище реализуйте через массив. При нахождении коллизий, создавайте новый массив
//  в два раза больше и не забывайте пересчитать хеши для всех уже вставленных элементов.
//  Метод GetHashCode использовать не нужно и массив/список объектов по одному адресу создавать тоже не нужно
//  (только один объект по одному индексу). Словарь не должен давать сохранить null в качестве строки,
//  соответственно, проверять заполнен элемент или нет можно сравнивая строку с null.
//
//  Описание/Пошаговая инструкция выполнения домашнего задания:
//  1. Реализуйте метод Add с неизменяемым массивом размером 32 элемента(исключение, если уже занято место).
//  2. Реализуйте метод Get.
//  3. Реализуйте увеличение массива в два раза при нахождении коллизий.
//  4. Убедитесь, что класс работает без ошибок (например, Get несуществующего элемента) не бросает исключений,
//  помимо заданных вами.Если это не так, то доработайте.
//  5. Добавьте к классу возможность работы с индексатором.
# endregion
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Проверка работы класса OtusDictionary");
            OtusDictionary? dict = new OtusDictionary();
            Random rnd = new Random();
            int key;
            string value;
            Item item = new();
            List<Item> items = new();
            for (int i = 0; i < 200; i++)
            {
                try
                {
                    key = rnd.Next(-400, 400);
                    value = $"Text for Key {key}";
                    dict.Add(key, value);
                    item.S(key, value);
                    items.Add(item);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Обработка еxception {e.Message}");
                }
            }
            string? dictFromKey;  // Содержимое словаря dict по ключу key
            bool enableTest = true;
            foreach (Item item1 in items)
            {
                key = item1.key;
                dictFromKey = dict.Get(key);
                if (item1.value != dictFromKey)
                {
                    Console.WriteLine($"Для ключа {key} значение не найдено или неверное: {dictFromKey ?? "NULL"}");
                    enableTest = false;
                }
                //else
                //    Console.WriteLine($"ключ {key}, {dictFromKey ?? "NULL"}");
            }
            if (!enableTest)
                Console.WriteLine("Класс OtusDictionary тест не прошел");
            else
                Console.WriteLine("Класс OtusDictionary тест прошел");
        }
        struct Item
        {
            internal int key;
            internal string? value;
            public void S(int keyS, string valueS)
            {
                key = keyS;
                value = valueS;
            }
        };
    };

}