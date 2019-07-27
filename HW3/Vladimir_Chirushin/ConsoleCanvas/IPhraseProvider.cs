namespace ConsoleCanvas
{
    public interface IPhraseProvider
    {
        string GetPhrase(Phrase requestedPhrase);
        void InitiatePhrases();
    }
}