namespace TradingApp.ConsoleTradingManager
{
    using System;
    using System.Linq;
    using System.Threading;
    using TradingApp.Core;
    using TradingApp.Core.DTO;

    public class ConsoleManager
    {
        private readonly RequestSender requestSender;

        public ConsoleManager(RequestSender requestSender)
        {
            this.requestSender = requestSender;
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
                    sellerId = rand.Next(1, this.requestSender.GetAllTradersList().Count() + 1);
                    buyerId = rand.Next(1, this.requestSender.GetAllTradersList().Count() + 1);
                    var sellerShares = this.requestSender.GetAllSharesByTrader(sellerId);
                    shareId = sellerShares[rand.Next(0, sellerShares.Count())].Id;

                    var transactionInfo = new TransactionInfo
                    {
                        BuyerId = buyerId,
                        SellerId = sellerId,
                        ShareId = shareId
                    };

                    Console.WriteLine(requestSender.MakeTransaction(transactionInfo));
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
                var allTraders = requestSender.GetOrangeStatusClients();
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
                var allTraders = requestSender.GetBlackStatusClients();
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

        private void MakeTransaction()
        {
            Console.Clear();
            Console.WriteLine("Проведение сделки");
            Console.WriteLine("Чтобы провести сделку необходимо выбрать покупателя, продавца и акцию, которая будет являться предметом продажи.");
            Console.WriteLine("Чтобы вернуться в меню введите menu на любом из этапов заполнения");
            Console.WriteLine("Список пользователей, находящихся на бирже:");

            var allTraders = requestSender.GetAllTradersList();
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

            var sellersShares = requestSender.GetAllSharesByTrader(sellerId);
            Console.WriteLine("Продавец обладает следующими акциями:");
            Console.WriteLine("{0, -3} {1, -15} {2, -10} {3, -9} {4,-10}", "ID", "Компания", "Количество", "Стоимость", "Тип");
            foreach (var item in sellersShares)
            {
                Console.WriteLine($"{item.Id,-3} {item.Stock.Company.Name,-15} " +
                    $"{item.Amount,-10} {item.Stock.PricePerUnit * item.Amount * item.ShareType.Multiplier,-9:0.00} " +
                    $"{item.ShareType.Name,-10}");
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
                var transactionInfo = new TransactionInfo
                {
                    BuyerId = buyerId,
                    SellerId = sellerId,
                    ShareId = shareId
                };
                requestSender.MakeTransaction(transactionInfo);

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

            var sharesWithInfo = requestSender.GetAllShares();

            foreach (var item in sharesWithInfo)
            {
                Console.Write($"{item.Id,-5} |{item.Owner.FirstName + " " + item.Owner.LastName,-20} " +
                    $"|{item.Stock.Company.Name,-15} |{item.Amount,-10} |{item.ShareType.Name,-10} " +
                    $"|{item.Amount * item.Stock.PricePerUnit * item.ShareType.Multiplier:0.00}\n");
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
                Console.WriteLine(requestSender.ChangeShareType(shareId, shareTypeId));
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
                var resp = requestSender.RegisterTrader(
                    new TraderInfo()
                    { FirstName = firstName, LastName = lastName, PhoneNumber = phoneNumber, Balance = balance });

                Console.WriteLine(resp);
            }
            catch (Exception ex)
            {
                Logger.ConsoleLogger.Error(ex.Message);
            }
            Console.WriteLine("Нажмите любую клавишу, чтобы вернуться в меню...");
            Console.ReadKey();
        }
    }
}
