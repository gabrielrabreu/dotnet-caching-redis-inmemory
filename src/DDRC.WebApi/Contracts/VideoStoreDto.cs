namespace DDRC.WebApi.Contracts
{
    public class VideoStoreDto
    {
        public string Name { get; set; }
    }

    public class VideoStoreForViewDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
