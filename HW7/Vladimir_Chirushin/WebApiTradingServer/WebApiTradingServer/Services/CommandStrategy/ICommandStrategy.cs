﻿namespace WebApiTradingServer.Services.CommandStrategy
{
    public interface ICommandStrategy
    {
        bool CanExecute(Command command);

        void Execute();
    }
}