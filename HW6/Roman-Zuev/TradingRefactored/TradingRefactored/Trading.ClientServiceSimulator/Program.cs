using System;
using StructureMap;
using Trading.ClientServiceSimulator.Dependecies;
using Trading.Core.Services;

namespace Trading.ClientServiceSimulator
{
    class Program
    {
        static void Main()
        {
            var container = new Container(new TradesClientServiceSimulator());
            var clientService = container.GetInstance<ClientsService>();

            clientService.RegisterNewClient(new Core.Dto.ClientRegistrationInfo()
            {
                Name = "Roman Zuev",
                Phone = "092929291"
            });

            var clientsInBlackZone = clientService.GetClientsInBlackZone();
            foreach (var item in clientsInBlackZone)
            {
                Console.WriteLine($"Client {item.Name} is in the Black zone with balance: {item.Balance}");
            }
            var clientsInOrangeZone = clientService.GetClientsInOrangeZone();
            foreach (var item in clientsInOrangeZone)
            {
                Console.WriteLine($"Client {item.Name} is in the Orange zone with balance: {item.Balance}");
            }
            clientService.PutMoneyToBalance(new Core.Dto.ArgumentsForPutMoneyToBalance()
            {
                AmountToPut = 100000,
                ClientId = 9
            });
            Console.ReadKey();
        }
    }
}
