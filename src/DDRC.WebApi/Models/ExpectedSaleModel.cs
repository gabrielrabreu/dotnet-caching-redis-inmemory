namespace DDRC.WebApi.Models
{
    public class ExpectedSaleModel
    {
        public Guid Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public int Amount { get; set; }

        public virtual VideoStoreModel VideoStore { get; set; }
        public virtual MovieModel Movie { get; set; }
    }
}
