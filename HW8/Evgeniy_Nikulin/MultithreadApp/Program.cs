namespace MultithreadApp
{
    using StructureMap;

    public class Program
    {
        public static void Main(string[] args)
        {
            var conteiner = new Container(new AppRegistry());
            IApplication app = conteiner.GetInstance<IApplication>();

            app.Run();
        }
    }
}