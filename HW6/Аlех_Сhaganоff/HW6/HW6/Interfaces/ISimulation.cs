using HW6.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW6.Interfaces
{
    public interface ISimulation
    {
        (int sellerId, int buyerId, int shareId, decimal sharePrice, int purchaseQuantity) PerformRandomOperation(IDataInteraction dataProvider, IOutputProvider outputProvider, ILogger logger);

        void UpdateDatabase(IDataInteraction dataProvider, IOutputProvider outputProvider, ILogger logger, int sellerId, int buyerId, int shareId, decimal sharePrice, int purchaseQuantity);
    }

}
