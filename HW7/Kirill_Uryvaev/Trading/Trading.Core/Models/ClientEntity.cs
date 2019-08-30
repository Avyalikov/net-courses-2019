namespace Trading.Core
{
    using System;
    using System.Collections.Generic;

    public class ClientEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClientEntity()
        {
            ClientsShares = new HashSet<ClientsSharesEntity>();
        }

        public int ClientID { get; set; }

        public string ClientFirstName { get; set; }

        public string ClientLastName { get; set; }

        public string PhoneNumber { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientsSharesEntity> ClientsShares { get; set; }

        public virtual BalanceEntity ClientBalance {get; set;}
}
}
