using System;

namespace Collections
{
    public class Arrays
    {
        public void CreateArraysExamples() {
            // declare and instantiate
            int[] myIntegers; // Declares a reference to an array
            myIntegers = new int[10]; // Creates an array of 10 ints on the manged heap
            myIntegers[0] = 3;
            myIntegers[1] = 4;

            // can instantiate like this:
            string[] names = new string[] { "Aidan", "Grant" };

            // or
            CurvePoint[] myPoints = new CurvePoint[5];
            myPoints[0] = new CurvePoint();

            // or:
            myIntegers = new int[] { 3, 5, 7, 2, 8, 0, 5 };

            //manipulation
            var test = Array.IndexOf(myIntegers, 5);
            var test1 = Array.LastIndexOf(myIntegers, 5);
            var t3 = Array.FindAll(myIntegers, element => element % 2 == 0);

            // Array.ForEach<int>(myIntegers, element => Console.WriteLine(element));

            // pass array to method
            CurvePoint[] marketPrices = CreateMarketPrices();        

            var maxLastYearPrice = GetMaxTaxedPrice(marketPrices, 2018); 
            var maxThisYearPrice = GetMaxTaxedPrice(marketPrices, 2019);

            maxLastYearPrice = GetMaxTaxedPrice(marketPrices, 2018);
        }

        private double GetMaxTaxedPrice(CurvePoint[] points, int year) {
            var newPrices = GetPricesWithTax(points);

            double maxPrice = 0;
            foreach (var point in newPrices)
            {
                if (maxPrice < point.Value && point.TimePoint.Year == year) {
                    maxPrice = point.Value;
                }
            }

            return maxPrice;
        }

        private CurvePoint[] GetPricesWithTax(CurvePoint[] points)
        {
            var copyPoints = DeepCopyPoints(points);
            foreach (var point in copyPoints)
            {
                point.Value *= 2;
            }

            return copyPoints;
        }

        private CurvePoint[] DeepCopyPoints(CurvePoint[] points)
        {
            // check empty: return empty array instead of null

            var resultArray = new CurvePoint[points.Length];
            // Array.Copy(points, resultArray, points.Length); //- will contain refs to same objects

            for (var i = 0; i < points.Length; i++)
            {
                resultArray[i] = new CurvePoint { TimePoint = points[i].TimePoint, Value = points[i].Value };
            }

            return resultArray;
        }

        private CurvePoint[] CreateMarketPrices()
        {
            CurvePoint[] marketPrices = new CurvePoint[6]; // Declares and creates array of 6 references initialized to null
            marketPrices[0] = new CurvePoint { TimePoint = new DateTime(2018, 1, 1), Value = 50.5 }; // only here the memory for the objects is allocatted
            marketPrices[1] = new CurvePoint { TimePoint = new DateTime(2018, 2, 1), Value = 53.2 };
            marketPrices[2] = new CurvePoint { TimePoint = new DateTime(2018, 3, 1), Value = 49.8 };
            marketPrices[3] = new CurvePoint { TimePoint = new DateTime(2019, 1, 1), Value = 50.5 }; // only here the memory for the objects is allocatted
            marketPrices[4] = new CurvePoint { TimePoint = new DateTime(2019, 2, 1), Value = 63.2 };
            marketPrices[5] = new CurvePoint { TimePoint = new DateTime(2019, 3, 1), Value = 49.8 };

            return marketPrices;
        }
    }
}
