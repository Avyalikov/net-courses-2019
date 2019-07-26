namespace GameWhichCanDraw.Interfaces
{
    /* Associates a key phrase with sentences in the source (File, DB, ...)
     */
    public interface IPhraseProvider
    {
        /* Return text from source by phraseKey
         */
        string GetPhrase(string phraseKey);

        void SetLanguage(string lang);
    }
}
