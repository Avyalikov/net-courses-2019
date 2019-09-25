using System;
using System.Collections.Generic;
using System.Text;

namespace CCExample1
{
    class Car
    {
        public string Name { get; set; }
        public Car(string name)
        {
            Name = name;
        }
        public virtual void Display()
        {
            Console.WriteLine(Name);
        }
    }

    class BMW : Car
    {
        public BMW(string name) : base(name)
        {

        }

        public override void Display()
        {
            Console.WriteLine("I'm BMW :) -> " + Name);
        }
    }

    delegate Car CarFactory(string name);

    delegate void PrintBMWInfo(BMW client);

    class Application
    {
        static void Run(string[] args)
        {
            CarFactory buildCar = BuildCar; // covariance, using more derived type for return value
            Car bmwX5 = buildCar("BMW X5");
            bmwX5.Display();

            PrintBMWInfo getCarInfo = GetCarInfo; // contrvariance, using more generic type that was specified for passing argument
            getCarInfo((BMW)bmwX5);
        }

        private static BMW BuildCar(string name)
        {
            return new BMW(name);
        }

        private static void GetCarInfo(Car p)
        {
            p.Display();
        }
    }


}
