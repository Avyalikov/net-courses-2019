using System;

namespace Delegates
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1 Simple delegate 
            // var simpleDelegateSample = new SimpleDelegateSample();
            //simpleDelegateSample.TransformTest();

            // 2 multicast delegate
            //var multicastDelegateSample = new MulticastDelegateSample();
            //multicastDelegateSample.TestProgress();

            // 3 anonymous delegate
            AnonymousDelegate.CreateDelegatesAndLambdas();
        }

    }
}
