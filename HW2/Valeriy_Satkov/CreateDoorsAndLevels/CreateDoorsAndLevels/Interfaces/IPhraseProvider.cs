namespace CreateDoorsAndLevels.Interfaces
{
    /* Associates a key phrase with sentences in the source (File, DB, ...)
     */
    interface IPhraseProvider
    {
        /* Return text from source by phraseKey
         */
        string GetPhrase(string phraseKey);
        void SetLanguage(string lang);
    }
}
