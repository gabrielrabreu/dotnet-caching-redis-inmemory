namespace DDRC.WebApi.Contracts
{
    public class MovieDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Actors { get; set; }
        public List<string> Categories { get; set; }
    }

    public class MovieForViewDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Actors { get; set; }
        public List<string> Categories { get; set; }
    }
}
