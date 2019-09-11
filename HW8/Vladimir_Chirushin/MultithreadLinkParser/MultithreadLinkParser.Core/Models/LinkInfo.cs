namespace MultithreadLinkParser.Core.Models
{
    using System.ComponentModel.DataAnnotations;

    public class LinkInfo
    {
        //public int LinkID { get; set; }

        [Key]
        public string urlString { get; set; }

        public int linkLayer { get; set; }
    }
}