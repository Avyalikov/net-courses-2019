using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteParser.Simulator
{
    class dbInitializer : DropCreateDatabaseAlways<SiteParserDbContext>
    {
        protected override void Seed(SiteParserDbContext context)
        {
        }
    }
    
}
