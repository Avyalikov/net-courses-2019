namespace TradingApp.ConsoleClient.JsModels
{
    public class JsShare
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
