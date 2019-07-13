using System;
using System.Collections.Generic;

namespace ReferencesVsValues
{
    struct PointAsValue
    {
        public int X;
        public int Y;
    }

    class PointAsReference
    {
        public int X;
        public int Y;
    }

    class CurveA : Curve
    {
        public string PropertyA { get; set; }
    }

    class CurveB : Curve
    {
        public string PropertyB { get; set; }
    }

    class Curve
    {
        private PointAsValue _point;
        public PointAsValue Point
        {
            get { return this._point; }
            set { this._point = value; }
        }

        private PointAsReference _pointR;
        public PointAsReference PointR
        {
            get { return this._pointR; }
            set { this._pointR = value; }
        }
    }

    class Program
    {

        static void Main(string[] args)
        {

            // example 1

            {
                var pointsAsArray = new PointAsValue[]
                {
                    new PointAsValue() { X = 2, Y = 3 }
                };
                var pointsAsList = new List<PointAsValue>()
                {
                    new PointAsValue() { X = 2, Y = 3 }
                };

                pointsAsArray[0].X = 2;

                var firstPoint = pointsAsList[0];
                firstPoint.X = 3;

                // this line does not compile during copying of value type
                //pointsAsList[0].X = 2;
            }

            // example 2
            {
                var pointsAsArray = new PointAsReference[] { new PointAsReference() { X = 2, Y = 3 } };
                var pointsAsList = new List<PointAsReference>() { new PointAsReference() { X = 2, Y = 3 } };

                pointsAsArray[0].X = 2;
                pointsAsList[0].X = 2;
            }

            // example 3

            {
                var testCurve = new Curve()
                {
                    Point = new PointAsValue()
                    {
                        X = 1,
                        Y = 2
                    }
                };

                // value type only!
                var point = testCurve.Point;
                point.X = 111;
                testCurve.Point = point;

                // this line does not compile, during copying of value type
                // testCurve.Point.X = 5;

                // reference type only!
                testCurve.PointR.X = 222;

            }


        }
    }
}
