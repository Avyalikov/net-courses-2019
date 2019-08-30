namespace Trading.Core
{
    using System;
    using System.Collections.Generic;

    public partial class ClientsSharesEntity
    {
        public int ClientID { get; set; }

        public int ShareID { get; set; }

        public int Amount { get; set; }

        public int CostOfOneShare { get; set; }

        public virtual ClientEntity Clients { get; set; }

    }
}
