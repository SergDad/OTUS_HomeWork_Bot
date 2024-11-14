//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace OtusDictionary
{
    public class OtusDictionary
    {
        #region Описания и конструктор
        private const int InitialSize = 32;
        private int _size;
        private int _count;
        private int[] _keys;
        private string[] _values;

        public OtusDictionary()
        {
            _size = InitialSize;
            _keys = new int[InitialSize];
            _values = new string[InitialSize];
            _count = 0;
        }
        #endregion
        #region  Метод добавления элемента
        public void Add(int key, string? value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), "Значение элемента не может быть null");

            int index = HashFunction(key);
            if (_keys[index] == key && _values[index] != null)
                throw new ArgumentException($"Элемент с ключем = {key} уже есть");  //, nameof(key));
            while (_values[index] != null)
            {
                // Если index уже занят другим key, увеличиваем хранилище в два раза
                Resize();
                index = HashFunction(key);
            }
            _keys[index] = key;
            _values[index] = value;
            _count++;
        }
        #endregion
        #region  Метод получения элемента по ключу
        public string? Get(int key)
        {
            int index = HashFunction(key);
            if (_keys[index] == key)
            {
                return _values[index];
            }
            return null; // Элемент не найден
        }
        #endregion
        #region  Индексатор для доступа по ключу
        public string? this[int key]
        {
            get => Get(key);
            set => Add(key, value);
        }
        #endregion
        #region Хеш-функция
        private int HashFunction(int key)
        {
            if (key > -1) return key * 2 % _size;
            return (key * (-2) - 1) % _size;
        }
        private int HashFunction(int key, int size)
        {
            if (key > -1) return key * 2 % size;
            return (key * (-2) - 1) % size;
        }
        #endregion
        #region Метод для увеличения размера массива
        private void Resize()
        {
            int newSize = _size * 2;
            // Console.WriteLine($"Resize to {newSize}");
            int[] newKeys = new int[newSize];
            string[] newValues = new string[newSize];
            int newIndex;

            for (int i = 0; i < _size; i++)
            {
                if (_values[i] != null)
                {
                    newIndex = HashFunction(_keys[i], newSize);
                    newKeys[newIndex] = _keys[i];
                    newValues[newIndex] = _values[i];
                }
            }
            _size = newSize;
            _keys = newKeys;
            _values = newValues;
        }
        #endregion


    }
}
