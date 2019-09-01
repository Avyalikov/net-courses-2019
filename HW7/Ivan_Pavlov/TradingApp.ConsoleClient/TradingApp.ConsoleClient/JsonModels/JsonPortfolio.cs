namespace TradingApp.ConsoleClient.JsonModels
{
    public class JsonPortfolio
    {
        public int Id { get; set; }
        public int UserEntityId { get; set; }
        public int ShareId { get; set; }
        public int Amount { get; set; }

        public virtual JsonShare Share { get; set; }

        public override string ToString()
        {
            return $"\t{Share.Name} в кол-ве {Amount} шт. по цене {Share.Price}";
        }
    }
}
