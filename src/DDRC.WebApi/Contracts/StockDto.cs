namespace DDRC.WebApi.Contracts
{
    public class StockDto
    {
        public DateTimeOffset Date { get; set; }
        public int Amount { get; set; }
        public string Movie { get; set; }
    }
}
