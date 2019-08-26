using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingApp.Core.Models;
using TradingApp.Core.Repositories;
using TradingApp.Core.Services;
using TradingApp.ConsoleTradingManager.Repositories;
using TradingApp.Core;
using TradingApp.Core.DTO;
using System.Threading;

namespace TradingApp.ConsoleTradingManager
{
    public class ConsoleManager
    {
        private readonly TransactionService transactionService;
        private readonly TraderService traderService;
        private readonly ShareService shareService;
        private readonly IRepository<TraderEntity> traderTableRepository;
        private readonly IRepository<ShareEntity> shareTableRepository;
        private readonly IRepository<CompanyEntity> companyTableRepository;
        private readonly IRepository<StockEntity> stockTableRepository;
        private readonly IRepository<ShareTypeEntity> shareTypeTableRepository;
        private readonly IRepository<TransactionEntity> transactionTableRepository;

        public ConsoleManager(
            TraderService traderService, 
            ShareService shareService, 
            TransactionService transactionService,
            IRepository<TraderEntity> traderTableRepository,
            IRepository<ShareEntity> shareTableRepository,
            IRepository<CompanyEntity> companyTableRepository,
            IRepository<StockEntity> stockTableRepository,
            IRepository<ShareTypeEntity> shareTypeTableRepository,
            IRepository<TransactionEntity> transactionTableRepository)
        {
            this.traderService = traderService;
            this.shareService = shareService;
            this.transactionService = transactionService;
            this.traderTableRepository = traderTableRepository;
            this.shareTableRepository = shareTableRepository;
            this.companyTableRepository = companyTableRepository;
            this.stockTableRepository = stockTableRepository;
            this.shareTypeTableRepository = shareTypeTableRepository;
            this.transactionTableRepository = transactionTableRepository;
        }
        private void ShowMenu()
        {
            Console.WriteLine("Добро пожаловать в менеджер симулятора торговли!");
            Console.WriteLine("Здесь вы можете сделать следующее:");
            Console.WriteLine("Нажмите 1, чтобы добавить нового пользователя в систему.");
            Console.WriteLine("Нажмите 2, чтобы изменить тип акции пользователя.");
            Console.WriteLine("Нажмите 3, чтобы провести сделку вручную.");
            Console.WriteLine("Нажмите 4, чтобы вывести список трейдеров в оранжевой зоне. (т.е. тех, у кого баланс равен нулю.)");
            Console.WriteLine("Нажмите 5, чтобы вывести список трейдеров в черной зоне. (т.е. тех, у кого баланс меньше нуля.)");
            Console.WriteLine("Нажмите 6, чтобы начать процесс имитации торгов.");
            Console.WriteLine("Нажмите E, чтобы выйти.");
        }

        internal void Start()
        {
            do
            {
                Console.Clear();
                ShowMenu();

                var pressedKey = Console.ReadKey();
                if (pressedKey.Key == ConsoleKey.E)
                    break;
                
                switch (pressedKey.Key)
                {
                    case ConsoleKey.D1:
                        RegisterNewTrader();
                        break;
                    case ConsoleKey.D2:
                        ChangeShareType();
                        break;
                    case ConsoleKey.D3:
                        MakeTransaction();
                        break;
                    case ConsoleKey.D4:
                        ShowTradersFromOrangeZone();
                        break;
                    case ConsoleKey.D5:
                        ShowTradersFromBlackZone();
                        break;
                    case ConsoleKey.D6:
                        StartTradingProcess();
                        break;
                }
            } while (true);
        }

        private void StartTradingProcess()
        {
            Console.Clear();
            Console.WriteLine("Процесс имитации торгов по времени");
            Console.WriteLine("Внимание! Процесс имитации торгов по времени не остановить после запуска. ");
            Console.WriteLine("Вы можете сейчас вернуться в меню введя \"menu\" и нажав Enter. Любой другой введенный текст запустит торги.");
            Random rand = new Random();
            string consoleInput = Console.ReadLine();
            if (consoleInput.ToLower() == "menu") return;

            while (true)
            {
                try
                {
                    int sellerId, buyerId, shareId;
                    sellerId = rand.Next(1, this.traderTableRepository.GetAll().Count+1);
                    buyerId = rand.Next(1, this.traderTableRepository.GetAll().Count+1);
                    var sellerShares = this.shareService.GetAllSharesByTraderId(sellerId);
                    shareId = sellerShares[rand.Next(0, sellerShares.Count())].Id;

                    var transactionId = transactionService.MakeShareTransaction(sellerId, buyerId, shareId);

                    var transaction = this.transactionTableRepository.GetById(transactionId);
                    Console.WriteLine($"Проведена сделка между {transaction.Seller.FirstName + " " + transaction.Seller.LastName} " +
                        $"и {transaction.Buyer.FirstName + " " + transaction.Buyer.LastName} стоимостью {transaction.TransactionPayment:0.00}");
                }
                catch (Exception ex)
                {
                    Logger.ConsoleLogger.Error(ex.Message);
                }

                Thread.Sleep(10000);
            }
        }

        private void ShowTradersFromOrangeZone()
        {
            Console.Clear();
            Console.WriteLine("Список пользователей, находящихся в чёрной зоне");
            Console.WriteLine("В оранжевую зону попадают те пользователи, чей баланс равен нулю.");
            Console.WriteLine("Список пользователей, находящихся в оранжевой зоне:");
            Console.WriteLine("{0, -3} |{1, -20} |{2, -6}", "ID", "Пользователь", "Баланс");

            try
            {
                var allTraders = traderService.GetTradersFromOrangeZone();
                foreach (var trader in allTraders)
                {
                    Console.WriteLine($"{trader.Id,-3} |{trader.FirstName + " " + trader.LastName,-20} |{trader.Balance,-6}");
                }
            }
            catch (Exception ex)
            {
                Logger.ConsoleLogger.Error(ex.Message);
            }
            Console.WriteLine("Нажмите любую клавишу, чтобы вернуться в меню...");
            Console.ReadKey();
        }

        private void ShowTradersFromBlackZone()
        {
            Console.Clear();
            Console.WriteLine("Список пользователей, находящихся в чёрной зоне");
            Console.WriteLine("В чёрную зону попадают те пользователи, чей баланс ниже нуля.");
            Console.WriteLine("Список пользователей, находящихся в чёрной зоне:");
            Console.WriteLine("{0, -3} |{1, -20} |{2, -6}", "ID", "Пользователь", "Баланс");

            try
            {
                var allTraders = traderService.GetTradersFromBlackZone();
                foreach (var trader in allTraders)
                {
                    Console.WriteLine($"{trader.Id, -3} |{trader.FirstName + " " + trader.LastName, -20} |{trader.Balance, -6}");
                }
            }
            catch(Exception ex)
            {
                Logger.ConsoleLogger.Error(ex.Message);
            }
            Console.WriteLine("Нажмите любую клавишу, чтобы вернуться в меню...");
            Console.ReadKey();
        }

        private void MakeTransaction()
        {
            Console.Clear();
            Console.WriteLine("Проведение сделки");
            Console.WriteLine("Чтобы провести сделку необходимо выбрать покупателя, продавца и акцию, которая будет являться предметом продажи.");
            Console.WriteLine("Чтобы вернуться в меню введите menu на любом из этапов заполнения");
            Console.WriteLine("Список пользователей, находящихся на бирже:");

            var allTraders = traderService.GetAllTraders();
            Console.WriteLine("ID\tПользователь");
            foreach (var trader in allTraders)
            {
                Console.WriteLine($"{trader.Id}\t{trader.FirstName} {trader.LastName}");
            }
            
            int sellerId;
            string consoleInput;
            do
            {
                Console.Write("Введите идентификатор пользователя, который является продавцом: ");
                consoleInput = Console.ReadLine();
                if (consoleInput.ToLower() == "menu") return;
            } while (!int.TryParse(consoleInput, out sellerId) || sellerId < allTraders.Min(t => t.Id) || sellerId > allTraders.Max(t => t.Id));

            int buyerId;
            do
            {
                Console.Write("Введите идентификатор пользователя, который является покупателем: ");
                consoleInput = Console.ReadLine();
                if (consoleInput.ToLower() == "menu") return;
            } while (!int.TryParse(consoleInput, out buyerId) || buyerId < allTraders.Min(t => t.Id) || buyerId > allTraders.Max(t => t.Id) || sellerId == buyerId);

            var sellersShares = from share in this.shareTableRepository.GetAll().Where(s => s.Owner.Id == sellerId)
                                join trader in this.traderTableRepository.GetAll() on share.Owner equals trader
                                join stock in this.stockTableRepository.GetAll() on share.Stock equals stock
                                join company in this.companyTableRepository.GetAll() on stock.Company equals company
                                join shareType in this.shareTypeTableRepository.GetAll() on share.ShareType equals shareType
                                select new {
                                    share.Id,
                                    CompanyName = company.Name,
                                    share.Amount,
                                    PackagePrice = share.Amount * shareType.Multiplier * stock.PricePerUnit,
                                    ShareTypeName = shareType.Name
                                    };
            Console.WriteLine("Продавец обладает следующими акциями:");
            Console.WriteLine("{0, -3} {1, -15} {2, -10} {3, -9} {4,-10}", "ID", "Компания", "Количество", "Стоимость", "Тип");
            foreach (var item in sellersShares)
            {
                Console.WriteLine($"{item.Id, -3} {item.CompanyName, -15} {item.Amount, -10} {item.PackagePrice, -9:0.00} {item.ShareTypeName, -10}");
            }
            int shareId;
            do
            {
                Console.Write("Введите идентификатор акции, которую хочет купить покупатель: ");
                consoleInput = Console.ReadLine();
                if (consoleInput.ToLower() == "menu") return;
            } while (!int.TryParse(consoleInput, out shareId) || !sellersShares.Any(s => s.Id == shareId) || !sellersShares.Any(o => o.Id == shareId));

            try
            {
                transactionService.MakeShareTransaction(sellerId, buyerId, shareId);

                Console.WriteLine("Продажа проведена успешно!");
            }
            catch (Exception ex)
            {
                Logger.ConsoleLogger.Error(ex.Message);
            }
            Console.WriteLine("Нажмите любую клавишу, чтобы вернуться в меню...");
            Console.ReadKey();
        }
        
        private void ChangeShareType()
        {
            Console.Clear();
            Console.WriteLine("Изменение типа акции");
            Console.WriteLine("Чтобы изменить тип акции необходимо выбрать акцию, находящуюся в торговле, введя её номер.");
            Console.WriteLine("Чтобы вернуться в меню введите menu на любом из этапов заполнения");
            Console.WriteLine("Список акций, находящихся на бирже:");
            Console.WriteLine("{0, -5} |{1,-20} |{2,-15} |{3,-10} |{4,-10} |{5}", 
                "Номер", "Владелец", "Компания", "Количество", "Тип акции", "Стоимость пакета");

            var sharesWithInfo = from share in this.shareTableRepository.GetAll()
                                 join trader in this.traderTableRepository.GetAll() on share.Owner equals trader
                                 join stock in this.stockTableRepository.GetAll() on share.Stock equals stock
                                 join company in this.companyTableRepository.GetAll() on stock.Company equals company
                                 join shareType in this.shareTypeTableRepository.GetAll() on share.ShareType equals shareType
                                 select new
                                 {
                                     share.Id,
                                     OwnerFullName = trader.FirstName + " " + trader.LastName,
                                     CompanyName = company.Name,
                                     share.Amount,
                                     PackagePrice = share.Amount * shareType.Multiplier * stock.PricePerUnit,
                                     ShareTypeName = shareType.Name
                                 };

            foreach (var item in sharesWithInfo)
            {
                Console.Write($"{item.Id, -5} |{item.OwnerFullName, -20} |{item.CompanyName,-15} |{item.Amount,-10} |{item.ShareTypeName,-10} |{item.PackagePrice:0.00}\n");
            }
            Console.WriteLine();
            int shareId;
            string consoleInput;
            do
            {
                Console.Write("Выберите номер акции, чей тип необходимо изменить: ");
                consoleInput = Console.ReadLine();
            } while (!int.TryParse(consoleInput, out shareId));
            int shareTypeId;
            do
            {
                Console.Write("Выберите тип акции, на который вы хотите изменить текущий (1 - Нормальный, 2 - Привилегированный, 3 - Особенный): ");
                consoleInput = Console.ReadLine();
            } while (!int.TryParse(consoleInput, out shareTypeId));

            try
            {
                shareService.ChangeShareType(shareId, shareTypeId);
                Console.WriteLine("Тип акции успешно изменён.");
            }
            catch (Exception ex)
            {
                Logger.ConsoleLogger.Error(ex.Message);
            }

            Console.WriteLine("Нажмите любую клавишу, чтобы вернуться в меню...");
            Console.ReadKey();
        }

        private void RegisterNewTrader()
        {
            Console.Clear();
            Console.WriteLine("Добавление пользователя");
            Console.WriteLine("Чтобы добавить пользователя в систему вам необходимо ввести его имя, фамилию, номер телефона и стартовый баланс.");
            Console.WriteLine("Чтобы вернуться в меню введите menu на любом из этапов заполнения");
            string firstName = string.Empty;
            do
            {
                Console.Write("Введите имя пользователя: ");
                firstName = Console.ReadLine();
                if (firstName.ToLower() == "menu") return;
            } while (!firstName.All(char.IsLetter) && firstName != string.Empty);
            string lastName = string.Empty;
            do
            {
                Console.Write("Введите фамилию пользователя: ");
                lastName = Console.ReadLine();
                if (lastName.ToLower() == "menu") return;
            } while (!lastName.All(char.IsLetter) && lastName != string.Empty);

            string phoneNumber = string.Empty;
            do
            {
                Console.Write("Введите номер телефона пользователя (без разделительных знаков): ");
                phoneNumber = Console.ReadLine();
                if (phoneNumber.ToLower() == "menu") return;
            } while (!phoneNumber.All(char.IsDigit) && phoneNumber != string.Empty);

            decimal balance;
            string balanceInput;
            do
            {
                Console.Write("Введите баланс пользователя: ");
                balanceInput = Console.ReadLine();
                if (balanceInput.ToLower() == "menu") return;
            } while (!decimal.TryParse(balanceInput, out balance));

            try
            {
                traderService.RegisterNewUser(
                    new TraderInfo()
                    { FirstName = firstName, LastName = lastName, PhoneNumber = phoneNumber, Balance = balance});

                Console.WriteLine("Пользователь добавлен!");
            } catch (Exception ex)
            {
                Logger.ConsoleLogger.Error(ex.Message);
            }
            Console.WriteLine("Нажмите любую клавишу, чтобы вернуться в меню...");
            Console.ReadKey();
        }
    }
}
