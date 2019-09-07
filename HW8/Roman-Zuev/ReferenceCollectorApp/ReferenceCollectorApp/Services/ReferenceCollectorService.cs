using ReferenceCollectorApp.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ReferenceCollectorApp.Services
{
    public class ReferenceCollectorService
    {
        private readonly IReferenceTable referenceTable;

        public ReferenceCollectorService(IReferenceTable referenceTable)
        {
            this.referenceTable = referenceTable;
        }
        public async Task DownloadPage (string uri, string filePath)
        {
            var httpClient = new HttpClient();
            try
            {
                string responseBody = await httpClient.GetStringAsync(uri);
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);
                File.WriteAllText(filePath+"page.txt", responseBody, new UTF8Encoding());
            }
            catch (HttpRequestException e)
            {
                throw e;
            }
        }

        public async Task GetRefsDictionary(string filePath, int iterationId)
        {
            string dataToParse = File.ReadAllText(filePath);
        }

        public void WriteToDb (IDictionary data)
        {
            referenceTable.AddBatch(data);
        }
    }


}
