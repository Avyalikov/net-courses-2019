namespace ReferenceCollector.Core.Services
{
    public interface IFileSystemService
    {
        string WriteDataToFile(string dataToWrite, string folderPath);
        void DeleteFile(string fileName);
    }
}
