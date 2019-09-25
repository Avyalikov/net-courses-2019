namespace TradingSoftware.ConsoleClient.Services
{
    public interface IDataBaseInitializer
    {
        void Initiate();

        void CreateRandomShare();
    }
}