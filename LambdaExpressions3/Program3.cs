namespace LambdaExpressions3
/*Скопировать решение из программы 2, но переделать метод "получить планету" так, чтобы он на вход принимал еще один параметр, 
описывающий способ защиты от слишком частых вызовов - делегат PlanetValidator (можно вместо него использовать Func), 
который на вход принимает название планеты, а на выходе дает строку с ошибкой.
Метод "получить планету" теперь не должен проверять сколько вызовов делалось ранее. 
Вместо этого он должен просто вызвать PlanetValidator и передать в него название планеты, 
поиск которой производится. И если PlanetValidator вернул ошибку - передать ее на выход из метода третьим полем.
Из main-метода при вызове "получить планету" в качестве нового параметра передавать лямбду, 
которая делает всё ту же проверку, которая была и ранее - на каждый третий вызов она возвращает строку 
"Вы спрашиваете слишком часто" (в остальных случаях возвращает null). Результат исполнения программы должен получиться идентичный программе 2.
(*) Дописать main-метод так, чтобы еще раз проверять планеты "Земля", "Лимония" и "Марс", но передавать другую лямбду так, 
чтобы она для названия "Лимония" возвращала ошибку "Это запретная планета", а для остальных названий - null. 
Убедиться, что в этой серии проверок ошибка появляется только для Лимонии.
Таким образом, вы делегировали логику проверки допустимости найденной планеты от метода "получить планету" к вызывающему этот метод коду.*/
{
    using System;
    using System.Collections.Generic;
    using static System.Runtime.InteropServices.JavaScript.JSType;

    class Program
    {

        static void Main()
        {
            PlanetCatalog catalog = new PlanetCatalog();
            // Запрашиваем планеты по названиям
            var search = new List<string> { "Земля", "Лимония", "Марс" };
            int counterRequest = 0;

            foreach (var planet in search)
            {
                var (number, equatorLength, error) = catalog.getPlanet(planet, (planet) => ++counterRequest % 3 == 0 ?  "Вы спрашиваете слишком часто": null );

                if (error == null)
                {
                    Console.WriteLine($"{planet}: Порядковый номер - {number}, Длина экватора - {equatorLength}");
                }
                else
                {
                    Console.WriteLine($"{planet}: {error}");
                }
            }
            Console.WriteLine("===================================");
            foreach (var planet in search)
            {
                var (number, equatorLength, error) = catalog.getPlanet(planet, (planet) => planet == "Лимония" ? "Это запретная планета" : null);

                if (error == null)
                {
                    Console.WriteLine($"{planet}: Порядковый номер - {number}, Длина экватора - {equatorLength}");
                }
                else
                {
                    Console.WriteLine($"{planet}: {error}");
                }
            }
        }
        public delegate string? PlanetValidator(string planet);
    }

}
