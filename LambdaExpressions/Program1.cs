﻿using System;
using System.IO;
using System.Numerics;

namespace LambdaExpressions
/*    Программа 1.
Создать четыре объекта анонимного типа для описания планет Солнечной системы со свойствами "Название", 
    "Порядковый номер от Солнца", "Длина экватора", "Предыдущая планета" (ссылка на объект - предыдущую планету):
Венера, Земля, Марс, Венера(снова)
Данные по планетам взять из открытых источников.
Вывести в консоль информацию обо всех созданных "планетах". Рядом с информацией по каждой планете вывести эквивалентна ли она Венере. */
{
    internal class Program
    {
        public static void PlanetPrint(dynamic Planet, dynamic eqPlanet)
        { 
            Console.Write($"Планета: {Planet.Название},  Порядковый номер от Солнца: {Planet.ПорядковыйНомер},  Длина экватора: {Planet.ДлинаЭкватора} км,  ");
            Console.WriteLine($"Предыдущая планета: {(Planet.ПредыдущаяПланета == null ? "отсутствует" : Planet.ПредыдущаяПланета.Название)} ");
            Console.WriteLine($"Эквивалентна  \"{eqPlanet.Название}\": {Planet.Equals(eqPlanet)}");
        }
        static void Main()
        {
            // Создаем 4 объекта анонимного типа для описания планет Солнечной системы
            var venus = new { Название = "Венера", ПорядковыйНомер = 2, ДлинаЭкватора = 38025, ПредыдущаяПланета = (object?) null };
            var earth = new { Название = "Земля", ПорядковыйНомер = 3, ДлинаЭкватора = 40075, ПредыдущаяПланета = venus };
            var mars = new { Название = "Марс", ПорядковыйНомер = 4, ДлинаЭкватора = 21344, ПредыдущаяПланета = earth };
            var venusAgain = new { Название = "Венера", ПорядковыйНомер = 2, ДлинаЭкватора = 38025, ПредыдущаяПланета = mars };
            // var venusAgain = new { Название = "Венера(снова)", ПорядковыйНомер = 2, ДлинаЭкватора = 38025, ПредыдущаяПланета = mars };

            // Выводим в консоль информацию о всех созданных "планетах" и об их эквивалентности Венере
            PlanetPrint(venus, venus);
            PlanetPrint(earth, venus);
            PlanetPrint(mars, venus);
            PlanetPrint(venusAgain, venus);
        }
    }
}