namespace ReferenceCollectorApp.Repositories
{
    using ReferenceCollector.Core.Repositories;
    using System.IO;
    using System.Text;
    
    public class FileSystemRepository : IFileSystemRepository
    {
        public void CreateDirectory(string folderPath)
        {
            Directory.CreateDirectory(folderPath);
        }

        public void Delete(string filePath)
        {
            File.Delete(filePath);
        }

        public bool DirectoryExists(string folderPath)
        {
            return Directory.Exists(folderPath);
        }

        public bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        public void WriteAllText(string filePath, string dataToWrite, UTF8Encoding uTF8Encoding)
        {
            File.WriteAllText(filePath, dataToWrite, uTF8Encoding);
        }
    }
}
