namespace TradingApp.View.Interface
{
    interface IPhraseProvider
    {
        string GetPhrase(string phraseKey);

        void SetLanguage(string lang);
    }
}
