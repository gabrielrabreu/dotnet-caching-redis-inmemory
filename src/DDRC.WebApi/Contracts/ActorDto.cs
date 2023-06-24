namespace DDRC.WebApi.Contracts
{
    public class ActorDto
    {
        public string Name { get; set; }
    }

    public class ActorForViewDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
