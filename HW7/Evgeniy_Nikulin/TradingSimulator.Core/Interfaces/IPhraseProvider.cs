namespace TradingSimulator.Core.Interfaces
{
    public enum Phrase {
        HeaderMain, HeaderHistory, HeaderTraderList, HeaderTraderManaging,
        HeaderGreenList, HeaderOrangeList, HeaderBlackList, HeaderAddTrader, HeaderAddShare, HeaderChandeShare,
        ButtonBack, ButtonRefresh, ButtonMenu,
        DescriptionMain, DescriptionHistory, DescriptionTraderList, DescriptionTraderManaging,
        DescriptionGreenList, DescriptionOrangeList, DescriptionBlackList, DescriptionAddTrader, DescriptionAddShare, DescriptionChandeShare,
        Green, Orange, Black,
        Sold, SharesOf, To,
        Choose, EnterName, EnterSuname, EnterPhone, EnterMoney, EnterPrice, EnterQuantity,
        Success, EmptyName, NameNotLetter, LongName, PhonePlus, PhoneRegion, PhoneIsLetter, MoneyIsNumber, IncorrectID, ShareIsLetter, PriceIsLetter, QuantityIsLetter
    }
    public interface IPhraseProvider
    {
        string GetPhrase(Phrase phrase);
    }
}