namespace TradingSimulator.View
{
    using Core.Interfaces;
    using System.Collections.Generic;
    using Core.Dto;
    using TradingSimulator.Core;
    using System.Linq;

    public class Terminal
    {
        private readonly Point start;
        private readonly int whidth;
        private readonly int height;

        private readonly IInputOutput io;
        private readonly IPhraseProvider phraseProvider;

        public Terminal(Point start, int whidth, int height, IInputOutput io, IPhraseProvider phraseProvider)
        {
            this.start = start;
            this.whidth = whidth;
            this.height = height;

            this.io = io;
            this.phraseProvider = phraseProvider;
            Init();
        }

        private void Init()
        {
            var temp = this.io.CursorPosition;

            for (int i = start.y; i < height; i++)
            {
                this.io.CursorPosition = (start.x - 1, i);
                io.Print(">");
                this.io.CursorPosition = (start.x + whidth, i);
                io.Print("<");
            }

            this.io.CursorPosition = temp;
        }

        private void ClearTerminal() => io.Clear(start, (start.x + whidth, start.y + height));

        public void PrintTerminal(IEnumerable<Transaction> transactions)
        {
            var temp = this.io.CursorPosition;

            ClearTerminal();

            int y = start.y;
            foreach (var t in transactions.TakeLast(5))
            {
                this.io.CursorPosition = (start.x, y);
                io.Print($" {t.seller.name} {t.seller.surname}");
                this.io.CursorPosition = (start.x + 20, y);
                io.Print($"{phraseProvider.GetPhrase(Phrase.Sold)} {t.sellerShare.quantity}");
                this.io.CursorPosition = (start.x + 20 + 9, y);
                io.Print($"{phraseProvider.GetPhrase(Phrase.SharesOf)} {t.sellerShare.name}");
                this.io.CursorPosition = (start.x + 20 + 9 + 30, y);
                io.Print($" {phraseProvider.GetPhrase(Phrase.To)} {t.buyer.name} {t.buyer.surname}");

                y++;
            }

            this.io.CursorPosition = temp;
        }
    }
}