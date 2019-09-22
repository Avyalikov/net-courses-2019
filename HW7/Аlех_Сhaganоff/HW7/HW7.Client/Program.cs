using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HW7.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IRequestsProvider requests = new ApiRequestsProvider();
            Client client = new Client(requests);
            client.Run();
        }
    }   
}
