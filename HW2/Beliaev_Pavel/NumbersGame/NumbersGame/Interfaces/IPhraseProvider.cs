namespace NumbersGame.Interfaces
{
    public interface IPhraseProvider
    {
        string GetPhrase(KeysForPhrases keyPhrase, string langPackName);
    }
}
