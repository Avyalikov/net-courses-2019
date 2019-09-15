using Microsoft.VisualStudio.TestTools.UnitTesting;
using Multithread.Core.Models;
using Multithread.Core.Repositories;
using Multithread.Core.Services;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multithread.Core.Test
{
    [TestClass]
    public class LinksHistoryServicesTest
    {
        [TestMethod]
        public void ShouldRegisterNewLinksHistory()
        {
            var linksHistoryRepositoroes = Substitute.For<ILinksHistoryRepositoroes>();
            LinksHistoryServices linksHistoryServices = new LinksHistoryServices(linksHistoryRepositoroes);
            LinksHistoryEntity args = new LinksHistoryEntity();
            args.Links = "https://www.google.ru/webhp?source=search_app";
            args.PreviousLink = "https://www.google.ru";

            linksHistoryServices.RegisterNewLinks(args);

            linksHistoryRepositoroes.Received(1).Add(Arg.Is<LinksHistoryEntity>(w => w.Links == args.Links && w.PreviousLink == args.PreviousLink));
            linksHistoryRepositoroes.Received(1).SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "This links has been registered. Can't continue")]
        public void ShouldCannotRegisterDuplicateLinks()
        {
            var linksHistoryRepositoroes = Substitute.For<ILinksHistoryRepositoroes>();
            LinksHistoryServices linksHistoryServices = new LinksHistoryServices(linksHistoryRepositoroes);
            LinksHistoryEntity args = new LinksHistoryEntity();
            args.Links = "https://www.google.ru/webhp?source=search_app";
            args.PreviousLink = "https://www.google.ru";

            linksHistoryServices.RegisterNewLinks(args);

            linksHistoryRepositoroes.Contains(Arg.Is<LinksHistoryEntity>(w => w.Links == args.Links && w.PreviousLink == args.PreviousLink)).Returns(true);
            linksHistoryServices.RegisterNewLinks(args);
        }

        [TestMethod]
        public void ShouldRegisterdNewLinksHistory()
        {
            var linksHistoryRepositoroes = Substitute.For<ILinksHistoryRepositoroes>();
            LinksHistoryServices linksHistoryServices = new LinksHistoryServices(linksHistoryRepositoroes);
            LinksHistoryEntity args = new LinksHistoryEntity();
            args.Links = "https://www.google.ru/webhp?source=search_app";
            args.PreviousLink = "https://www.google.ru";

            linksHistoryServices.ContainsLinks(args);

            linksHistoryRepositoroes.Contains(Arg.Is<LinksHistoryEntity>(w => w.Links == args.Links && w.PreviousLink == args.PreviousLink)).Returns(true);
            linksHistoryServices.ContainsLinks(args);
        }
    }
}
