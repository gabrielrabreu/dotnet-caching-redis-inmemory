namespace DDRC.WebApi.Contracts
{
    public class ExpectedSaleDto
    {
        public DateTimeOffset Date { get; set; }
        public int Amount { get; set; }
        public string VideoStore { get; set; }
        public string Movie { get; set; }
    }
}
