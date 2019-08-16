namespace Trading
{
    using System;
    using System.Collections.Generic;
    using Interfaces;
    using System.Linq;
    using Entities;
    using System.Text;

    class StockExchange
    {
        readonly IInputOutputDevice ioProvider;
        readonly StockExchangeContext db;

        public StockExchange(IInputOutputDevice ioProvider, StockExchangeContext context)
        {
            this.ioProvider = ioProvider;
            this.db = context;
        }

        public void Start()
        {
            /* Menu
             * 1. Start traiding
             * 2. Clients in 'Orange' zone
             * 3. Change the cost of share type
             * 4. Add a new share into system
             * 5. Add a new share type into system
             * 6. Add a new client
             */

            string s;
            ioProvider.WriteLineOutput("Enter 'e' for exit");
            do
            {
                s = ioProvider.ReadInput();
            } while (s != "e");
        }
    }
}
