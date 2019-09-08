namespace MultithreadLinkParser
{
    using MultithreadLinkParser.Models;
    using System.Data.Entity;

    public class LinksParserContext : DbContext
        {
            public DbSet<LinkInfo> Links { get; set; }
        }
    }
