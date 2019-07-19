using System.Collections.Generic;

namespace Generics
{    
    public class BaseClass { }

    public class DerivedClass : BaseClass { }

    public class BaseComparer : IEqualityComparer<BaseClass>
    {
        public int GetHashCode(BaseClass baseInstance)
        {
            return baseInstance.GetHashCode();
        }

        public bool Equals(BaseClass x, BaseClass y)
        {
            return x == y;
        }
    }

    public class Contravariance
    {
        public static void Test()
        {
            IEqualityComparer<BaseClass> baseComparer = new BaseComparer();
            IEqualityComparer<DerivedClass> childComparer = baseComparer;

            var derived1 = new DerivedClass();
            var derived2 = new DerivedClass();

            var test = new Dictionary<DerivedClass, string>(childComparer);
            test.Add(derived1, "1");
            test.Add(derived2, "2");
        }
    }
}
