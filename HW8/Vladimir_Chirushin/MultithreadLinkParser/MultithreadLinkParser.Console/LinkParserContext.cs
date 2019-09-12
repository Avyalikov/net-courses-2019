namespace MultithreadLinkParser.Console
{
    using System.Data.Entity;
    using MultithreadLinkParser.Core.Models;

    public class LinksParserContext : DbContext
    {
        public DbSet<LinkInfo> Links { get; set; }
    }
}
