namespace TradingSimulator
{
    using Core.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Web;
    using Core;
    using View;
    using Core.Dto;
    using Newtonsoft.Json;

    internal class Controller : IController
    {
        public readonly Menu menu;
        public readonly Terminal terminal;

        private readonly IInputOutput io;
        private readonly IPhraseProvider phraseProvider;

        private readonly Random rnd = new Random();

        public Controller(
            IInputOutput io,
            IPhraseProvider phraseProvider,
            GameSettings gs)
        {
            this.io = io;
            this.phraseProvider = phraseProvider;

            Point start = (0, 0);
            int whidth = gs.whidthWindow;
            int height = gs.heightWindow;

            if (whidth < 160)
            {
                io.Print("Window width is low" + Environment.NewLine);
                throw new Exception();
            }
            if (height < 30)
            {
                io.Print("Window height is low" + Environment.NewLine);
                throw new Exception();
            }
            
            this.io.SetWindowSize(whidth + 4, height);
            terminal = new Terminal(
                (whidth / 2 + 1, start.y),
                whidth / 2,
                5,
                io,
                phraseProvider);

            menu = new Menu(
                start,
                whidth / 2,
                height,
                io,
                phraseProvider);
                
        }

        public void Run()
        {
            TradeTread();
            MenuTread();
        }

        private void TradeTread()
        {
            new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);

                    int tradersCount = int.Parse(Http.Get("localhost/clients/count"));
                    
                    int sellerId;
                    int sharesCount;
                    do
                    {
                        sellerId = this.rnd.Next(1, tradersCount + 1);
                        sharesCount = int.Parse(Http.Get($"localhost/share/count?OwnerId={sellerId}"));
                    }
                    while (sharesCount == 0);

                    int buyerId;
                    do
                    {
                        buyerId = this.rnd.Next(1, tradersCount + 1);
                    }
                    while (sellerId == buyerId);
                    
                    int index = this.rnd.Next(sharesCount);
                    var share = JsonConvert.DeserializeObject<Share>(Http.Get($"localhost/share/get?OwnerId={sellerId}&Index={index}"));
                    int quantity = this.rnd.Next(1, share.quantity + 1);

                    Http.Post($"localhost/deal/make?sellerId={sellerId}&buyerId={buyerId}" +
                        $"&shareName={share.name}&quantity={quantity}");

                    terminal.PrintTerminal(JsonConvert.DeserializeObject<List<Transaction>>(Http.Get($"localhost/transaction/list")));
                    
                }
            }).Start();
        }
        
        private void MenuTread()
        {
            
            Dictionary<string, Action> Actions = new Dictionary<string, Action>
            {
                { phraseProvider.GetPhrase(Phrase.HeaderMain), Plug },

                { phraseProvider.GetPhrase(Phrase.HeaderHistory), ShowHistory },
                { phraseProvider.GetPhrase(Phrase.HeaderTraderList), Plug },
                { phraseProvider.GetPhrase(Phrase.HeaderTraderManaging), Plug },

                { phraseProvider.GetPhrase(Phrase.HeaderAddTrader), AddTrader },
                { phraseProvider.GetPhrase(Phrase.HeaderAddShare), AddShare },
                { phraseProvider.GetPhrase(Phrase.HeaderChandeShare), ChangeShare },

                { phraseProvider.GetPhrase(Phrase.HeaderGreenList), ShowGreenList },
                { phraseProvider.GetPhrase(Phrase.HeaderOrangeList), ShowOrangeList },
                { phraseProvider.GetPhrase(Phrase.HeaderBlackList), ShowBlackList }
            };
            
            while (true)
            {
                menu.PrintHeader();
                menu.PrintDiscription();
                menu.Action(Actions);
                menu.PrintButtons();
                menu.SwitchPage();
            }
        }

        private void Plug() { }

        private void ShowHistory()
        {
            io.Print(Environment.NewLine);

            Point start = io.CursorPosition;

            int y = start.y;
            foreach (var t in JsonConvert.DeserializeObject<List<Transaction>>(Http.Get($"localhost/transaction/list")))
            {
                this.io.CursorPosition = (start.x, y);
                io.Print($" {t.seller.name} {t.seller.surname}");
                this.io.CursorPosition = (start.x + 20, y);
                io.Print($"{phraseProvider.GetPhrase(Phrase.Sold)} {t.sellerShare.quantity}");
                this.io.CursorPosition = (start.x + 20 + 8, y);
                io.Print($"{phraseProvider.GetPhrase(Phrase.SharesOf)} {t.sellerShare.name}");
                this.io.CursorPosition = (start.x + 20 + 8 + 30, y);
                io.Print($" {phraseProvider.GetPhrase(Phrase.To)} {t.buyer.name} {t.buyer.surname}");

                y++;
            }

            io.Print(Environment.NewLine);
        }
        
        private void AddTrader()
        {
            io.Print(Environment.NewLine);
            io.Print(phraseProvider.GetPhrase(Phrase.EnterName));
            string Name = io.Input();

            io.Print(phraseProvider.GetPhrase(Phrase.EnterSuname));
            string Suename = io.Input();

            io.Print(phraseProvider.GetPhrase(Phrase.EnterPhone));
            string Phone = io.Input();

            io.Print(phraseProvider.GetPhrase(Phrase.EnterMoney));
            string Money = io.Input();

            io.Print(Environment.NewLine);

            string res = Http.Post($"localhost/clients/add?name={Name}&surname={Suename}" +
                $"&phone={HttpUtility.UrlEncode(Phone)}&money={Money}");
            io.Print(res + Environment.NewLine);
        }
        
        private void AddShare()
        {
            io.Print(Environment.NewLine);

            foreach (var trader in JsonConvert.DeserializeObject<List<Trader>>(Http.Get($"localhost/clients/list")))
            {
                io.Print(trader + Environment.NewLine);
            }

            io.Print(Environment.NewLine);
            io.Print(phraseProvider.GetPhrase(Phrase.Choose));
            string OwnerId = io.Input();

            io.Print(Environment.NewLine);
            io.Print(phraseProvider.GetPhrase(Phrase.EnterName));
            string Name = io.Input();

            io.Print(phraseProvider.GetPhrase(Phrase.EnterPrice));
            string Price = io.Input();

            io.Print(phraseProvider.GetPhrase(Phrase.EnterQuantity));
            string Quantity = io.Input();

            io.Print(Environment.NewLine);
            string res = Http.Post($"localhost/share/add?shareName={Name}&price={Price}" +
                $"&quantity={Quantity}&ownerId={OwnerId}");
            io.Print(res + Environment.NewLine);
        }
        
        private void ChangeShare()
        {
            io.Print(Environment.NewLine);

            foreach (var trader in JsonConvert.DeserializeObject<List<Trader>>(Http.Get($"localhost/clients/list")))
            {
                io.Print(trader + Environment.NewLine);
            }

            io.Print(Environment.NewLine);
            io.Print(phraseProvider.GetPhrase(Phrase.Choose));
            string OwnerId = io.Input();
            io.Print(Environment.NewLine);

            foreach (var share in JsonConvert.DeserializeObject<List<Share>>(Http.Get($"localhost/share/list?ownerId={OwnerId}")))
            {
                io.Print(share + Environment.NewLine);
            }

            io.Print(Environment.NewLine);
            io.Print(phraseProvider.GetPhrase(Phrase.Choose)); string ShareId = io.Input();

            io.Print(Environment.NewLine);
            io.Print(phraseProvider.GetPhrase(Phrase.EnterName));
            string Name = io.Input();

            io.Print(phraseProvider.GetPhrase(Phrase.EnterPrice));
            string Price = io.Input();

            io.Print(Environment.NewLine);
            string res = Http.Post($"localhost/share/update?shareId={ShareId}&newName={Name}" +
                $"&newPrice={Price}&ownerId={OwnerId}");
            io.Print(res + Environment.NewLine);
        }
        
        private void ShowGreenList()
        {
            io.Print(Environment.NewLine);

            foreach (var trader in JsonConvert.DeserializeObject<List<Trader>>(Http.Get($"localhost/clients/greenlist")))
            {
                io.Print(trader + Environment.NewLine);
            }
        }
        
        private void ShowOrangeList()
        {
            io.Print(Environment.NewLine);

            foreach (var trader in JsonConvert.DeserializeObject<List<Trader>>(Http.Get($"localhost/clients/orangelist")))
            {
                io.Print(trader + Environment.NewLine);
            }
        }

        private void ShowBlackList()
        {
            io.Print(Environment.NewLine);

            foreach (var trader in JsonConvert.DeserializeObject<List<Trader>>(Http.Get($"localhost/clients/blacklist")))
            {
                io.Print(trader + Environment.NewLine);
            }
        }
        
    }
}