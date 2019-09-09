namespace MultithreadConsoleApp.UrlsStrategy
{
    interface IChooseUrl
    {
        bool CanExecute(string number);
        string Execute();
    }
}
