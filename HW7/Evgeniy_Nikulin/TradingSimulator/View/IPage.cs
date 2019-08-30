namespace TradingSimulator.View
{
    using System.Collections.Generic;

    public interface IPage
    {
        string Header { get; }
        string Description { get; }
        List<string> Buttons { get; }
        string GetPage(string res);
    }
}