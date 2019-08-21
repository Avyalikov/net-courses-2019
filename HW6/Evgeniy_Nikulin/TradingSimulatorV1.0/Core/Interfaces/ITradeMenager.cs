namespace Core.Interfaces
{
    using Core.Model;
    using System.Collections.Generic;

    public interface ITradeMenager
    {
        List<Transaktion> Transaktion { get; }
        void Trade();
    }
}