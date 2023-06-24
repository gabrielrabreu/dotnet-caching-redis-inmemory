using Serilog;

namespace DDRC.WebApi.Scope.Extensions
{
    public static class SerilogLoggingBuilderExtensions
    {
        public static void AddDDRCSerilog(this ILoggingBuilder logging)
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .CreateLogger();

            logging.ClearProviders();
            logging.AddSerilog(logger);
        }
    }
}
