using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Trading
{
    class TradeManager : ITrade
    {
        private readonly IPhraseProvider phraseProvider;
        private readonly IIOProvider ioProvider;
        private readonly IValidator validator;
        private readonly IOperations operations;

        Thread clientsOperation;

        public TradeManager(IPhraseProvider phraseProvider, IIOProvider ioProvider, IValidator validator, IOperations operations)
        {
            this.phraseProvider = phraseProvider;
            this.ioProvider = ioProvider;
            this.validator = validator;
            this.operations = operations;

            clientsOperation = new Thread(operations.StartTradingOperations);
        }

        public void Run()
        {
            string userInput = "";
            ioProvider.WriteLine(phraseProvider.GetPhrase("Welcome"));       

            using (var db = new TradingDBContext())
            {
                clientsOperation.Start(db);
                while (!userInput.ToLower().Equals("e"))
                {
                    userInput = ioProvider.ReadLine();
                    processUserInput(db, userInput);
                }
            }
        }

        private void processUserInput(TradingDBContext db, string userInput)
        {
            string[] splitedUserInput = userInput.Split(' ', '\t', ';');
            switch (splitedUserInput[0].ToLower())
            {
                case "addclient":
                    addNewClient(db, splitedUserInput);
                    break;
                case "showorange":
                    showOrangeList(db);
                    break;
                case "showblack":
                    break;
                default:
                    break;
            }
        }

        private void addNewClient(TradingDBContext db, string[] clientInfo)
        {
            if (validator.ValidateClient(clientInfo))
            {
                Clients client = new Clients()
                {
                    ClientFirstName = clientInfo[1],
                    ClientLastName = clientInfo[2],
                    PhoneNumber = clientInfo[3],
                    ClientBalance = decimal.Parse(clientInfo[4])
                };
                db.Clients.Add(client);
                db.SaveChanges();
            }
        }

        private void showOrangeList(TradingDBContext db)
        {
            var orangeList = db.Clients.Where(x => x.ClientBalance == 0).ToList();
            foreach (Clients client in orangeList)
            {
                
            }
        }
    }
}
