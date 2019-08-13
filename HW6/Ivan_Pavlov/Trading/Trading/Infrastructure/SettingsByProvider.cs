namespace Trading.Infrastructure
{
    using Trading.Interface;
    using Trading.Resource;
    using Trading.View;

    internal static class SettingsByProvider
    {
        public static readonly IPhraseProvider phraseProvider = new JsonPhraseProvider();
        public static readonly IIOProvider iOProvider = new ConsoleIO();
    }
}
