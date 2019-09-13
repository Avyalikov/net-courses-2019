namespace MultithreadConsoleApp.UrlsStrategy
{
    public class MummyFilmUrl : IChooseUrl
    {
        public bool CanExecute(string number)
        {
            return number == "1";
        }

        public string Execute()
        {
            return "https://en.wikipedia.org/wiki/The_Mummy_(1999_film)";
        }
    }

    public class TerminalFilmUrl : IChooseUrl
    {
        public bool CanExecute(string number)
        {
            return number == "2";
        }

        public string Execute()
        {
            return "https://en.wikipedia.org/wiki/The_Terminal";
        }
    }

    public class InterstateFilmUrl : IChooseUrl
    {
        public bool CanExecute(string number)
        {
            return number == "3";
        }

        public string Execute()
        {
            return "https://en.wikipedia.org/wiki/Interstate_60";
        }
    }

    public class AvatarFilmUrl : IChooseUrl
    {
        public bool CanExecute(string number)
        {
            return number == "4";
        }

        public string Execute()
        {
            return "https://en.wikipedia.org/wiki/Avatar_(2009_film)";
        }
    }

    public class ShawshankFilmUrl : IChooseUrl
    {
        public bool CanExecute(string number)
        {
            return number == "5";
        }

        public string Execute()
        {
            return "https://en.wikipedia.org/wiki/The_Shawshank_Redemption";
        }
    }
}
