namespace Core.Model
{
    public class Transaktion
    {
        public PersonalCard seller;
        public PersonalCard buyer;
        public Share share;

        public Transaktion(PersonalCard seller, PersonalCard buyer, Share share, int quantity)
        {
            this.seller = new PersonalCard
            {
                ID = seller.ID,
                Name = seller.Name,
                Surname = seller.Surname
            };

            this.buyer = new PersonalCard
            {
                ID = buyer.ID,
                Name = buyer.Name,
                Surname = buyer.Surname
            };

            this.share = new Share
            {
                Name = share.Name,
                Price = share.Price,
                Quantity = quantity
            };
        }
    }
}