namespace TradingApp.View.View
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using TradingApp.Core.Models;
    using TradingApp.View.Interface;

    class ShareView
    {
        private readonly IPhraseProvider phraseProvider;
        private readonly IIOProvider iOProvider;

        public ShareView(IPhraseProvider phraseProvider, IIOProvider iOProvider)
        {
            this.phraseProvider = phraseProvider;
            this.iOProvider = iOProvider;
        }

        public void PrintAllShares(ICollection<ShareEntity> shares)
        {
            iOProvider.Clear();
            StringBuilder result = new StringBuilder();
            foreach (var share in shares)
            {
                result.AppendLine($"{share.Id}. {share.Name} от компании {share.CompanyName} по цене {share.Price}");
            }
            iOProvider.WriteLine(result.ToString());
        }

        public int ShareId()
        {
            return ChooseId();
        }

        public decimal ShareNewPrice()
        {
            return NewPrice();
        }

        private int ChooseId(bool valid = false)
        {
            iOProvider.WriteLine(phraseProvider.GetPhrase("EnterStockId"));
            if (valid)
                iOProvider.WriteLine(phraseProvider.GetPhrase("InputError"));

            if (int.TryParse(iOProvider.ReadLine(), out int id))
                return id;
            return ChooseId(true);
        }

        private decimal NewPrice(bool valid = false)
        {
            iOProvider.WriteLine(phraseProvider.GetPhrase("EnterNewPrice"));
            if (valid)
                iOProvider.WriteLine(phraseProvider.GetPhrase("InputError"));

            if (decimal.TryParse(iOProvider.ReadLine(), out decimal newPrice))
                return newPrice;
            return NewPrice(true);
        }
    }
}
