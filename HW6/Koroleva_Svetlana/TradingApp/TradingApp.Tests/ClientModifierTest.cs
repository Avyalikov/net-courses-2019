using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using TradingApp.IRepositories;

namespace TradingApp.Tests
{
    [TestClass]
    public class ClientModifierTest
    {
        [TestMethod]
        public void ShpouldRegisterNewCloient()
        {
            var clientsTableRep = Substitute.For<IClientsTableRep>();
           
        }
    }
}
