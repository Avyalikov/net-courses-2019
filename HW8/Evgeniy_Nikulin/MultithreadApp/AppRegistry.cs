namespace MultithreadApp
{
    using MultithreadApp.Components;
    using MultithreadApp.DataBase;
    using MultithreadApp.Interfaces;
    using StructureMap;

    class AppRegistry : Registry
    {
        public AppRegistry()
        {
            this.For<IApplication>().Use<Application>();
            this.For<ILinksServices>().Use<LinksServices>();
            this.For<IDataBase>().Use<DataBaseUW>();
            this.For<IHttpProvider>().Use<WikiHttpProvider>();
            this.For<IFileProvider>().Use<FileProvider>();
        }
    }
}
