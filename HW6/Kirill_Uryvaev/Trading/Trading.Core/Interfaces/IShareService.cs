using System.Collections.Generic;
using Trading.Core.DataTransferObjects;

namespace Trading.Core
{
    public interface IShareService
    {
        IEnumerable<ShareEntity> GetAllShares();
        int RegisterShare(ShareRegistrationInfo shareInfo);
    }
}