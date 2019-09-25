using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeVsEquals
{
    class Program
    {
        public class TestHome
        {

        }

        public struct Point2D
        {
            public int X { get; set; }
            public int Y { get; set; }

            public override bool Equals(object obj)
            {
                if (obj is Point2D point2D)
                {
                    if (point2D.X == this.X && point2D.Y == this.Y)
                    {
                        return true;
                    }
                }

                if (obj is Point3D point3D)
                {
                    if (point3D.Z == 0 && point3D.X == this.X && point3D.Y == this.Y)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public struct Point3D
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }
        }

        public class Room
        {
            public int Size { get; set; }
        }

        public class Kitchen : Room
        {
            public bool ContainsWindow { get; set; }

            public override bool Equals(object obj)
            {
                //return base.Equals(obj);
                if (obj is Room room)
                {
                    return room.Size == this.Size;
                }

                return false;
            }

            public override int GetHashCode()
            {
                return 0;
                 //return (Size, ContainsWindow).GetHashCode();
            }
        }

        public struct Person : IEquatable<Person>
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public bool Equals(Person person)
            {
                bool isNotNull = (!object.ReferenceEquals(person, null));
                bool valEquals = (Id, Name).Equals((person.Id, person.Name));
                return isNotNull && valEquals;
            }

            public override bool Equals(object obj)
            {
                return (obj is Person) && Equals((Person)obj);
            }

            public override int GetHashCode()
            {
                return (Id, Name).GetHashCode();
            }

            public static bool operator ==(Person p1, Person p2)
            {
                bool isNotNull = (!object.ReferenceEquals(p1, null));
                bool valEquals = p1.Equals(p2);
                return isNotNull && valEquals;
            }

            public static bool operator !=(Person p1, Person p2)
            {
                return !(p1 == p2);
            }
        }

        static void Main(string[] args)
        {
            {                
                var kitchenA = new Kitchen() { Size = 24, ContainsWindow = true };
                var kitchenB = new Kitchen() { Size = 24, ContainsWindow = false };

                var hashTable = new Hashtable();

                hashTable.Add(kitchenA, $"This is kitchen A");
                hashTable.Add(kitchenB, $"This is kitchen B");

                Console.WriteLine(hashTable[kitchenA]);
                Console.WriteLine(hashTable[kitchenB]);
            }

            //{
            //    var pointA = new Point2D() { X = 1, Y = 2 };
            //    var pointB = new Point2D() { X = 1, Y = 2 };

            //    Console.WriteLine(pointA.Equals(pointB));
            //    Console.WriteLine(ReferenceEquals(pointA, pointB));
            //}

            //{
            //    var pointA = new Point2D() { X = 1, Y = 2 };
            //    var pointB = new Point2D() { X = 1, Y = 3 };

            //    Console.WriteLine(pointA.Equals(pointB));
            //    Console.WriteLine(ReferenceEquals(pointA, pointB));
            //}

            //{
            //    var pointA = new Point2D() { X = 1, Y = 2 };
            //    var pointB = new Point3D() { X = 1, Y = 2, Z = 0 };
            //    var pointC = new Point3D() { X = 1, Y = 2, Z = 0 };

            //    Console.WriteLine(pointA.Equals(pointB));
            //    Console.WriteLine(pointA.Equals(pointC));
            //}

            //{
            //    var roomA = new Room() { Size = 23 };
            //    var roomB = new Room() { Size = 24 };
            //    var roomC = new Room() { Size = 23 };

            //    Console.WriteLine(roomA.Equals(roomB));
            //    Console.WriteLine(roomA.Equals(roomC));
            //}


            //{
            //    var kitchenA = new Kitchen() { Size = 24, ContainsWindow = false };
            //    var kitchenB = new Kitchen() { Size = 24, ContainsWindow = true };

            //    Console.WriteLine(kitchenA.Equals(kitchenB));
            //}


            //    {
            //        var personA = new Person() { Id = 1, Name = "PersonA" };
            //        var personB = new Person() { Id = 2, Name = "PersonB" };

            //        Console.WriteLine(personA.Equals((object)personB));
            //    }

            Console.ReadLine();
        }
    }
}
