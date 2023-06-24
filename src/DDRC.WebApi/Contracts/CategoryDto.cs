namespace DDRC.WebApi.Contracts
{
    public class CategoryDto
    {
        public string Name { get; set; }
    }

    public class CategoryForViewDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
