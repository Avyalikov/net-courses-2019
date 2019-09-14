namespace ReferenceCollectorApp
{
    using StructureMap;
    using ReferenceCollectorApp.Dependencies;
    class Program
    {
        static void Main()
        {
            var container = new Container(new ReferenceCollectorRegistry());
            var program = container.GetInstance<IWikiReferenceCollector>();
            program.Run();
        }
    }
}
