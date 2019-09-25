namespace SiteParser.Core.Repositories
{
    using SiteParser.Core.Models;

    public interface ISaver
    {
        /// <summary>
        /// Checks if url already conains in DataBase.
        /// </summary>
        /// <param name="urlToCheck">Url to check.</param>
        /// <returns></returns>
        bool Contains(string urlToCheck);

        /// <summary>
        /// Add entry to Database.
        /// </summary>
        /// <param name="entityToAdd"> Entity to add.</param>
        /// <returns></returns>
        string Save(LinkEntity entityToAdd);

        /// <summary>
        /// Save changes.
        /// </summary>
        void SaveChanges();
    }
}
