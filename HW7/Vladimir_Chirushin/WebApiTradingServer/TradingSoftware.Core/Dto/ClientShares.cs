namespace TradingSoftware.Core.Dto
{
    using System.Collections.Generic;

    public class ClientShares
    {
        public ClientShares()
        {
            this.ShareWithPrice = new Dictionary<string, decimal>();
        }

        public string clientName { get; set; }

        public Dictionary<string, decimal> ShareWithPrice { get; set; }
    }
}
