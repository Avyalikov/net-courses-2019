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
            string s;
            ioProvider.WriteLineOutput("Please wait until db is loading...");

            db.Database.Initialize(false);

            ioProvider.WriteLineOutput("You are in db using.\nEnter 'e' for exit");
            do
            {
                s = ioProvider.ReadInput();
            } while (s != "e");
        }
    }
}
