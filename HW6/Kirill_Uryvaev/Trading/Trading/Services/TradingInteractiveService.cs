using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core;
using Trading.Core.Services;
using Trading.Core.DataTransferObjects;

namespace Trading.ConsoleApp
{
    public class TradingInteractiveService
    {
        private readonly IPhraseProvider phraseProvider;
        private readonly IIOProvider ioProvider;
        private readonly ILogger logger;
        private readonly IValidator validator;

        private readonly IClientService clientService;
        private readonly IShareService shareService;
        private readonly IClientsSharesService clientsSharesService;

        public TradingInteractiveService(IPhraseProvider phraseProvider, IIOProvider ioProvider, ILogger logger, IValidator validator, 
            IClientService clientService, IShareService shareService, IClientsSharesService clientsSharesService)
        {
            this.phraseProvider = phraseProvider;
            this.ioProvider = ioProvider;
            this.logger = logger;
            this.validator = validator;
            this.clientService = clientService;
            this.shareService = shareService;
            this.clientsSharesService = clientsSharesService;
        }

        public void Run()
        {
            logger.InitLogger();
            string userInput = "";
            logger.WriteInfo("Program started");
            logger.WriteInfo(phraseProvider.GetPhrase("Welcome"));
            while (!userInput.ToLower().Equals("e"))
            {
                userInput = ioProvider.ReadLine();
                logger.WriteInfo($"User input: {userInput}");
                logger.RunWithExceptionLogging(() => processUserInput(userInput));
            }

            logger.WriteInfo("Program ended");
        }

        private void processUserInput(string userInput)
        {
            string[] splitedUserInput = userInput.Split(' ', '\t', ';');
            switch (splitedUserInput[0].ToLower())
            {
                case "addclient":
                    ClientRegistrationInfo clientInfo = new ClientRegistrationInfo()
                    {
                        FirstName = splitedUserInput[1],
                        LastName = splitedUserInput[2],
                        PhoneNumber = splitedUserInput[3]
                    };
                    registerEntity(validator.ValidateClientInfo, clientService.RegisterClient, clientInfo);
                    break;

                case "addshare":
                    ShareRegistrationInfo shareInfo = new ShareRegistrationInfo()
                    {
                        Name = splitedUserInput[1],
                        Cost = decimal.Parse(splitedUserInput[2])
                    };
                    registerEntity(validator.ValidateShareInfo, shareService.RegisterShare, shareInfo);
                    break;

                case "changesharestoclient":
                    ClientsSharesInfo clientsShareInfo = new ClientsSharesInfo()
                    {
                        ClientID = int.Parse(splitedUserInput[1]),
                        ShareID = int.Parse(splitedUserInput[2]),
                        Amount = int.Parse(splitedUserInput[3])
                    };
                    registerEntity(validator.ValidateShareToClient, clientsSharesService.ChangeClientsSharesAmount, clientsShareInfo);
                    break;
                case "changeclientmoney":
                    changeClientsMoney(splitedUserInput);
                    break;
                case "showorange":
                    showClientsList(clientService.GetClientsFromOrangeZone());
                    break;
                case "showblack":
                    showClientsList(clientService.GetClientsFromBlackZone());
                    break;
                case "showfullclients":
                    showClientsList(clientService.GetAllClients());
                    break;
                case "showfullshares":
                    var fullList = shareService.GetAllShares();
                    logger.WriteInfo(phraseProvider.GetPhrase("ShareHeader"));
                    foreach (ShareEntity share in fullList)
                    {
                        string sharesInfo = $"{share.ShareID.ToString()} {share.ShareName} {share.ShareCost.ToString()}";
                        logger.WriteInfo(sharesInfo);
                    }
                    logger.WriteInfo("Successfully showed full share list");
                    break;
                case "help":
                    logger.WriteInfo(phraseProvider.GetPhrase("Help"));
                    break;
                case "e":
                    break;
                default:
                    logger.WriteWarn("Unknown command");
                    return;
            }
        }

        private void registerEntity<T>(Func<T,ILogger, bool> validateFunction, Func<T,int> registerFunction, T info)
        {
            if (validateFunction(info,logger))
            {
                int result = registerFunction(info);
                logger.WriteInfo($"Successfully registry entity from {info.ToString()} with result {result}");
            }
        }

        private void changeClientsMoney(string[] splitedUserInput)
        {
            int clientID = int.Parse(splitedUserInput[1]);
            int amount = int.Parse(splitedUserInput[2]);
            if (validator.ValidateClientMoney(clientID, amount, logger))
            {
                clientService.ChangeMoney(clientID, amount);
                logger.WriteInfo($"Successfully changed balance of {clientID} by {amount}");
            }
        }

        private void showClientsList(IEnumerable<ClientEntity> clients)
        {
            logger.WriteInfo(phraseProvider.GetPhrase("ClientHeader"));
            foreach (ClientEntity client in clients)
            {
                string clientInfo = $"{client.ClientID.ToString()} {client.ClientFirstName} {client.ClientLastName} {client.PhoneNumber} {client.ClientBalance.ToString()}";
                logger.WriteInfo(clientInfo);
            }
            logger.WriteInfo("Successfully showed clients list");
        }
    }
}
