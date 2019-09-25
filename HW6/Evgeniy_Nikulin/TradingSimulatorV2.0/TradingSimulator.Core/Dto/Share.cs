namespace TradingSimulator.Core.Dto
{
    public class Share
    {
        public int id;
        public string name;
        public decimal price;
        public int quantity;
        public int ownerId;

        public override string ToString() => $"{id}. {name}    {quantity} Quantiry    {price}$";
    }
}