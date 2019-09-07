namespace Multithread.Core.Services.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Multithread.Core.Repo;
    using NSubstitute;
    using System.IO;

    [TestClass()]
    public class LinksServicesTests
    {
        [TestMethod()]
        public void ShouldDowloandPageTest()
        {
            var repo = Substitute.For<ILinksRepo>();
            LinksServices linksServices = new LinksServices(repo);          
            var testUrl = "Resource\\Mummia.html";
            var testFile = File.ReadAllText(testUrl);   

             linksServices.DowloandPage(testUrl);


        }
    }
}