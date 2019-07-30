namespace DrawGame.Interfaces
{
    internal interface IPhraseProvider
    {
        string GetPhrase(string langPackName, KeysForPhrases phraseKey);
    }
}
