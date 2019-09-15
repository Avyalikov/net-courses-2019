namespace ReferenceCollector.Core.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ReferenceCollector.Core.Services;
    using ReferenceCollector.Core.Repositories;
    using NSubstitute;
    using System.Text;

    [TestClass]
    public class FileSystemServiceTests
    {
        IFileSystemRepository fileSystemRepository;
        IFileSystemService fileSystemService;

        [TestInitialize]
        public void Initialize()
        {
            this.fileSystemRepository = Substitute.For<IFileSystemRepository>();
            this.fileSystemService = new FileSystemService(fileSystemRepository);
        }
        [TestMethod]
        public void ShouldDeleteFile()
        {
            //Arrange
            string filePath = "test";
            fileSystemRepository.FileExists(filePath).Returns(true);

            //Act
            fileSystemService.DeleteFile(filePath);

            //Assert
            fileSystemRepository.Received(1).Delete(Arg.Is(filePath));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "File doesn't exist. Trying to find it here: test")]
        public void ShouldNotDeleteFileIfDoesntExist()
        {
            //Arrange
            string filePath = "test";
            fileSystemRepository.FileExists(filePath).Returns(false);

            //Act
            fileSystemService.DeleteFile(filePath);
        }

        [TestMethod]
        public void ShouldWriteFile()
        {
            //Arrange
            string folderPath= "testFolder";
            string data = "testData";
            fileSystemRepository.DirectoryExists(folderPath).Returns(true);

            //Act
            fileSystemService.WriteDataToFile(data, folderPath);

            //Assert
            fileSystemRepository.Received(1).WriteAllText(Arg.Any<string>(), data, new UTF8Encoding());
        }
    }
}
