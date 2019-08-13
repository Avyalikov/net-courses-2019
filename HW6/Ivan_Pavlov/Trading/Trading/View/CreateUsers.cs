namespace Trading.View
{
    using Trading.Infrastructure;
    using Trading.Interface;

    static class CreateUser
    {
        private static readonly IPhraseProvider phraseProvider;
        private static readonly IIOProvider iOProvider;

        static CreateUser()
        {
            phraseProvider = SettingsByProvider.phraseProvider;
            iOProvider = SettingsByProvider.iOProvider;         
        }

        public static string EnterSurname(bool Valid = false)
        {
            iOProvider.Clear();
            iOProvider.WriteLine(phraseProvider.GetPhrase("EnterSurname"));
            if (Valid)
                iOProvider.WriteLine(phraseProvider.GetPhrase("InputError"));
            return iOProvider.ReadLine();
        }

        public static string EnterName(bool Valid = false)
        {
            iOProvider.WriteLine(phraseProvider.GetPhrase("EnterName"));
            if (Valid)
                iOProvider.WriteLine(phraseProvider.GetPhrase("InputError"));
            return iOProvider.ReadLine();
        }

        public static string EnterPhone(bool Valid = false)
        {
            iOProvider.WriteLine(phraseProvider.GetPhrase("EnterPhone"));
            if (Valid)
                iOProvider.WriteLine(phraseProvider.GetPhrase("InputError"));
            return iOProvider.ReadLine();
        }

        public static string EnterBalance(bool Valid = false)
        {
            iOProvider.WriteLine(phraseProvider.GetPhrase("EnterBalance"));
            if (Valid)
                iOProvider.WriteLine(phraseProvider.GetPhrase("InputError"));
            return iOProvider.ReadLine();
        }

        public static void UserCreated(string userInfo)
        {
            iOProvider.WriteLine(phraseProvider.GetPhrase("UserСreated"));
            iOProvider.WriteLine(userInfo);
            iOProvider.WriteLine(phraseProvider.GetPhrase("BackToMain"));
            iOProvider.ReadKey();
        }
    }
}
