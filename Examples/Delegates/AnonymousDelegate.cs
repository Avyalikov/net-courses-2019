using System;

namespace Delegates
{
    public class AnonymousDelegate
    {
        delegate void WithoutReturn(int x);
        delegate int WithReturn(int x);

        public static void CreateDelegatesAndLambdas()
        {
            WithoutReturn p =
                delegate (int x)
                {
                    Console.WriteLine("Delegate without return: " + x);
                };

            WithoutReturn lambda_p = (int x) => { Console.WriteLine("lambda without return: " + x); };

            p(2);
            lambda_p(2);

            WithReturn p2 =
                delegate (int x)
                {
                    return x * x;
                };

            WithReturn lambda_p2 = (int x) => x * x;

            var delegateResult = p2(10);
            Console.WriteLine("Delegate with return:" + delegateResult);

            var lambdaResult = lambda_p2(10);
            Console.WriteLine("Lambda with return:" + lambdaResult);
        }
    }
}
