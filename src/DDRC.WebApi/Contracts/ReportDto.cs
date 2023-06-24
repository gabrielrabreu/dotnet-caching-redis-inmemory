namespace DDRC.WebApi.Contracts
{
    public class VideoStoreReportsDto
    {
        public virtual List<VideoStoreReportDto> VideoStores { get; set; } = new List<VideoStoreReportDto>();
    }

    public class VideoStoreReportDto
    {
        public string VideoStore { get; set; }
        public virtual List<MovieSalesReportDto> Movies { get; set; } = new List<MovieSalesReportDto>();
    }

    public class MovieSalesReportDto
    {
        public string Movie { get; set; }
        public virtual List<DayMovieSalesReportDto> Days { get; set; } = new List<DayMovieSalesReportDto>();
        public virtual List<DayMovieSalesReportDto> RetroactiveDays { get; set; } = new List<DayMovieSalesReportDto>();
    }

    public class DayMovieSalesReportDto
    {
        public DateTimeOffset Date { get; set; }
        public int Stock { get; set; }
        public int SalesOnAllVideoStores { get; set; }
        public int SalesOnCurrentVideoStore { get; set; }
    }
}
