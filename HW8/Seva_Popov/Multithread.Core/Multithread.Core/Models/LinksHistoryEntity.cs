using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Multithread.Core.Models
{
    public class LinksHistoryEntity
    {
        [Key]
        public int Id { get; set; }
        public string Links { get; set; }
        public string PreviousLink { get; set; }
    }
}
