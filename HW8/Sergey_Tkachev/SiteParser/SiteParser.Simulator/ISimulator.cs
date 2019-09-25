namespace SiteParser.Simulator
{
    public interface ISimulator
    {
        /// <summary>
        /// Starts simulation.
        /// </summary>
        /// <param name="startPageToParse">Initial Url adress.</param>
        /// <param name="baseUrl">Base Url adress.</param>
        void Start(string startPageToParse, string baseUrl);
    }
}
