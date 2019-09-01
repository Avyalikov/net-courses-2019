using System;
using System.Collections.Generic;
using System.Text;

namespace WikipediaParser.Models
{
    public class LinkEntity
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public int IterationId { get; set; }
    }
}
