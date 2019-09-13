namespace MultithreadApp.Components
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Interfaces;

    public class FileProvider : IFileProvider
    {
        private readonly string path = Environment.CurrentDirectory + @"\Pages\";

        public FileProvider()
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public async Task SaveToFileAsync(string page, string value)
        {
            using (FileStream fs = File.Create(path + page + ".html"))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                await sw.WriteAsync(value);
            }
        }

        public string LoadHtml(string page)
        {
            using (FileStream fs = File.OpenRead(path + page + ".html"))
            using (StreamReader sr = new StreamReader(fs))
            {
                return sr.ReadToEnd();
            }
        }
    }
}