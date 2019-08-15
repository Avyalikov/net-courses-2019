namespace TradingView.Interface
{
    internal interface IPhraseProvider
    {
        string GetPhrase(string phraseKey);

        void SetLanguage(string lang);
    }
}
