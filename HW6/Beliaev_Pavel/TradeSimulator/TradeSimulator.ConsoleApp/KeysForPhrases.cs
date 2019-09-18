using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeSimulator.ConsoleApp
{
    public enum KeysForPhrases
    {
        GeneralReg,
        GeneralTrade,
        GeneralShow,

        RegName,
        RegSurname,
        RegPhone,
        RegBalance,

        RegStock,
        RegStockType,
        RegStockQuant,
        RegStockPrice,

        Trade,
        TradeBuyer,
        TradeSeller,
        TradeStockType,
        TradeStockQuant,
        TradeDeal,

        ShowClients,
        ShowStocksOfClients,
        ShowStockPrice,
        ShowHistory,

        ExitKey,
        InvalidInput,
        Goodbye,
        CloseProg
    }
}
