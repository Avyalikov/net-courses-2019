namespace Trading.Core
{
    using System;
    using System.Collections.Generic;

    public partial class ShareEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ShareEntity()
        {
            ClientsShares = new HashSet<ClientsSharesEntity>();
        }

        public int ShareID { get; set; }

        public string ShareName { get; set; }

        public decimal? ShareCost { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientsSharesEntity> ClientsShares { get; set; }
    }
}
