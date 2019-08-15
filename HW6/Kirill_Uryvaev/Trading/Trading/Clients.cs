namespace Trading
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Clients
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Clients()
        {
            ClientsShares = new HashSet<ClientsShares>();
        }

        [Key]
        public int ClientID { get; set; }

        [StringLength(20)]
        public string ClientFirstName { get; set; }

        [StringLength(20)]
        public string ClientLastName { get; set; }

        [StringLength(10)]
        public string PhoneNumber { get; set; }

        [Column(TypeName = "money")]
        public decimal? ClientBalance { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientsShares> ClientsShares { get; set; }
    }
}
