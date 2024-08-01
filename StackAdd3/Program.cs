using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace Stack
/*Нужно создать класс Stack у которого будут следующие свойства

1. В нем будем хранить строки
2. В качестве хранилища используйте список List
3. Конструктор стека может принимать неограниченное количество входных параметров типа string, которые по порядку добавляются в стек
4. Метод Add(string) - добавить элемент в стек
5. Метод Pop() - извлекает верхний элемент и удаляет его из стека. При попытке вызова метода Pop у пустого стека - выбрасывать исключение с сообщением "Стек пустой"
6. Свойство Size - количество элементов из Стека
7. Свойство Top - значение верхнего элемента из стека. Если стек пустой - возвращать null */
/* Пример работы
var s = new Stack("a", "b", "c");
// size = 3, Top = 'c'
Console.WriteLine($"size = {s.Size}, Top = '{s.Top}'");
var deleted = s.Pop();
// Извлек верхний элемент 'c' Size = 2
Console.WriteLine($"Извлек верхний элемент '{deleted}' Size = {s.Size}");
s.Add("d");
// size = 3, Top = 'd'
Console.WriteLine($"size = {s.Size}, Top = '{s.Top}'");
s.Pop();
s.Pop();
s.Pop();
// size = 0, Top = null
Console.WriteLine($"size = {s.Size}, Top = {(s.Top == null ? "null" : s.Top)}");
s.Pop(); */
/*       Доп. задание 1
Создайте класс расширения StackExtensions и добавьте в него метод расширения Merge, который на вход принимает стек s1, и стек s2.
Все элементы из s2 должны добавится в s1 в обратном порядке
Сам метод должен быть доступен в класс Stack
var s = new Stack("a", "b", "c")
s.Merge(new Stack("1", "2", "3"))
// в стеке s теперь элементы - "a", "b", "c", "3", "2", "1" <- верхний  */
/*       Доп. задание 2
В класс Stack и добавьте статический метод Concat, который на вход неограниченное количество параметров типа Stack
и возвращает новый стек с элементами каждого стека в порядке параметров, но сами элементы записаны в обратном порядке
var s =Stack.Concat(new Stack("a", "b", "c"), new Stack("1", "2", "3"), new Stack("А", "Б", "В"));
// в стеке s теперь элементы - "c", "b", "a" "3", "2", "1", "В", "Б", "А" <- верхний      */
/*        Доп. задание 3
Вместо коллекции - создать класс StackItem, который
 - доступен только для класс Stack (отдельно объект класса StackItem вне Stack создать нельзя)
 - хранит текущее значение элемента стека
 - ссылку на предыдущий элемент в стеке
 - Методы, описанные в основном задании переделаны под работу со StackItem                     */
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var s = new Stack("a", "b", "c");
                // size = 3, Top = 'c'
                Console.WriteLine($"size = {s.Size}, Top = '{s.Top}'");
                var deleted = s.Pop();
                // Извлек верхний элемент 'c' Size = 2
                Console.WriteLine($"Извлек верхний элемент '{deleted}' Size = {s.Size}");
                s.Add("d");
                // size = 3, Top = 'd'
                Console.WriteLine($"size = {s.Size}, Top = '{s.Top}'");
                s.Pop();
                s.Pop();
                s.Pop();
                // size = 0, Top = null
                Console.WriteLine($"size = {s.Size}, Top = {(s.Top ?? "null")}");

                //  Console.WriteLine($"Проверка Pop:  {s.Pop()}  ");

                Console.WriteLine("================");
                Console.WriteLine("Проверка Merge: ");
                s = new Stack("a", "b", "c");
                s.Merge(new Stack("1", "2", "3"));
                while (s.Size > 0)
                {
                    Console.WriteLine(s.Pop());
                }
                Console.WriteLine("================");
                Console.WriteLine("Проверка Concat: ");
                s = Stack.Concat(new Stack("a", "b", "c"), new Stack("1", "2", "3"), new Stack("А", "Б", "В"));
                while (s.Size > 0)
                {
                    Console.WriteLine(s.Pop());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
        }
    }
}
