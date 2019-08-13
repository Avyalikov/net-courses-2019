namespace trading_software
{
    public interface ITransactionValidator
    {
        bool Validate(Transaction transaction);
    }
}