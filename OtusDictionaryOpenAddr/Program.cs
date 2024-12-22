using System;
namespace OtusDictionaryOpenAddr
{
    public class OtusDictionary
    {
        private const int InitialSize = 32;
        private int _count;
        private int[] _keys;
        private string[] _values;

        public OtusDictionary()
        {
            _keys = new int[InitialSize];
            _values = new string[InitialSize];
            _count = 0;
        }
        // Метод для добавления элемента
        public void Add(int key, string? value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), "Значение не может быть null");

            // Проверяем, что ключ уже существует и заменяем значение, если это так
            for (int i = 0; i < _count; i++)
            {
                if (_keys[i] == key)
                {
                    _values[i] = value;
                    return;
                }
            }
            if (_count >= _keys.Length)
            {
                Resize();
            }
            int index = HashFunction(key);
            while (_values[index] != null)
            {
                index = (index + 1) % _keys.Length;
            }
            _keys[index] = key;
            _values[index] = value;
            _count++;
        }
        // Метод для получения элемента по ключу
        public string? Get(int key)
        {
            int index = HashFunction(key);
            int startIndex = index;

            while (_values[index] != null)
            {
                if (_keys[index] == key)
                {
                    return _values[index];
                }


                index = (index + 1) % _keys.Length;

                if (index == startIndex)
                    break; // Цикл завершен, элемент не найден
            }

            return null; // Элемент не найден
        }

        // Индексатор для доступа по ключу
        public string? this[int key]
        {
            get => Get(key);
            set => Add(key, value);
        }

        // Хеш-функция
        private int HashFunction(int key)
        {
            return Math.Abs(key) % _keys.Length;
        }

        // Метод для увеличения размера массива
        private void Resize()
        {
            int newSize = _keys.Length * 2;
            Console.WriteLine($"Resize to {newSize}");
            int[] newKeys = new int[newSize];
            string[] newValues = new string[newSize];

            for (int i = 0; i < _keys.Length; i++)
            {
                if (_values[i] != null)
                {
                    int newIndex = Math.Abs(_keys[i]) % newSize;
                    while (newValues[newIndex] != null)
                    {
                        newIndex = (newIndex + 1) % newSize;
                    }

                    newKeys[newIndex] = _keys[i];
                    newValues[newIndex] = _values[i];
                }
            }

            _keys = newKeys;
            _values = newValues;
        }
    }

    class Program
    {
        static void Main()
        {
            var dict = new OtusDictionary();
            for (int i = 0; i < 100; i++)
            {
                dict.Add(i, $"Text Key {i}");
            }
            //for (int i = 0; i < 100; i++)
            //{
            //    Console.WriteLine($"ключ {i}, значение {dict.Get(i)}");
            //}
            for (int i = 0; i < 10; i++)
            {
                dict.Add(i * 32, $"New Text Key {i * 32}");
            }
            for (int i = 0; i < 120; i++)
            {
                Console.WriteLine($"ключ {i}, значение {dict.Get(i)}");
            }

            //dict.Add(1, "One");
            //dict.Add(2, "Two");
            //dict.Add(3, "Three");

            //Console.WriteLine(dict.Get(1)); // Output: One
            //Console.WriteLine(dict.Get(2)); // Output: Two
            //Console.WriteLine(dict.Get(10)); // Output: null (ключ не существует)

            //dict[4] = "Four";
            //Console.WriteLine(dict[4]); // Output: Four

            //dict.Add(1, "New One");
            //Console.WriteLine(dict[1]); // Output: New One
        }
    }
}
