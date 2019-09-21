namespace stockSimulator.Modulation.Repositories
{
    using stockSimulator.Core.Models;
    using stockSimulator.Core.Repositories;
    using System.Linq;

    internal class ClientTableRepository : IClientTableRepository
    {
        private readonly StockSimulatorDbContext dbContext;

        /// <summary>
        /// Initializes an Instance of ClientTableRepository class.
        /// </summary>
        /// <param name="dbContext">Database context.</param>
        public ClientTableRepository(StockSimulatorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Add new client instance to Database.
        /// </summary>
        /// <param name="entity">Client's entity to add.</param>
        public void Add(ClientEntity entity)
        {
            this.dbContext.Clients.Add(entity);
        }

        /// <summary>
        /// Check if such client is already exists in Database by its fiels.
        /// </summary>
        /// <param name="entityToCheck">Client's entity to check.</param>
        /// <returns></returns>
        public bool Contains(ClientEntity entityToCheck)
        {
            return this.dbContext.Clients
               .Any(c => c.Name == entityToCheck.Name
               && c.Surname == entityToCheck.Surname
               && c.PhoneNumber == entityToCheck.PhoneNumber
               && c.AccountBalance == entityToCheck.AccountBalance);
        }

        /// <summary>
        /// Check if such client is already exists in Database by its id.
        /// </summary>
        /// <param name="clientId">Id of client.</param>
        /// <returns></returns>
        public bool ContainsById(int clientId)
        {
            return this.dbContext.Clients
               .Any(c => c.ID == clientId);
        }

        /// <summary>
        /// Returns Client entity by its id.
        /// </summary>
        /// <param name="clientId">Id of client.</param>
        /// <returns></returns>
        public ClientEntity Get(int clientId)
        {
            return this.dbContext.Clients
                .Where(c => c.ID == clientId)
                .FirstOrDefault();
        }

        /// <summary>
        /// Returns client's balance.
        /// </summary>
        /// <param name="clientId">Id of client.</param>
        /// <returns></returns>
        public decimal GetBalance(int clientId)
        {
            return this.dbContext.Clients
                .Where(c => c.ID == clientId)
                .Select(c => c.AccountBalance)
                .FirstOrDefault();
        }

        /// <summary>
        /// Returns all clients.
        /// </summary>
        /// <returns></returns>
        public IQueryable<ClientEntity> GetClients()
        {
            var retListOfClients = this.dbContext.Clients;

            return retListOfClients;
        }

        /// <summary>
        /// Save changes.
        /// </summary>
        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }

        /// <summary>
        /// Updates client's entity.
        /// </summary>
        /// <param name="clientId">Client's ID.</param>
        /// <param name="entityToEdit">Entity with new data.</param>
        public void Update(int clientId, ClientEntity entityToEdit)
        {
            var clientToUpdate = this.dbContext.Clients.First(c => c.ID == clientId);
            clientToUpdate = entityToEdit;
        }

        /// <summary>
        /// Updates client's balance by its id.
        /// </summary>
        /// <param name="clientId">Client's ID</param>
        /// <param name="newBalance">New balance.</param>
        public void UpdateBalance(int clientId, decimal newBalance)
        {
            var clientToUpdate = this.dbContext.Clients.First(c => c.ID == clientId);
            clientToUpdate.AccountBalance = newBalance;
        }
    }
}