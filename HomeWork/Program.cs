//   Описание / Пошаговая инструкция выполнения домашнего задания:
//Вам предстоит создать консольное приложение, которое будет имитировать интерактивное меню бота согласно следующему плану:

//Приветствие: При запуске программы отображается сообщение приветствия со списком доступных команд: / start, / help, / info, / exit.
//Обработка команды /start: Если пользователь вводит команду /start, программа просит его ввести своё имя. Сохраните введенное имя в переменную.
//   Программа должна обращаться к пользователю по имени в каждом следующем ответе.
//Обработка команды /help: Отображает краткую справочную информацию о том, как пользоваться программой.
//Обработка команды /info: Предоставляет информацию о версии программы и дате её создания.
//Доступ к команде /echo: После ввода имени становится доступной команда /echo. При вводе этой команды с аргументом (например, /echo Hello),
//   программа возвращает введенный текст (в данном примере "Hello").
//Основной цикл программы: Программа продолжает ожидать ввод команды от пользователя, пока не будет введена команда /exit.
//Примечание
//Для получения информации из консоли пользователя воспользуйтесь функцией Console.ReadLide()
//Например
//var input = Console.ReadLine(); // Тут от консоли требуется ввести текст а потом нажать enter

//   Критерии оценки:
//Отображение приветственного сообщения и списка команд: 1 балл
//Правильная обработка команды /start и сохранение имени: 2 балла
//Корректное обращение к пользователю по имени: 1 балл
//Отображение справочной информации по команде /help: 1 балл
//Отображение информации о версии и дате создания по команде /info: 1 балл
//Обработка команды /echo с аргументом: 2 балла
//Поддержка цикла для продолжения работы программы: 1 балл
//Код написан аккуратно и без ошибок: 1 балл
//Максимальное количество баллов: 10 баллов
//Минимальное количество баллов для сдачи ДЗ: 6 баллов
//using System;

namespace HomeWork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в БОТ! Доступны команды: /start, /help, /info, /exit");

            string userName = "";
            bool setUserName = false;
            bool exit=false;
            do
            {
                string message = Console.ReadLine() ?? string.Empty;
                // предварительная обработка
                string messageCommand="";
                string messageParam="";
                int position = message.IndexOf(' ', 1);
                if (position >= 0)
                {
                    messageCommand=message.Substring(0, position).ToLower();
                    messageParam = message.Substring(position+1);
                }
                else
                {
                    messageCommand = message.ToLower();
                }
                switch (messageCommand)
                {
                    case "/start":
                        Console.WriteLine("Введите ваше имя:");
                        userName = Console.ReadLine() ?? string.Empty;
                        if (userName.Length>0)
                        {
                            setUserName = true;
                            Console.WriteLine($"Привет, {userName}!");
                            Console.WriteLine("Теперь доступна еще одна команда /echo - эхо");
                        }
                        break;  
                    case "/help":
                        if (setUserName)
                        {
                            Console.WriteLine($"Спасибо за вопрос, {userName}!");
                            Console.WriteLine("Список доступных команд: /start - начать, /help - помощь, /info - информация, /echo - эхо, /exit - выход");
                        }
                        else
                        {
                            Console.WriteLine("Список доступных команд: /start - начать, /help - помощь, /info - информация, /exit - выход");
                        }
                        break;
                    case "/info":
                        if (setUserName)
                        {
                            Console.WriteLine($"Спасибо за вопрос, {userName}!");
                        }
                        Console.WriteLine("Версия программы: v.0.1, Дата создания: 27.06.2024");
                        break;
                    case "/echo":
                        if (!setUserName)
                        {
                            Console.WriteLine("Начните с команды /start, чтобы ввести ваше имя");
                        }
                        else
                        {
                            Console.WriteLine($"{userName}, Вы ввели \"{messageParam}\"");
                        }
                        break;
                    case "/exit":
                        exit=true;
                        //var text1 = "До свидания";
                        //if (setUserName)
                        //    text1 += " " + userName;
                        //Console.WriteLine(text1 +"!");
                        Console.WriteLine($"До свидания {userName}!");
                        break;
                    default:
                        Console.WriteLine($"Вы ввели: '{message}'");
                        Console.WriteLine($"Это неизвестная мне команда. Для помощи введите /help.");
                        break;
                }
                                                                        
            } while (!exit);
        }
    }
    }
