namespace Trading.Interface
{
    interface IIOProvider
    {
        void WriteLine(string line);

        string ReadLine();
    }
}
