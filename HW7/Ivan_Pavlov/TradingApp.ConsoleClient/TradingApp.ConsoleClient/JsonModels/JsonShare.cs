namespace TradingApp.ConsoleClient.JsonModels
{
    public class JsonShare
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"{Name} {CompanyName} стоимость {Price}";
        }
    }
}
