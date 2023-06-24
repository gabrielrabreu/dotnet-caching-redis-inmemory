namespace DDRC.WebApi.Models
{
    public class VideoStoreModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual List<FulfilledSaleModel> FulfilledSales { get; set; }
        public virtual List<ExpectedSaleModel> ExpectedSales { get; set; }
    }
}
