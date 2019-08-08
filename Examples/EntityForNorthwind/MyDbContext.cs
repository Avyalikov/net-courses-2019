namespace EntityForNorthwind
{
    using EntityForNorthwind.Model;
    using System.Data.Entity;

    public class MyDbContext : DbContext
    {
         public MyDbContext()
            : base("name=MyDbContext")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOptional<Category>(t => t.Category)
                .WithMany(t => t.Products).Map(m => m.MapKey("CategoryID")); 

            var product = modelBuilder.Entity<Product>();
            product.HasKey(p => p.ProductId).ToTable("Products");

            var category = modelBuilder.Entity<Category>();
            category.Property(c => c.CategoryName).IsRequired().IsUnicode().HasMaxLength(15);
            category.Property(c => c.Description).IsOptional();          
        }
    }
}