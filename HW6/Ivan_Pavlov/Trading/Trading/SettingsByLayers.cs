namespace Trading
{
    using TradingData;
    using TradingView;
    using TradingView.Interface;

    internal static class SettingsByLayers
    {
        internal static readonly IView viewProvider = new ViewProvider();
        internal static readonly IDbProvider dbProvider = new DbProvider();
    }
}
