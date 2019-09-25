namespace ReferenceCollector.Core.Repositories
{
    using System.Text;
    public interface IFileSystemRepository
    {
        void Delete(string filePath);
        bool DirectoryExists(string folderPath);
        void CreateDirectory(string folderPath);
        void WriteAllText(string filePath, string dataToWrite, UTF8Encoding uTF8Encoding);
        bool FileExists(string filePath);
    }
}