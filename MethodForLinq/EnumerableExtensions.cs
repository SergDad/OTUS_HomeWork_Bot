namespace MethodForLinq
{
    public static class EnumerableExtensions
    {
        // Дженерик методы расширения для IEnumerable, принимающие значение Х от 1 до 100 процентов
        // и возвращающий элементы выборки в порядке убывания в количестве, соответсвующем заданному
        // количеству процентов с округлением количества элементов в большую сторону.

        // Дженерик метод Top, сортирующий коллекцию по умолчанию.
        public static IEnumerable<T> Top<T>(this IEnumerable<T> collection, int percentage)
        {
            var sortedCollection = collection.OrderByDescending(x => x).ToList();
            return MyTop<T>(sortedCollection, percentage);
        }

        // Дженерик перегрузка метода Top c делегетом Func, указывающим поле, по которому проводится сортировка.
        public static IEnumerable<T> Top<T>(this IEnumerable<T> collection, int percentage, Func<T, int> selector)
        {
            var sortedCollection = collection.OrderByDescending(selector).ToList();
            return MyTop<T>(sortedCollection, percentage);
        }

        // Вспомогательный метод для сокращения дублирования кода.
        public static IEnumerable<T> MyTop<T>(IEnumerable<T> collection, int percentage)
        {
            if (percentage < 1 || percentage > 100)
                throw new ArgumentException("Percentage must be between 1 and 100");
            int countToTake = (int)Math.Ceiling(collection.Count() * (percentage / 100.0));
            return collection.Take(countToTake);
        }
    }
}
