namespace MultithreadApp.Core.Model
{
    public class Link
    {   
        public int Id { get; set; }
        public string URL { get; set; }
        public int IterationId { get; set; }

        public virtual Link FatherLink { get; set; }
    }
}
