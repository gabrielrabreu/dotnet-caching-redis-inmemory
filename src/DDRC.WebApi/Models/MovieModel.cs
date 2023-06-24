namespace DDRC.WebApi.Models
{
    public class MovieModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual List<CategoryModel> Categories { get; set; }
        public virtual List<ActorModel> Actors { get; set; }
        public virtual List<FulfilledSaleModel> FulfilledSales { get; set; }
        public virtual List<ExpectedSaleModel> ExpectedSales { get; set; }
        public virtual List<StockModel> Stocks { get; set; }
    }
}
