using Offer_collector.Models.UrlBuilders;

namespace worker.Models.DTO
{
    public class JobTask
    {
        public string JobId { get; set; }
        public SearchFilters SearchFilters { get; set; }
        public int SiteTypeId { get; set; }
        public int BatchSize { get; set; }
        public int BatchLimit { get; set; }
        public int Offset { get; set; }

        }
}
