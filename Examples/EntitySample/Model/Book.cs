using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitySample.Model
{
    public class Book
    {
        public int BookId { get; set; }

        public string Heading { get; set; }

        public virtual Author Author { get; set; }
    }
}
