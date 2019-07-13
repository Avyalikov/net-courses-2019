using System;

namespace ClassesVsInterfaces
{
    interface ISmartHomeFeature
    {
        void BoilWater();
        void TurnLightOn();
        void TurnLightOff();

        bool IsKitchen();
    }

    class House
    {
        public House(ISmartHomeFeature[] features)
        {
            Features = features;
        }

        public ISmartHomeFeature[] Features { get; }

        public void Start()
        {
            foreach (var feature in this.Features)
            {
                feature.TurnLightOn();
            }

            if (DateTime.Now.Hour == 9)
            {
                foreach (var feature in this.Features)
                {
                    if (feature.IsKitchen())
                    {
                        feature.BoilWater();
                    }
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

        }
    }
}
