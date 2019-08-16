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

        public StockExchange(IInputOutputDevice ioProvider)
        {
            this.ioProvider = ioProvider;
        }

        public void Start()
        {
            string s;
            ioProvider.WriteLineOutput("Please wait until db is loading...");

            using (var db = new StockExchangeContext())
            {
                db.Database.Initialize(false);

                ioProvider.WriteLineOutput("You are in db using.\nEnter 'e' for exit");
                do
                {
                    s = ioProvider.ReadInput();
                } while (s != "e");
            }
        }
    }
}
