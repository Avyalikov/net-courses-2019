using EntitySample.Model;
using System.Data.Entity;

namespace EntitySample
{
    public class SampleDbInitializer : DropCreateDatabaseAlways<SampleDbContext>
    {
        protected override void Seed(SampleDbContext context)
        {
            var author1 = new Author { Name = "Mark123", Surname = "Twain" };
            var author2 = new Author { Name = "Jerome", Surname = "Salinger" };
            context.Authors.Add(author1);
            context.Authors.Add(author2);

            var book1 = new Book { Heading = "Tom Sawyer", Author = author1 };
            var book2 = new Book { Heading = "The catcher in the Rye", Author = author2 };
            context.Books.Add(book1);
            context.Books.Add(book2);
            base.Seed(context);
        }
    }
}
