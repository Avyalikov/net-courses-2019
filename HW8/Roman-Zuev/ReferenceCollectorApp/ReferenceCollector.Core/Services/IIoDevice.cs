namespace ReferenceCollector.Core.Services
{
    public interface IIoDevice
    {
        void Print(string data);
        void ReadKey();
    }
}