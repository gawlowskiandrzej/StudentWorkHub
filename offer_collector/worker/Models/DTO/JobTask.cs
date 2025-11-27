namespace worker.Models.DTO
{
    public class JobTask
    {
        public string JobId { get; set; }
        public int SiteTypeId { get; set; }
        public int OfferAmount { get; set; }
        public int ClientType { get; set; }
        public int BatchSize { get; set; }
    }
}
