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
    public class Stack
    {
        private StackItem? _top;
        private int _size;
        private class StackItem
        {
            public string Value { get; }
            public StackItem? Previous { get; }
            public StackItem(string value, StackItem previous)
            {
                Value = value; Previous = previous;
            }
        }
        public Stack()
        {
            _top = null;
            _size = 0;
        }
        public Stack(params string[] arg) : this()
        {
            foreach (string data in arg)
            {
                Add(data);
            }
        }
        public void Add(string data)
        {
            _top = new StackItem(data, _top);
            _size++;
        }
        public string? Pop()
        {
            string? rezult;
            if (_size == 0) throw new Exception("Стек пустой");
            rezult = _top.Value;
            _top = _top.Previous;
            _size--;
            return rezult;
        }
        public string? Top
        {
            get { return _size > 0 ? _top.Value : null; }
        }
        // public int Size => _stack.Count;
        public int Size
        {
            get => _size;
        }
        public static Stack Concat(params Stack[] stacks)
        {
            var newStack = new Stack();
            foreach (var stack in stacks)
            {
                while (stack.Size > 0)
                {
                    newStack.Add(stack.Pop());
                }
            }
            return newStack;
        }
    }
}
