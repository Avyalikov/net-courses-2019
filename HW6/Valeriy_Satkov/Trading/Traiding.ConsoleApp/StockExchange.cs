using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap;
using Traiding.Core.Dto;
using Traiding.Core.Services;

namespace Traiding.ConsoleApp
{
    public class StockExchange
    {
        private readonly Container traidingRegistryContainer;
        private readonly ClientsService clientsService;

        public StockExchange(Container traidingRegistryContainer)
        {
            this.traidingRegistryContainer = traidingRegistryContainer;
            this.clientsService = traidingRegistryContainer.GetInstance<ClientsService>();
        }

        public void Start()
        {
            string inputString;

            do
            {
                Console.Clear();
                /* Time until the next deal: ...[10] // countdown to next deal from 10 to 1, Now
                 * Last deal: [] // last operation from dto.Operations
                 */
                /* Menu
                 * 1. Add a new client
                 * 2. Clients in 'Orange' zone // client with zero balances
                 * 3. Add a new share into system
                 * 4. Add a new share type into system
                 * 5. Change the cost of share type
                 */
                Console.WriteLine("Time until the next deal: ...10");
                Console.WriteLine("Last deal: ");
                Console.WriteLine("Menu");
                Console.WriteLine(" 1. Add a new client");
                Console.WriteLine(String.Empty);
                Console.Write("Type the number or 'e' for exit and press Enter: ");

                inputString = Console.ReadLine();
                switch (inputString)
                {
                    case "1":
                        Console.WriteLine("  Client registration service."); // signal about enter into case
                        AddClient();
                        // ioProvider.ReadLine(); // pause
                        break;
                    default:
                        break;
                }
            } while (inputString != "e");
        }

        public void AddClient()
        {
            string lastName = string.Empty,
                firstName = string.Empty,
                phoneNumber = string.Empty;

            string outputString = string.Empty;
            while (outputString != "e")
            {
                if (string.IsNullOrEmpty(lastName))
                {
                    Console.Write("   Enter the Last name of client: ");
                    outputString = Console.ReadLine();
                    if (outputString.Length < 2 || outputString.Length > 20)
                    {
                        Console.WriteLine("Wrong Line. Try again.");
                        continue;
                    }
                    lastName = outputString;
                }

                if (string.IsNullOrEmpty(firstName))
                {
                    Console.Write("   Enter the First name of client: ");
                    outputString = Console.ReadLine();
                    if (outputString.Length < 2 || outputString.Length > 20)
                    {
                        Console.WriteLine("Wrong Line. Try again.");
                        continue;
                    }
                    firstName = outputString;
                }

                if (string.IsNullOrEmpty(phoneNumber))
                {
                    Console.Write("   Enter the phone number of client: ");
                    outputString = Console.ReadLine();
                    if (outputString.Length < 2 || outputString.Length > 20)
                    {
                        Console.WriteLine("Wrong Line. Try again.");
                        continue;
                    }
                    phoneNumber = outputString;
                }

                break;
            }

            if (outputString == "e")
            {
                return;
            }

            Console.WriteLine("    Wait a few seconds, please.");

            clientsService.RegisterNewClient(new ClientRegistrationInfo()
            {
                LastName = lastName,
                FirstName = firstName,
                PhoneNumber = phoneNumber                
            });

            Console.WriteLine("     New client was added! Press Enter.");
            Console.ReadLine(); // pause
        }
    }
}
