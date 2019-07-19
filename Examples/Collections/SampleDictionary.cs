using System;
using System.Collections.Generic;

namespace Collections
{
    public class SampleDictionary
    {
        // Dictionary and Hashtable require: any two objects that are equal must have the same hash code value
        // so: Equal => same hashCode
        // but NOT same hashCode => Equal

        public class SomeKeyClass {
            int xValue;
            int yValue;

            public SomeKeyClass(int xValue, int yValue)
            {
                this.xValue = xValue;
                this.yValue = yValue;                    
            }

            public override Int32 GetHashCode()
            {
                return 0; 
            }
        }

        public void TestDictionary() {           
            var myDictionary = new Dictionary<SomeKeyClass, string>();
            var first = new SomeKeyClass(1, 2);
            var second = new SomeKeyClass(9, 4);

        // will work: all elements in same bucket (hash code = 0), but not equal. 
        // But: bad performance - slow retrieval by key!
            myDictionary.Add(first, "first");
            myDictionary.Add(second, "second");
        }

        /*
         * Tips for GetHashCode() (from Richter "CLR via C#"):
         * 
            - Use an algorithm that gives a good random distribution for the best performance of the hash
            table.

            - Your algorithm can also call the base type’s GetHashCode method, including its return value.
            However, you don’t generally want to call Object’s or ValueType’s GetHashCode method,
            because the implementation in either method doesn’t lend itself to high-performance hashing
            algorithms.

            - Your algorithm should use at least one instance field.

            - Ideally, the fields you use in your algorithm should be immutable; that is, the fields should
            be initialized when the object is constructed, and they should never again change during the
            object’s lifetime.
            - Your algorithm should execute as quickly as possible.

            - Objects with the same value should return the same code. For example, two String objects
            with the same text should return the same hash code value.
         */
    }
}
