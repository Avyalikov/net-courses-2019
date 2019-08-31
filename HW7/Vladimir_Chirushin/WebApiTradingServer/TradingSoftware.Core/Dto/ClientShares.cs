namespace TradingSoftware.Core.Dto
{
    using System.Collections.Generic;

    public class ClientShares
    {
        public ClientShares()
        {
            ShareWithPrice = new Dictionary<string, decimal>();
        }

        public string clientName;
        public Dictionary<string, decimal> ShareWithPrice;
    }
}
