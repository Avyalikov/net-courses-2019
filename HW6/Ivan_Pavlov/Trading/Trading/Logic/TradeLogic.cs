namespace Trading.Logic
{

    public class TradeLogic
    {
        private static TradeLogic tradeLogic = null;

        protected TradeLogic()
        {}

        public static TradeLogic Initialize()
        {
            if (tradeLogic == null)
                tradeLogic = new TradeLogic();
            return tradeLogic;
        }

        public void Run()
        {
            Home.Run();
        }
    }
}
