namespace DDRC.WebApi.Models
{
    public class ActorModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual List<MovieModel> Movies { get; set; }
    }
}
