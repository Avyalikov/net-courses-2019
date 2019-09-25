using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TradeSimulator.Client.Dto;
using TradeSimulator.Client.Interfaces;
using TradeSimulator.Client.Misc;
using TradeSimulator.Client.Modules;

namespace TradeSimulator.Client
{
    internal class ClientApp
    {
        private readonly RequestSenderModule requestSender;
        private readonly IInputOutput inputOutputModule;
        private readonly IPhraseProvider phraseProvider;
        private readonly GameSettings gameSettings;

        public ClientApp(RequestSenderModule requestSender, IInputOutput inputOutputModule, IPhraseProvider phraseProvider, ISettingsProvider settingsProvider)
        {
            this.requestSender = requestSender;
            this.phraseProvider = phraseProvider;
            this.inputOutputModule = inputOutputModule;

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
            for (bool i = true; i;)
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
            inputOutputModule.WriteOutput(phraseProvider.GetPhrase(gameSettings.LangPackName, KeysForPhrases.GeneralAutoTrade));
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
                AutoTrading();
            }

            if (userInput == "4")
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
            inputOutputModule.WriteOutput(phraseProvider.GetPhrase(gameSettings.LangPackName, KeysForPhrases.ShowBalance));
            inputOutputModule.WriteOutput(phraseProvider.GetPhrase(gameSettings.LangPackName, KeysForPhrases.ShowHistory));
            inputOutputModule.WriteOutput(gameSettings.ExitButton);
            inputOutputModule.WriteOutput(phraseProvider.GetPhrase(gameSettings.LangPackName, KeysForPhrases.ExitKey));

            userInput = inputOutputModule.ReadInput();

            if (userInput == "1")
            {
                inputOutputModule.ClearMenu();
                inputOutputModule.WriteOutput("List of clients\n");
                int lengthOfPage = Convert.ToInt32(AskUser(KeysForPhrases.ShowClientsLength, true));
                int numberOfPage = Convert.ToInt32(AskUser(KeysForPhrases.ShowClientsPage, true));

                foreach (var client in requestSender.GetPageOfClientsList(lengthOfPage, numberOfPage))
                {
                    inputOutputModule.WriteOutput($"ID: {client.Id} Name: {client.Name} Surname: {client.Surname} Phone: {client.PhoneNumber}\n");
                }
            }

            if (userInput == "2")
            {
                int prevID = -1;
                string filter = AskUser(KeysForPhrases.ShowStocksOfClientsFilter);
                if (filter == "all")
                {
                    inputOutputModule.ClearMenu();
                    inputOutputModule.WriteOutput("List of client's stocks\n");
                    foreach (var stock in requestSender.GetAllStocksOfClientsList())
                    {
                        if (stock.ClientId != prevID)
                        {
                            inputOutputModule.WriteOutput($"ClientID: {stock.ClientId}\n");
                        }
                        inputOutputModule.WriteOutput($"Type: {stock.TypeOfStocks} Quantity: {stock.quantityOfStocks} Price: {stock.PriceOfStock}\n");
                        prevID = stock.ClientId;
                    }
                }

                if (ValidateUserInput(filter, true) == InputCheckResult.Valid)
                {
                    int clientId = Convert.ToInt32(filter);
                    inputOutputModule.ClearMenu();
                    if(CheckIfClientIdIsValid(clientId) == InputCheckResult.Invalid)
                    {
                        inputOutputModule.WriteOutput("Invalid Id\n");
                        inputOutputModule.ReadKey();
                        return true;
                    }
                    inputOutputModule.WriteOutput("List of client's stocks\n");
                    foreach (var stock in requestSender.GetAllStocksOfClientListByClientId(clientId))
                    {
                        if (stock.ClientId != prevID)
                        {
                            inputOutputModule.WriteOutput($"ClientID: {stock.ClientId}\n");
                        }
                        inputOutputModule.WriteOutput($"Type: {stock.TypeOfStocks} Quantity: {stock.quantityOfStocks} Price: {stock.PriceOfStock}\n");
                        prevID = stock.ClientId;
                    }
                }
            }

            if (userInput == "3")//balance
            {
                inputOutputModule.ClearMenu();
                inputOutputModule.WriteOutput(phraseProvider.GetPhrase(gameSettings.LangPackName, KeysForPhrases.ShowBalance));

                int clientId = Convert.ToInt32(AskUser(KeysForPhrases.ShowBalanceId, true));

                inputOutputModule.ClearMenu();

                if (CheckIfClientIdIsValid(clientId) == InputCheckResult.Invalid)
                {
                    inputOutputModule.WriteOutput("Invalid Id\n");
                    inputOutputModule.ReadKey();
                    return true;
                }

                inputOutputModule.WriteOutput($"Balance of client {clientId}\n");

                var account = requestSender.GetAccountByClientId(clientId);
                
                inputOutputModule.WriteOutput($"Balance: {account.Balance} Zone: {account.Zone}\n");
                
            }

            if (userInput == "4")
            {
                int clientId = Convert.ToInt32(AskUser(KeysForPhrases.ShowHistoryId, true));
                int top = Convert.ToInt32(AskUser(KeysForPhrases.ShowHistoryQuant, true));

                inputOutputModule.ClearMenu();
                if (CheckIfClientIdIsValid(clientId) == InputCheckResult.Invalid)
                {
                    inputOutputModule.WriteOutput("Invalid Id\n");
                    inputOutputModule.ReadKey();
                    return true;
                }
                inputOutputModule.WriteOutput("History of transaction\n");
                foreach (var history in requestSender.GetNClientsHistoryRecords(clientId, top))
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

        //private void Show()
        //{
        //    inputOutputModule.ClearMenu();
        //    inputOutputModule.WriteOutput("List of clients\n");

        //    foreach (var client in requestSender.GetAllTradersList())
        //    {
        //        inputOutputModule.WriteOutput($"ID: {client.Id} Name: {client.Name} Surname: {client.Surname} Phone: {client.PhoneNumber}\n");
        //    }
        //}

        private InputCheckResult CheckIfClientIdIsValid(int clientId)
        {
            if (requestSender.GetAllClients().Any(a=>a.Id == clientId))
            {
                return InputCheckResult.Valid;
            }
            return InputCheckResult.Invalid;
        }


        private void Registration()
        {
            RegInfo regInfo = new RegInfo();

            regInfo.Name = AskUser(KeysForPhrases.RegName);

            regInfo.Surname = AskUser(KeysForPhrases.RegSurname);

            regInfo.PhoneNumber = AskUser(KeysForPhrases.RegPhone, true);
                   
            regInfo.Balance = Convert.ToDecimal(AskUser(KeysForPhrases.RegBalance, true));

            if (Convert.ToInt32(AskUser(KeysForPhrases.RegStock, true)) == 1)
            {
                regInfo.HaveStoks = true;

                regInfo.TypeOfStocks = AskUser(KeysForPhrases.RegStockType);

                regInfo.quantityOfStocks = Convert.ToInt32(AskUser(KeysForPhrases.RegStockQuant, true));

                if (!requestSender.GetCheckResultIfStockPriceAlreadyExists(regInfo.TypeOfStocks))
                {
                    regInfo.PriceOfStock = Convert.ToDecimal(AskUser(KeysForPhrases.RegStockPrice, true));                                        
                }

                requestSender.RegisterNewClient(regInfo);
                return;
            }

            regInfo.HaveStoks = false;
            requestSender.RegisterNewClient(regInfo);
            return;
        }

        private void Trading()
        {
            TransactionInfo transactionInfo = new TransactionInfo();
            var clients = requestSender.GetAllClients();
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
                    if (requestSender.GetAccountByClientId(client.Id).Zone != "white")
                    {
                        continue;
                    }
                    goodInput.Add(client.Id.ToString());
                    inputOutputModule.WriteOutput($"ID: {client.Id} Name: {client.Name} Surname: {client.Surname} Phone: {client.PhoneNumber} Balance: {requestSender.GetAccountByClientId(client.Id).Balance}\n");
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
                    if (requestSender.GetAccountByClientId(client.Id).Zone == "black")
                    {
                        continue;
                    }
                    if (client.Id == transactionInfo.BuyerId)
                    {
                        continue;
                    }
                    if (requestSender.GetAllStocksOfClientsList().DefaultIfEmpty(null).FirstOrDefault(f => f.ClientId == client.Id) == null)
                    {
                        continue;
                    }
                    goodInput.Add(client.Id.ToString());
                    inputOutputModule.WriteOutput($"ID: {client.Id} Name: {client.Name} Surname: {client.Surname} Phone: {client.PhoneNumber} Balance: {requestSender.GetAccountByClientId(client.Id).Balance}\n");
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
                foreach (var stock in requestSender.GetAllStocksOfClientListByClientId(transactionInfo.SellerId))
                {
                    goodInput.Add(stock.TypeOfStocks);
                    inputOutputModule.WriteOutput($"Type: {stock.TypeOfStocks} Quantity: {stock.quantityOfStocks} Price per stock: {stock.PriceOfStock}\n");
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
                int quantityOfSellerStocks = requestSender.GetAllStocksOfClientListByClientId(transactionInfo.SellerId).First(f => f.TypeOfStocks == transactionInfo.TypeOfStock).quantityOfStocks;
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
                if (Convert.ToInt32(uInput) == 1)
                {
                    requestSender.MakeATrade(transactionInfo);
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
                    if (requestSender.GetAccountByClientId(client.Id).Zone == "black")
                    {
                        continue;
                    }
                    if (!requestSender.GetAllStocksOfClientsList().Any(a=>a.ClientId == client.Id))
                    {
                        continue;
                    }

                    goodInput.Add(client.Id.ToString());
                    inputOutputModule.WriteOutput($"ID: {client.Id} Name: {client.Name} Surname: {client.Surname} Phone: {client.PhoneNumber} Balance: {requestSender.GetAccountByClientId(client.Id).Balance}\n");
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
                foreach (var stock in requestSender.GetAllStocksOfClientListByClientId(transactionInfo.SellerId))
                {
                    goodInput.Add(stock.TypeOfStocks);
                    inputOutputModule.WriteOutput($"Type: {stock.TypeOfStocks} Quantity: {stock.quantityOfStocks} Price per stock: {stock.PriceOfStock}\n");
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
                int quantityOfSellerStocks = requestSender.GetAllStocksOfClientListByClientId(transactionInfo.SellerId).First(f => f.TypeOfStocks == transactionInfo.TypeOfStock).quantityOfStocks;
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

                goodInput.Clear();
                inputOutputModule.ClearMenu();
                inputOutputModule.WriteOutput("List of clients\n");
                foreach (var client in clients)
                {
                    if (client.Id == transactionInfo.SellerId)
                    {
                        continue;
                    }
                    if (requestSender.GetAccountByClientId(client.Id).Zone != "white")
                    {
                        continue;
                    }
                    goodInput.Add(client.Id.ToString());
                    inputOutputModule.WriteOutput($"ID: {client.Id} Name: {client.Name} Surname: {client.Surname} Phone: {client.PhoneNumber} Balance: {requestSender.GetAccountByClientId(client.Id).Balance}\n");
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
                uInput = AskUser(KeysForPhrases.TradeDeal, true);
                if (Convert.ToInt32(uInput) == 1)
                {
                    requestSender.MakeATrade(transactionInfo);
                }
            }
        }

        public void AutoTrading()
        {
            Random random = new Random();
            List<int> badIdForSeller = new List<int>();
            TransactionInfo transactionInfo = new TransactionInfo();
            var clients = this.requestSender.GetAllClients();
            inputOutputModule.ClearMenu();

            do
            {
                transactionInfo.BuyerId = clients.ElementAt(random.Next(0, clients.Count - 1)).Id;
                badIdForSeller.Add(transactionInfo.BuyerId);
                transactionInfo.SellerId = clients.ElementAt(random.Next(0, clients.Count - 1)).Id;

                if (transactionInfo.BuyerId == transactionInfo.SellerId)
                {
                    do
                    {
                        transactionInfo.SellerId = clients.ElementAt(random.Next(0, clients.Count - 1)).Id;
                        if (!requestSender.GetAllStocksOfClientsList().Any(a=>a.ClientId == transactionInfo.SellerId))
                        {
                            badIdForSeller.Add(transactionInfo.SellerId);
                        }
                    } while (badIdForSeller.Contains(transactionInfo.SellerId));
                }

                var sellersAccount = this.requestSender.GetAccountByClientId(transactionInfo.SellerId);
                var sellerStocks = requestSender.GetAllStocksOfClientListByClientId(sellersAccount.ClientId);
                var StockForSell = sellerStocks.ElementAt(random.Next(0, sellerStocks.Count - 1));

                transactionInfo.TypeOfStock = StockForSell.TypeOfStocks;
                transactionInfo.QuantityOfStocks = random.Next(1, StockForSell.quantityOfStocks);

                inputOutputModule.WriteOutput($"BuyerId: {transactionInfo.BuyerId} SellerId: {transactionInfo.SellerId} Type of stock: {transactionInfo.TypeOfStock} Quantitiy: {transactionInfo.QuantityOfStocks} Total price: {(transactionInfo.QuantityOfStocks* StockForSell.PriceOfStock)}\n");

                this.requestSender.MakeATrade(transactionInfo);

                Thread.Sleep(10000);
            } while (!this.requestSender.GetCheckIfBlackZoneIsNotEmpty());
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

            if (isUserSupposedToEnterNumber)
            {
                try
                {
                    Convert.ToInt32(userInput);
                }
                catch (Exception)
                {
                    return InputCheckResult.Invalid;
                }
            }

            return InputCheckResult.Valid;
        }
    }
}
