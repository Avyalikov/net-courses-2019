using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeSimulator.Client.Misc
{
    public enum KeysForPhrases
    {
        GeneralReg,
        GeneralTrade,
        GeneralAutoTrade,
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
        ShowClientsLength,
        ShowClientsPage,

        ShowStocksOfClients,
        ShowStocksOfClientsFilter,

        ShowBalance,
        ShowBalanceId,

        ShowHistory,
        ShowHistoryId,
        ShowHistoryQuant,

        ExitKey,
        InvalidInput,
        Goodbye,
        CloseProg
    }
}
