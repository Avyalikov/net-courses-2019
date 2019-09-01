namespace TradingApp.ConsoleClient.JsModels
{
    public class JsBalance
    {
        public string UserInfo { get; set; }
        public decimal Balance { get; set; }
        public string InfoAboutStatus { get; set; }

        public override string ToString()
        {
            return $"{UserInfo} {Balance} {InfoAboutStatus}";
        }
    }
}
