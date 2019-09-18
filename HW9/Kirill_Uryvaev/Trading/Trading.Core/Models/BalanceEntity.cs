namespace Trading.Core
{
    using System;
    using System.Collections.Generic;

    public class BalanceEntity
    {
        public int ClientID { get; set; }

        public decimal ClientBalance { get; set; }

        public virtual ClientEntity Client { get; set; }
    }
}