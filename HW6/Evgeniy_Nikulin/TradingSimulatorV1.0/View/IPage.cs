using System.Collections.Generic;

namespace TradingSimulator
{
    public interface IPage
    {
        string Header { get; }
        string Description { get; }
        List<string> Buttons { get; }
        string GetPage(string res);
    }
}