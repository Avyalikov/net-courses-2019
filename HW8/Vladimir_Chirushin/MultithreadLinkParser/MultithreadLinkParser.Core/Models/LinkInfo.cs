namespace MultithreadLinkParser.Core.Models
{
    using System.ComponentModel.DataAnnotations;

    public class LinkInfo
    {
        [Key]
        [StringLength(300)]
        public string UrlString { get; set; }

        public int LinkLayer { get; set; }
    }
}