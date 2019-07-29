namespace ConsoleDraw.Interfaces
{
    public interface IPhraseProvider
    {
        string GetPhrase(string phraseKey);

        string GetPhraseAndReplace(string phraseKey, string rewriteStr, string rightStr);

        void SetLanguage(string lang);
    }
}
