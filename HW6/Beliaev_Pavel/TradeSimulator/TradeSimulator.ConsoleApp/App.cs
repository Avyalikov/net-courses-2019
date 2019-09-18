using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeSimulator.ConsoleApp.Interfaces;
using TradeSimulator.Core.Dto;
using TradeSimulator.Core.Services;

namespace TradeSimulator.ConsoleApp
{
    internal enum InputCheckResult
    {
        Valid,
        Invalid
    }

    class App
    {
        private readonly IPhraseProvider phraseProvider;
        private readonly IInputOutput inputOutputModule;

        private readonly GameSettings gameSettings;

        ClientsService clientsService;
        TradingService tradingService;
        ShowDbInfoService showDbInfoService;

        public App(IPhraseProvider phraseProvider, IInputOutput inputOutputModule, ISettingsProvider settingsProvider, 
            ClientsService clientsService, TradingService tradingService, ShowDbInfoService showDbInfoService)
        {
            this.phraseProvider = phraseProvider;
            this.inputOutputModule = inputOutputModule;

            this.clientsService = clientsService;
            this.tradingService = tradingService;
            this.showDbInfoService = showDbInfoService;

            try
            {
                this.gameSettings = settingsProvider.GetGameSettings();
            }
            catch (ArgumentException ex)
            {
                inputOutputModule.WriteOutput(ex.Message);
                this.gameSettings = null;
                return;
            }
        }

        public void Run()
        {
            for(bool i = true; i; )
            {
                i = GeneralMenu();
            }         
        }

        private bool GeneralMenu()
        {
            string userInput;

            inputOutputModule.ClearMenu();
            inputOutputModule.WriteOutput(phraseProvider.GetPhrase(gameSettings.LangPackName, KeysForPhrases.GeneralReg));
            inputOutputModule.WriteOutput(phraseProvider.GetPhrase(gameSettings.LangPackName, KeysForPhrases.GeneralTrade));
            inputOutputModule.WriteOutput(phraseProvider.GetPhrase(gameSettings.LangPackName, KeysForPhrases.GeneralShow));
            inputOutputModule.WriteOutput(gameSettings.ExitButton);
            inputOutputModule.WriteOutput(phraseProvider.GetPhrase(gameSettings.LangPackName, KeysForPhrases.ExitKey));

            userInput = inputOutputModule.ReadInput();

            if (userInput == "1")
            {
                Registration();
            }

            if (userInput == "2")
            {
                Trading();
            }

            if (userInput == "3")
            {
                for (bool i = true; i;)
                {
                    i = ShowInfoMenu();
                }
            }

            if (userInput == gameSettings.ExitButton)
            {
                return false;
            }
            return true;
        }

        private bool ShowInfoMenu()
        {
            string userInput;

            inputOutputModule.ClearMenu();
            inputOutputModule.WriteOutput(phraseProvider.GetPhrase(gameSettings.LangPackName, KeysForPhrases.ShowClients));
            inputOutputModule.WriteOutput(phraseProvider.GetPhrase(gameSettings.LangPackName, KeysForPhrases.ShowStocksOfClients));
            inputOutputModule.WriteOutput(phraseProvider.GetPhrase(gameSettings.LangPackName, KeysForPhrases.ShowStockPrice));
            inputOutputModule.WriteOutput(phraseProvider.GetPhrase(gameSettings.LangPackName, KeysForPhrases.ShowHistory));
            inputOutputModule.WriteOutput(gameSettings.ExitButton);
            inputOutputModule.WriteOutput(phraseProvider.GetPhrase(gameSettings.LangPackName, KeysForPhrases.ExitKey));

            userInput = inputOutputModule.ReadInput();

            if (userInput == "1")
            {
                inputOutputModule.ClearMenu();
                inputOutputModule.WriteOutput("List of clients\n");
                foreach (var client in showDbInfoService.GetAllClients())
                {
                    inputOutputModule.WriteOutput($"ID: {client.Id} Name: {client.Name} Surname: {client.Surname} Phone: {client.PhoneNumber} Balance: {showDbInfoService.GetAccountByClientId(client.Id).Balance} Zone: {showDbInfoService.GetAccountByClientId(client.Id).Zone}\n");
                }
            }

            if (userInput == "2")
            {
                int prevID = -1;
                inputOutputModule.ClearMenu();
                inputOutputModule.WriteOutput("List of client's stocks\n");
                foreach (var stock in showDbInfoService.GetAllStocksOfClient())
                {
                    if(stock.AccountForStock.ClientId != prevID)
                    {
                        inputOutputModule.WriteOutput($"ClientID: {stock.AccountForStock.ClientId}\n");
                    }
                    inputOutputModule.WriteOutput($"Type: {stock.TypeOfStocks} Quantity: {stock.quantityOfStocks}\n");
                    prevID = stock.AccountForStock.ClientId;
                }
            }

            if (userInput == "3")
            {
                inputOutputModule.ClearMenu();
                inputOutputModule.WriteOutput("List of stocks prices\n");
                foreach (var stock in showDbInfoService.GetAllStockPrice())
                {
                    inputOutputModule.WriteOutput($"Type: {stock.TypeOfStock} Price: {stock.PriceOfStock}\n");
                }
            }

            if (userInput == "4")
            {
                inputOutputModule.ClearMenu();
                inputOutputModule.WriteOutput("History of transaction\n");
                foreach (var history in showDbInfoService.GetFullHistory())
                {
                    inputOutputModule.WriteOutput($"Id: {history.Id} BuyerId: {history.BuyerId} SellerId: {history.SellerId} TypeOfStock: {history.TypeOfStock} QuantityOfStocks: {history.QuantityOfStocks} FullPrice: {history.FullPrice}\n");
                }
            }

            if (userInput == gameSettings.ExitButton)
            {
                return false;
            }

            inputOutputModule.ReadKey();
            return true;
        }

        private void Registration()
        {
            ClientRegistrationInfo clientRegistrationInfo = new ClientRegistrationInfo();
            AccountRegistrationInfo accountRegistrationInfo = new AccountRegistrationInfo();
            StockOfClientInfo stockOfClientInfo = new StockOfClientInfo();
            
            clientRegistrationInfo.Name = AskUser(KeysForPhrases.RegName);
                        
            clientRegistrationInfo.Surname = AskUser(KeysForPhrases.RegSurname);
                        
            clientRegistrationInfo.PhoneNumber = AskUser(KeysForPhrases.RegPhone, true);

            accountRegistrationInfo.ClientId = clientsService.RegisterNewClient(clientRegistrationInfo);

            accountRegistrationInfo.Balance = Convert.ToDecimal(AskUser(KeysForPhrases.RegBalance, true));

            stockOfClientInfo.ClientsAccountId = clientsService.CreateNewAccountForNewClient(accountRegistrationInfo);

            if (Convert.ToInt32(AskUser(KeysForPhrases.RegStock, true)) == 1)
            {                
                stockOfClientInfo.TypeOfStocks = AskUser(KeysForPhrases.RegStockType);

                stockOfClientInfo.quantityOfStocks = Convert.ToInt32(AskUser(KeysForPhrases.RegStockQuant, true));

                if (!clientsService.CheckIfStockPriseConteinStockOfClientByTypeOfStock(stockOfClientInfo.TypeOfStocks))
                {
                    StockPriceInfo stockPriceInfo = new StockPriceInfo()
                    {
                        TypeOfStock = stockOfClientInfo.TypeOfStocks
                    };
                    
                    stockPriceInfo.PriceOfStock = Convert.ToDecimal(AskUser(KeysForPhrases.RegStockPrice, true));

                    clientsService.RegisterNewTypeOfStock(stockPriceInfo);
                }

                clientsService.RegisterStockForNewClient(stockOfClientInfo);
            }
        }

        private void Trading()
        {
            TransactionInfo transactionInfo = new TransactionInfo();
            var clients = showDbInfoService.GetAllClients();
            List<string> goodInput = new List<string>();
            string userInput = "empty";

            inputOutputModule.ClearMenu();
            inputOutputModule.WriteOutput(phraseProvider.GetPhrase(gameSettings.LangPackName, KeysForPhrases.Trade));
            inputOutputModule.WriteOutput(gameSettings.ExitButton);
            inputOutputModule.WriteOutput(phraseProvider.GetPhrase(gameSettings.LangPackName, KeysForPhrases.ExitKey));
            userInput = AskUser(KeysForPhrases.ExitKey, true);

            if (userInput == "1")
            {
                string uInput = "empty";                

                inputOutputModule.ClearMenu();
                inputOutputModule.WriteOutput("List of clients\n");
                foreach (var client in clients)
                {
                    if (showDbInfoService.GetAccountByClientId(client.Id).Zone != "white")
                    {
                        continue;
                    }
                    goodInput.Add(client.Id.ToString());
                    inputOutputModule.WriteOutput($"ID: {client.Id} Name: {client.Name} Surname: {client.Surname} Phone: {client.PhoneNumber} Balance: {showDbInfoService.GetAccountByClientId(client.Id).Balance}\n");
                }
                inputOutputModule.WriteOutput(phraseProvider.GetPhrase(gameSettings.LangPackName, KeysForPhrases.TradeBuyer));
                for (bool i = false; i != true;)
                {
                    uInput = inputOutputModule.ReadInput();
                    if (ValidateUserInput(userInput, true) == InputCheckResult.Valid)
                    {
                        if (goodInput.Contains(uInput))
                        {
                            i = true;
                        }
                    }
                }
                transactionInfo.BuyerId = Convert.ToInt32(uInput);

                goodInput.Clear();
                inputOutputModule.ClearMenu();
                inputOutputModule.WriteOutput("List of clients\n");
                foreach (var client in clients)
                {
                    if (showDbInfoService.GetAccountByClientId(client.Id).Zone == "black")
                    {
                        continue;
                    }
                    if(client.Id == transactionInfo.BuyerId)
                    {
                        continue;
                    }
                    if (showDbInfoService.GetAllStocksOfClient().DefaultIfEmpty(null).FirstOrDefault(f=>f.AccountForStock.ClientId == client.Id) == null)
                    {
                        continue;
                    }
                    goodInput.Add(client.Id.ToString());
                    inputOutputModule.WriteOutput($"ID: {client.Id} Name: {client.Name} Surname: {client.Surname} Phone: {client.PhoneNumber} Balance: {showDbInfoService.GetAccountByClientId(client.Id).Balance}\n");
                }
                inputOutputModule.WriteOutput(phraseProvider.GetPhrase(gameSettings.LangPackName, KeysForPhrases.TradeSeller));
                for (bool i = false; i != true;)
                {                    
                    uInput = inputOutputModule.ReadInput();
                    if (ValidateUserInput(userInput, true) == InputCheckResult.Valid)
                    {
                        if (goodInput.Contains(uInput))
                        {
                            i = true;
                        }
                    }
                }
                transactionInfo.SellerId = Convert.ToInt32(uInput);

                goodInput.Clear();
                inputOutputModule.ClearMenu();
                inputOutputModule.WriteOutput("Seller's stocks\n");
                foreach (var stock in showDbInfoService.GetAccountByClientId(transactionInfo.SellerId).Stocks)
                {
                    goodInput.Add(stock.TypeOfStocks);
                    inputOutputModule.WriteOutput($"Type: {stock.TypeOfStocks} Quantity: {stock.quantityOfStocks} Price per stock: {showDbInfoService.GetStockPriceByType(stock.TypeOfStocks).PriceOfStock}\n");
                }
                inputOutputModule.WriteOutput(phraseProvider.GetPhrase(gameSettings.LangPackName, KeysForPhrases.TradeStockType));
                for (bool i = false; i != true;)
                {
                    uInput = inputOutputModule.ReadInput();
                    if (ValidateUserInput(userInput) == InputCheckResult.Valid)
                    {
                        if (goodInput.Contains(uInput))
                        {
                            i = true;
                        }
                    }
                }
                transactionInfo.TypeOfStock = uInput;

                goodInput.Clear();
                inputOutputModule.ClearMenu();
                int quantityOfSellerStocks = showDbInfoService.GetAccountByClientId(transactionInfo.SellerId).Stocks.First(f => f.TypeOfStocks == transactionInfo.TypeOfStock).quantityOfStocks;
                inputOutputModule.WriteOutput($"Quantity of chosen stock: {quantityOfSellerStocks}\n");
                inputOutputModule.WriteOutput(phraseProvider.GetPhrase(gameSettings.LangPackName, KeysForPhrases.TradeStockQuant));
                for (bool i = false; i != true;)
                {
                    uInput = inputOutputModule.ReadInput();
                    if (ValidateUserInput(userInput, true) == InputCheckResult.Valid)
                    {
                        if (Convert.ToInt32(uInput) <= quantityOfSellerStocks)
                        {
                            i = true;
                        }
                    }
                }
                transactionInfo.QuantityOfStocks = Convert.ToInt32(uInput);

                uInput = AskUser(KeysForPhrases.TradeDeal, true);
                if(Convert.ToInt32(uInput) == 1)
                {
                    tradingService.MakeATrade(transactionInfo);
                }
            }

            if (userInput == "2")
            {
                string uInput = "empty";
                goodInput.Clear();
                inputOutputModule.ClearMenu();
                inputOutputModule.WriteOutput("List of clients\n");
                foreach (var client in clients)
                {
                    if (showDbInfoService.GetAccountByClientId(client.Id).Zone == "black")
                    {
                        continue;
                    }
                    if (!showDbInfoService.GetAccountByClientId(client.Id).Stocks.Any())
                    {
                        continue;
                    }

                    goodInput.Add(client.Id.ToString());
                    inputOutputModule.WriteOutput($"ID: {client.Id} Name: {client.Name} Surname: {client.Surname} Phone: {client.PhoneNumber} Balance: {showDbInfoService.GetAccountByClientId(client.Id).Balance}\n");
                }
                inputOutputModule.WriteOutput(phraseProvider.GetPhrase(gameSettings.LangPackName, KeysForPhrases.TradeSeller));
                for (bool i = false; i != true;)
                {
                    uInput = inputOutputModule.ReadInput();
                    if (ValidateUserInput(userInput, true) == InputCheckResult.Valid)
                    {
                        if (goodInput.Contains(uInput))
                        {
                            i = true;
                        }
                    }
                }
                transactionInfo.SellerId = Convert.ToInt32(uInput);

                goodInput.Clear();
                inputOutputModule.ClearMenu();
                inputOutputModule.WriteOutput("Seller's stocks\n");
                foreach (var stock in showDbInfoService.GetAccountByClientId(transactionInfo.SellerId).Stocks)
                {
                    goodInput.Add(stock.TypeOfStocks);
                    inputOutputModule.WriteOutput($"Type: {stock.TypeOfStocks} Quantity: {stock.quantityOfStocks} Price per stock: {showDbInfoService.GetStockPriceByType(stock.TypeOfStocks).PriceOfStock}\n");
                }
                inputOutputModule.WriteOutput(phraseProvider.GetPhrase(gameSettings.LangPackName, KeysForPhrases.TradeStockType));
                for (bool i = false; i != true;)
                {
                    uInput = inputOutputModule.ReadInput();
                    if (ValidateUserInput(userInput) == InputCheckResult.Valid)
                    {
                        if (goodInput.Contains(uInput))
                        {
                            i = true;
                        }
                    }
                }
                transactionInfo.TypeOfStock = inputOutputModule.ReadInput();

                goodInput.Clear();
                inputOutputModule.ClearMenu();
                int quantityOfSellerStocks = showDbInfoService.GetAccountByClientId(transactionInfo.SellerId).Stocks.First(f => f.TypeOfStocks == transactionInfo.TypeOfStock).quantityOfStocks;
                inputOutputModule.WriteOutput($"Quantity of chosen stock: {quantityOfSellerStocks}\n");
                inputOutputModule.WriteOutput(phraseProvider.GetPhrase(gameSettings.LangPackName, KeysForPhrases.TradeStockQuant));
                for (bool i = false; i != true;)
                {
                    uInput = inputOutputModule.ReadInput();
                    if (ValidateUserInput(userInput, true) == InputCheckResult.Valid)
                    {
                        if (Convert.ToInt32(uInput) <= quantityOfSellerStocks)
                        {
                            i = true;
                        }
                    }
                }
                transactionInfo.QuantityOfStocks = Convert.ToInt32(inputOutputModule.ReadInput());

                goodInput.Clear();
                inputOutputModule.ClearMenu();
                inputOutputModule.WriteOutput("List of clients\n");
                foreach (var client in clients)
                {
                    if (client.Id == transactionInfo.SellerId)
                    {
                        continue;
                    }
                    if (showDbInfoService.GetAccountByClientId(client.Id).Zone != "white")
                    {
                        continue;
                    }
                    goodInput.Add(client.Id.ToString());
                    inputOutputModule.WriteOutput($"ID: {client.Id} Name: {client.Name} Surname: {client.Surname} Phone: {client.PhoneNumber} Balance: {showDbInfoService.GetAccountByClientId(client.Id).Balance}\n");
                }
                inputOutputModule.WriteOutput(phraseProvider.GetPhrase(gameSettings.LangPackName, KeysForPhrases.TradeBuyer));
                for (bool i = false; i != true;)
                {
                    uInput = inputOutputModule.ReadInput();
                    if (ValidateUserInput(userInput, true) == InputCheckResult.Valid)
                    {
                        if (goodInput.Contains(uInput))
                        {
                            i = true;
                        }
                    }
                }
                transactionInfo.BuyerId = Convert.ToInt32(inputOutputModule.ReadInput());

                goodInput.Clear();
                uInput = AskUser(KeysForPhrases.TradeDeal, true);
                if (Convert.ToInt32(uInput) == 1)
                {
                    tradingService.MakeATrade(transactionInfo);
                }
            }
        }

        private string AskUser(KeysForPhrases question = KeysForPhrases.ExitKey, bool isUserSupposedToEnterNumber = false)
        {

            string userInput = "empty";
            for (InputCheckResult i = InputCheckResult.Invalid; i != InputCheckResult.Valid;)
            {
                if (question != KeysForPhrases.ExitKey)// ExitKey used as flag for execution without question
                {
                    inputOutputModule.ClearMenu();
                    inputOutputModule.WriteOutput(phraseProvider.GetPhrase(gameSettings.LangPackName, question));
                }
                userInput = inputOutputModule.ReadInput();
                i = ValidateUserInput(userInput, isUserSupposedToEnterNumber);
            }
            return userInput;
        }

        private InputCheckResult ValidateUserInput(string userInput, bool isUserSupposedToEnterNumber = false)
        {
            //check if empty
            if (string.IsNullOrWhiteSpace(userInput))
            {
                return InputCheckResult.Invalid;
            }

            if(isUserSupposedToEnterNumber)
            {
                try
                {
                    Convert.ToInt32(userInput);
                }
                catch(Exception)
                {
                    return InputCheckResult.Invalid;
                }
            }

            return InputCheckResult.Valid;
        }

    }
}
