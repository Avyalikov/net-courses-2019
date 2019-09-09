using HW8.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW8.Classes
{
    public class LinkProcessorProvider : ILinkProcessorProvider
    {
        private IOutputProvider outputProvider;
        private IStorageProvider storageProvider;

        public LinkProcessorProvider(IOutputProvider outputProvider, IStorageProvider storageProvider)
        {
            this.outputProvider = outputProvider;
            this.storageProvider = storageProvider;
        }

        public string ProcessLink(string link, int recursionLevel, string startingUrl, object storageLock)
        {
            Uri uri = new Uri(startingUrl);
            string completeLink = uri.Scheme + @":\\" + new Uri(startingUrl).Authority + @"/" + link;
            bool isNewValue = false;

            lock (storageLock)
            {
                if (!storageProvider.Contains(completeLink))
                {
                    outputProvider.WriteLine(completeLink + " " + (recursionLevel + 1).ToString());
                    storageProvider.TryAdd(completeLink, recursionLevel + 1);
                    isNewValue = true;
                }
            }

            if(isNewValue)
            {
                return completeLink;
            }
            else
            {
                return null;
            }
        }
    }
}
