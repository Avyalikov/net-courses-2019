namespace TradingSimulator.Interfaces
{
    public enum Phrase { Welcome }
    public interface IPhraseProvider
    {
        string GetPhrase(Phrase phrase);
    }
}