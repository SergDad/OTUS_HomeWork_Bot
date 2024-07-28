using System.Collections.Generic;

namespace IRobot
{
 /* 1  Создать интерфейс IRobot с публичным методами string GetInfo() и List GetComponents(), а также string GetRobotType() с дефолтной реализацией, 
  *     возвращающей значение "I am a simple robot.".
    2  Создать интерфейс IChargeable с методами void Charge() и string GetInfo().
    3  Создать интерфейс IFlyingRobot как наследник IRobot с дефолтной реализацией GetRobotType(), возвращающей строку "I am a flying robot.".
    4  Создать класс Quadcopter, реализующий IFlyingRobot и IChargeable.В нём создать список компонентов List _components = new List { "rotor1", "rotor2", "rotor3", "rotor4" } 
        и возвращать его из метода GetComponents().
    5  Реализовать метод Charge() должен писать в консоль "Charging..." и через 3 секунды "Charged!". Ожидание в 3 секунды реализовать через Thread.Sleep(3000).
    6  Реализовать все методы интерфейсов в классе. До этого пункта достаточно было "throw new NotImplementedException();"*/
    internal class Program
    {
        public interface IRobot
        {
            public string GetInfo();
            public List<string> GetComponents();
            public string  GetRobotType() => "I am a simple robot.";
        }
        public interface IChargeable
        {
            void Charge();
            string GetInfo();
        }
        public interface IFlyingRobot:IRobot
        {
            new public string GetRobotType() => "I am a flying robot.";
        }
        public class Quadcopter: IFlyingRobot, IChargeable
        {

            private List<string> _components = new() { "rotor1", "rotor2", "rotor3", "rotor4" };
            public List<string> GetComponents() => _components; 
            public void Charge()
            {
                Console.WriteLine("Charging...");
                Thread.Sleep(3000);
                Console.WriteLine("Charged!");
            }
            // string IRobot.GetInfo() => "I am Flying robot. I can interface IRobot";
            // string IChargeable.GetInfo() => "I am Flying robot. I can interface ICargeable";
            public string GetInfo() => "I am Flying robot. I can interface IRobot & IChargeable";
            
        }
        static void Main(string[] args)
        {
            var copter = new Quadcopter();
            Console.WriteLine(copter.GetInfo());
            Console.WriteLine($"My components: {String.Join(", ", copter.GetComponents())}");
            copter.Charge();
            IFlyingRobot flyingCopter = new Quadcopter();
            Console.WriteLine(flyingCopter.GetRobotType());
            Console.WriteLine(flyingCopter.GetInfo());
            IChargeable chargeCopter = new Quadcopter();
            Console.WriteLine(chargeCopter.GetInfo());
            

        }
    }
}
