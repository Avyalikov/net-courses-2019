namespace NumbersGame.Interfaces
{
    public interface IPhraseProvider
    {
        string GetPhrase(string phraseKey, Language ChosenLang);
    }
}
