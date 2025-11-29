using UnifiedOfferSchema;

namespace offer_manager.Models.PaginationService
{
    public class PaginationResponse
    {
        public IEnumerable<UOS> Items { get; set; } = new List<UOS>();
        public int Page { get; set; }
        public int PageSize { get; set; }
        public long TotalItems { get; set; }
        public int TotalPages { get; set; }
        public bool IsLastPage { get; set; }
    }
}
