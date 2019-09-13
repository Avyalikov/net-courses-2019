namespace MultithreadApp.DataBase
{
    using DataBase.Repository;
    using Repository.Interface;

    public class DataBaseUW : IDataBase
    {
        private SiteDbContext db;

        public ILinksRepository Links { get; private set; }

        public void SaceChanges() =>
            this.db.SaveChanges();

        public void Connect()
        {
            this.db = new SiteDbContext();
            this.Links = new LinksRepository(db);
        }

        public void Disconnect()
        {
            this.db.Dispose();
            this.Links.Dispose();
        }
    }
}