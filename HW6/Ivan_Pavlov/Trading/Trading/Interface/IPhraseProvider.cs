namespace Trading.Interface
{
    public interface IPhraseProvider
    {
        string GetPhrase(string phraseKey);

        void SetLanguage(string lang);
    }
}
