namespace Trading
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Shares
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Shares()
        {
            ClientsShares = new HashSet<ClientsShares>();
        }

        [Key]
        public int ShareID { get; set; }

        [StringLength(10)]
        public string ShareName { get; set; }

        [Column(TypeName = "money")]
        public decimal? ShareCost { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientsShares> ClientsShares { get; set; }
    }
}
