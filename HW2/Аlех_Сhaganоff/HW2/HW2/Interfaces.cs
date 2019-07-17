using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyType = System.Int32;

namespace HW2
{
    public interface IStorageProvider
    {
        MyType peek();
        void push(MyType value);
        MyType pop();
        int Count { get; }
    }

    public interface IReadInputProvider
    {
        string readInput();
    }

    public interface ISendOutputProvider
    {
        void sendOutput(MyType[] currentNumbers);
        void printOutput(string text);
    }

    public interface IRandomProvider
    {
        MyType[] rand(int NumberOfValues, int MinRand, int MaxRand);
    }

    public interface ITextMessagesProvider
    {
        //TextMessages getTextMessages();
    }

    public interface ISettingsProvider
    {
        void getSettings();
    }
}