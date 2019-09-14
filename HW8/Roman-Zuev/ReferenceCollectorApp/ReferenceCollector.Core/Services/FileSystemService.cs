namespace ReferenceCollector.Core.Services
{
    using ReferenceCollector.Core.Repositories;
    using System;
    using System.Text;
    public class FileSystemService : IFileSystemService
    {

        private readonly object fileLock = new object();
        private readonly IFileSystemRepository fileSystemRepository;

        public FileSystemService(IFileSystemRepository fileSystemRepository)
        {
            this.fileSystemRepository = fileSystemRepository;
        }

        public void DeleteFile(string filePath)
        {
            if (!fileSystemRepository.FileExists(filePath))
            {
                throw new ArgumentException($"File doesn't exist. Trying to find it here: {filePath}");
            }

            fileSystemRepository.Delete(filePath);
        }

        public string WriteDataToFile(string dataToWrite, string folderPath)
        {
            if (dataToWrite == null)
            {
                return null;
            }

            var random = new Random();
            string filePath = folderPath;
            if (!fileSystemRepository.DirectoryExists(folderPath))
                fileSystemRepository.CreateDirectory(folderPath);

            lock (fileLock)
            {
                do
                {
                    filePath += random.Next(1000);
                } while (fileSystemRepository.FileExists(filePath));

                fileSystemRepository.WriteAllText(filePath, dataToWrite, new UTF8Encoding());
            }
            return filePath;
        }
    }
}
