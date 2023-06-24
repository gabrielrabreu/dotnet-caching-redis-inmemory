namespace DDRC.WebApi.Models
{
    public class FulfilledSaleModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }

        public virtual VideoStoreModel VideoStore { get; set; }
        public virtual MovieModel Movie { get; set; }
    }
}
