using System;
using System.Collections.Generic;
using System.Text;

namespace CCExample2
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

    class Application
    {
        delegate T Builder<out T>(string name);  
        delegate void GetInfo<in T>(T item);  

        static void Run(string[] args)
        {
            Builder<Car> carBuilder = BuildCar;
            Builder<BMW> bmwBuilder = BuildBMW;

            carBuilder = bmwBuilder; // covariant - we pick more derived type and assign to less derived

            Car bmwX11 = carBuilder("X11");
            bmwX11.Display();

            GetInfo<BMW> bmwInfo = BmwInfo;
            GetInfo<Car> carInfo = CarInfo;

            bmwInfo = carInfo; // contravariant - we pick less derived type and assign to more derived

            carInfo(bmwX11);
        }

        private static void BmwInfo(BMW bmw)
        {
            bmw.Display();
        }

        private static void CarInfo(Car car)
        {
            car.Display();
        }

        private static Car BuildCar(string name)
        {
            return new Car(name);
        }
        private static BMW BuildBMW(string name)
        {
            return new BMW(name);
        }
    }
}
