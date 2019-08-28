namespace WebApiTradingServer.Services
{
    public interface IDataBaseInitializer
    {
        void Initiate();

        void CreateRandomShare();
    }
}