namespace ReferenceCollectorApp.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ReferenceCollectorApp.Repositories;
    using ReferenceCollectorApp.Services;
    using NSubstitute;
    using System.Collections.Generic;
    using ReferenceCollectorApp.Models;

    [TestClass]
    public class ReferencesDbServiceTests
    {
        IReferencesDbService referencesDbService;
        IReferenceTable referenceTable;

        [TestInitialize]
        public void Initialize()
        {
            referenceTable = Substitute.For<IReferenceTable>();
            referencesDbService = new ReferencesDbService(referenceTable);
        }

        [TestMethod]
        public void ShouldFilterReferencesIfTheyAreInDb()
        {
            //Arrange
            string reference1 = "test1";
            string reference2 = "test2";
            int iterationId = 0;

            var test = new Dictionary<string, int>()
            {
                {reference1, iterationId },
                {reference2, iterationId }
            };
            referenceTable.ContainsById(reference1).Returns(true);
            referenceTable.ContainsById(reference2).Returns(false);

            //Act
            referencesDbService.WriteRefsToDb(ref test);

            //Assert
            Assert.IsTrue(test.ContainsKey(reference2) && test.Count == 1);
        }

        [TestMethod]
        public void ShouldWriteReferencesToDb()
        {
            //Arrange
            string reference = "test1";
            int iterationId = 0;
            var test = new Dictionary<string, int>()
            {
                {reference, iterationId }
            };
            referenceTable.ContainsById(reference).Returns(false);

            //Act
            referencesDbService.WriteRefsToDb(ref test);

            //Assert
            referenceTable.Received(1).Add(Arg.Is<ReferenceEntity>(s => 
            s.Reference == reference
            && s.iterationId == iterationId));
            referenceTable.Received(1).SaveChanges();
        }

        [TestMethod]
        public void ShouldNotWriteReferencesToDbIfItExists()
        {
            //Arrange
            string reference = "test1";
            int iterationId = 0;
            var test = new Dictionary<string, int>()
            {
                {reference, iterationId }
            };
            referenceTable.ContainsById(reference).Returns(true);

            //Act
            referencesDbService.WriteRefsToDb(ref test);

            //Assert
            referenceTable.Received(0).Add(Arg.Any<ReferenceEntity>());
            referenceTable.Received(0).SaveChanges();
        }
    }
}
