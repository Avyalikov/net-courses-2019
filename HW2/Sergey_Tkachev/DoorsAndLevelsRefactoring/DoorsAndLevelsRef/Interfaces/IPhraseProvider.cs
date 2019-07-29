namespace DoorsAndLevelsRef
{
    public interface IPhraseProvider
    {
        /// <summary>Get phase by the key.</summary>
        /// <param name="phraseKey">Key for phrase</param>
        /// <returns></returns>
        string GetPhrase(string phraseKey);
    }
}
