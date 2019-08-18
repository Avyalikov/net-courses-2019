namespace Trading.DataModel
{
    internal interface ITransactionGenerator
    {
        void GenerateTransactionParams(out int sellerId, out int buyerId, out int sharesId, out int quantity);
    }
}