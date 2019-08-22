using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Core.Repositories
{
    public interface IShareRepository: IDBTable
    {
        void Add(ShareEntity share);
        IEnumerable<ShareEntity> LoadAllShares();
        ShareEntity LoadShareByID(int ID);
    }
}
