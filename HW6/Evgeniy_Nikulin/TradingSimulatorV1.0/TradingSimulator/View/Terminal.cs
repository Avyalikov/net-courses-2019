namespace TradingSimulator
{
    using Core.Interfaces;
    using Core.Model;
    using System.Collections.Generic;

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

        public void PrintTerminal(IEnumerable<Transaktion> transaktions)
        {
            var temp = this.io.CursorPosition;

            ClearTerminal();

            int y = start.y;
            foreach (var t in transaktions.TakeLast(5))
            {
                this.io.CursorPosition = (start.x, y);
                io.Print($" {t.seller.Name} {t.seller.Surname}");
                this.io.CursorPosition = (start.x + 20, y);
                io.Print($"{phraseProvider.GetPhrase(Phrase.Sold)} {t.share.Quantity}");
                this.io.CursorPosition = (start.x + 20 + 8, y);
                io.Print($"{phraseProvider.GetPhrase(Phrase.SharesOf)} {t.share.Name}");
                this.io.CursorPosition = (start.x + 20 + 8 + 30, y);
                io.Print($" {phraseProvider.GetPhrase(Phrase.To)} {t.buyer.Name} {t.buyer.Surname}");

                y++;
            }

            this.io.CursorPosition = temp;
        }
    }
}