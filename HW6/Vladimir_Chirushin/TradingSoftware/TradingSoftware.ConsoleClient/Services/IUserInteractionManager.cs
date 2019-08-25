namespace TradingSoftware.Services
{
    public interface IUserInteractionManager
    {
        void ManualAddClient();

        void ManualAddShare();

        void ManualAddTransaction();

        void ManualAddNewBlockOfShare();
    }
}