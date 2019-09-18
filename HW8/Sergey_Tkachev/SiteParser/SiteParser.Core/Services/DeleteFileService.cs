using SiteParser.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SiteParser.Core.Services
{
    public class DeleteFileService
    {
        private readonly ICleaner cleaner;

        public DeleteFileService(ICleaner cleaner)
        {
            this.cleaner = cleaner;
        }

        public string DeleteFile(string fileName)
        {
            var result = cleaner.DeleteFile(fileName);
            return result;
        }
    }
}
