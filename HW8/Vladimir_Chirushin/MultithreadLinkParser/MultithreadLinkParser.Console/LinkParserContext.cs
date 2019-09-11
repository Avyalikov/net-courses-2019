namespace MultithreadLinkParser.Console
{
    using MultithreadLinkParser.Core.Models;
    using System.Data.Entity;

    public class LinksParserContext : DbContext
    {
        public DbSet<LinkInfo> Links { get; set; }
    }
}
