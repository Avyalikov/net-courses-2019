namespace ConsoleCanvas.Interfaces
{
    public interface IPhraseProvider
    {
        string GetPhrase(Phrase requestedPhrase);

        void Initialize();
    }
}