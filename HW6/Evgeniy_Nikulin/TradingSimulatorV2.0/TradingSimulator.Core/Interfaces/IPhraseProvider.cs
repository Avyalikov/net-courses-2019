namespace TradingSimulator.Core.Interfaces
{
    public enum Phrase {
        HeaderMain, HeaderHistory, HeaderTraderList, HeaderTraderManaging,
        HeaderWhiteList, HeaderOrangeList, HeaderBlackList, HeaderAddTrader, HeaderAddShare, HeaderChandeShare,
        ButtonBack, ButtonRefresh, ButtonMenu,
        DescriptionMain, DescriptionHistory, DescriptionTraderList, DescriptionTraderManaging,
        DescriptionWhiteList, DescriptionOrangeList, DescriptionBlackList, DescriptionAddTrader, DescriptionAddShare, DescriptionChandeShare,
        White, Orange, Black,
        Sold, SharesOf, To,
        Choose, EnterName, EnterSuname, EnterPhone, EnterMoney, EnterPrice, EnterQuantity,
        Success, EmptyName, NameNotLetter, LongName, PhonePlus, PhoneRegion, PhoneIsLetter, MoneyIsNumber, IncorrectID, ShareIsLetter, PriceIsLetter, QuantityIsLetter
    }
    public interface IPhraseProvider
    {
        string GetPhrase(Phrase phrase);
    }
}