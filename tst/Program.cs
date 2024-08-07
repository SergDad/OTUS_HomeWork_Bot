using System;
using System.Collections.Generic;

class Планета
{
    public string Название { get; set; }
    public int ПорядковыйНомерОтСолнца { get; set; }
    public double ДлинаЭкватора { get; set; }
    public Планета ПредыдущаяПланета { get; set; }
}

class КаталогПланет
{
    private List<Планета> планеты;

    public КаталогПланет()
    {
        планеты = new List<Планета>();

        Планета венера = new Планета { Название = "Венера", ПорядковыйНомерОтСолнца = 2, ДлинаЭкватора = 38025, ПредыдущаяПланета = null };
        Планета земля = new Планета { Название = "Земля", ПорядковыйНомерОтСолнца = 3, ДлинаЭкватора = 40075, ПредыдущаяПланета = венера };
        Планета марс = new Планета { Название = "Марс", ПорядковыйНомерОтСолнца = 4, ДлинаЭкватора = 21344, ПредыдущаяПланета = земля };

        планеты.Add(венера);
        планеты.Add(земля);
        планеты.Add(марс);
    }

    public (int, double, string) ПолучитьПланету(string название, Func<string, string> planetValidator)
    {
        string ошибка = planetValidator(название);
        if (ошибка != null)
        {
            return (0, 0, ошибка);
        }

        foreach (var планета in планеты)
        {
            if (планета.Название == название)
            {
                return (планета.ПорядковыйНомерОтСолнца, планета.ДлинаЭкватора, null);
            }
        }

        return (0, 0, "Не удалось найти планету");
    }
}

class Program
{
    static void Main(string[] args)

    {
        //Predicate<int> isPositive = (int x) => x > 0;
        static bool isPositive (int x) => x > 0;
        static bool Foo(int x, Predicate<int> pr) => pr(x); 
        Console.WriteLine(isPositive(20));
        Console.WriteLine(isPositive(-20));
        Console.WriteLine("=====================");

        КаталогПланет каталог = new КаталогПланет();
        int запросы = 0;

        Func<string, string> validator = (string название) =>
        {
            запросы++;
            if (запросы % 3 == 0)
            {
                return "Вы спрашиваете слишком часто";
            }
            return null;
        };

        var результат1 = каталог.ПолучитьПланету("Земля", validator);
        if (результат1.Item3 == null)
        {
            Console.WriteLine($"Название: Земля, Порядковый номер: {результат1.Item1}, Длина экватора: {результат1.Item2}");
        }
        else
        {
            Console.WriteLine(результат1.Item3);
        }

        var результат2 = каталог.ПолучитьПланету("Лимония", validator);
        if (результат2.Item3 == null)
        {
            Console.WriteLine($"Название: Лимония, Порядковый номер: {результат2.Item1}, Длина экватора: {результат2.Item2}");
        }
        else
        {
            Console.WriteLine(результат2.Item3);
        }

        var результат3 = каталог.ПолучитьПланету("Марс", validator);
        if (результат3.Item3 == null)
        {
            Console.WriteLine($"Название: Марс, Порядковый номер: {результат3.Item1}, Длина экватора: {результат3.Item2}");
        }
        else
        {
            Console.WriteLine(результат3.Item3);
        }
    }
}
