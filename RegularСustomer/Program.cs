using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace RegularСustomer
{
    //   Напишите программу "Постоянный покупатель" с двумя классами:
    // Shop(Магазин) и Customer(Покупатель)
    //   В классе Shop должна храниться информация о списке товаров(экземпляры классов Item).
    // Также в классе Shop должны быть методы Add(для добавления товара) и Remove(для удаления товара).
    //   В классе Item должны быть свойства Id(идентификатор товара) и Name(название товара).
    //   В классе Customer должен быть метод OnItemChanged, который будет срабатывать,
    // когда список товаров в магазине обновился. В этом методе надо выводить в консоль информацию
    // о том, какое именно изменение произошло (добавлен товар с таким-то названием
    // и таким-то идентификатором / удален такой-то товар).
    //   В основном файле программы создайте Магазин, создайте Покупателя.
    // Реализуйте через ObservableCollection возможность подписки Покупателем на изменения
    // в ассортименте Магазина - все изменения сразу должны отображаться в консоли
    // (должен срабатывать метод Customer.OnItemChanged).
    //   По нажатии клавиши A добавляйте новый товар в магазин.
    // Товар должен называться "Товар от <текущее дата и время>",
    // где вместо<текущее дата и время> подставлять текущую дату и время.
    //   По нажатии клавиши D спрашивайте какой товар надо удалить.
    // Пользователь должен ввести идентификатор товара, после чего товар необходимо удалить из ассортимента магазина.
    //    По нажатии клавиши X выходите из программы.
    //    Добавьте в Магазин несколько товаров, удалите какие-то из них - убедитесь, что сообщения выводятся в консоль.
    class Program
    {
        static void Main(string[] args)
        {
            // Создаем магазин и покупателя.
            Shop shop = new();
            Customer customer = new();

            // Подписываем покупателя на изменения в магазине.
            shop.Items.CollectionChanged += customer.OnItemChanged;

            // Добавляем начальные товары
            shop.Add(new Item { Id = 1, Name = $"Товар №1 от {DateTime.Now}" });
            shop.Add(new Item { Id = 2, Name = $"Товар №2 от {DateTime.Now}" });

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Нажмите A для добавления товара, D для удаления товара, L для вывода списка товаров, X для выхода из программы.");
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.A:   
                        // Добавление нового товара.
                        string itemName = $"Товар от {DateTime.Now}";
                        // Определение Id нового товара.
                        int newId = shop.Items.Count > 0 ? shop.Items.Max(i => i.Id) + 1 : 1;
                        shop.Add(new Item { Id = newId, Name = itemName });
                        break;

                    case ConsoleKey.L:   
                        // Вывод списка товаров.
                        Console.WriteLine("----- Список товаров ------");
                        foreach (Item item in shop.Items)
                            Console.WriteLine($"ID {item.Id}: {item.Name}");
                        break;

                    case ConsoleKey.D:   
                        // Удаление товара.
                        Console.WriteLine("Введите ID товара для удаления:");
                        if (int.TryParse(Console.ReadLine(), out int idToRemove))
                        {
                            shop.Remove(idToRemove);
                        }
                        else
                        {
                            Console.WriteLine($"Некорректный ID.");
                        }
                        break;

                    case ConsoleKey.X:
                        // Выход из программы.
                        Console.WriteLine("==>Выход из программы. Приходите ещё.");
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Неверная команда.");
                        break;
                }
            }
        }
    }

    class Shop
    {
        // Коллекция товаров в магазине.
        public ObservableCollection<Item> Items { get; private set; } = [];

        // Метод для добавления товара.
        public void Add(Item item)
        {
            Items.Add(item);
        }

        // Метод для удаления товара.
        public void Remove(int id)
        {
            var item = Items.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                Items.Remove(item);
            }
            else
            {
                Console.WriteLine($"Товар с ID {id} не найден.");
            }
        }
    }

    class Item
    {
        // Свойства товара.
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    class Customer
    {
        // Метод обработки изменения списка товаров.
        public void OnItemChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count > 0)
            {
                // Доступ к новому элементу
                Item? newItem = e.NewItems[0] as Item;
                Console.WriteLine($"Добавлен товар: {newItem?.Name} с ID {newItem?.Id}");
            }
            if (e.OldItems != null && e.OldItems.Count > 0)
            {
                // Доступ к удаленному элементу.
                Item? oldItem = e.OldItems[0] as Item;
                Console.WriteLine($"Удален товар: {oldItem?.Name} с ID {oldItem?.Id}");
            }
        }
    }
}
