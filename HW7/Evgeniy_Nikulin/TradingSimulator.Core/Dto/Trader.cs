namespace TradingSimulator.Core.Dto
{
    public class Trader
    {
        public int Id;
        public string name;
        public string surname;
        public string phone;
        public decimal money;

        public override string ToString() => $"{Id}. {name} {surname}    {money}$";
    }
}