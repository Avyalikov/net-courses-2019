namespace ODataWebApi
{
    using ODataWebApi.Models;
    using System.Collections.Generic;
    using System.Data.Entity;

    public class AppDb : DbContext
    {
        static AppDb()
        {
            Database.SetInitializer<AppDb>(new ContextInitializer());
        }

        public AppDb() : base("DbConnection") { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                 .Entity<User>()
                 .HasKey(p => p.Id)
                 .ToTable("Users");
        }
    }

    class ContextInitializer : DropCreateDatabaseIfModelChanges<AppDb>
    {
        protected override void Seed(AppDb db)
        {
            db.Users.AddRange(new List<User>() {
                new User() { Name = "Vasya", SurName = "Pupkin", Age = 20 },
                new User() { Name = "Tolya", SurName = "Elkin", Age = 40 }});

            db.SaveChanges();
        }

    }
}