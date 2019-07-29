namespace ConsoleDraw.Interfaces
{
    public interface IPhraseProvider
    {
        string GetPhrase(string phraseKey);
     
        void SetLanguage(string lang);
    }
}
