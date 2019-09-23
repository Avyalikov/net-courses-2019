namespace TradeSimulator.Core.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TradeSimulator.Core.Models;

    public interface IClientsTableRepository
    {
        void Add(ClientEntity entity);
        void Remove(ClientEntity entity);
        void SaveChanges();

        ClientEntity GetClientByNameAndSurname(string Name, string Surname);
        ClientEntity GetClientById(int clientId);
        ICollection<ClientEntity> GetAllClients();
    }
}
