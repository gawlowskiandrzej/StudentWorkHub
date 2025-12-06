using Newtonsoft.Json;

namespace Offer_collector.Models.OlxPraca
{
    public class OlxPracaCategory
    {
        public int id { get; set; }
        public string? label { get; set; }
        public int parentId { get; set; }
        public string? name { get; set; }
        public string? normalizedName { get; set; }
        public int position { get; set; }
        public string? viewType { get; set; }
        public string? iconName { get; set; }
        public int level { get; set; }
        public int displayOrder { get; set; }
        public List<int>? children { get; set; }
        public string? path { get; set; }
        public string? type { get; set; }
        public bool isAdding { get; set; }
        public bool isSearch { get; set; }
        public bool isOfferSeek { get; set; }
        public bool privateBusiness { get; set; }
        public int photosMax { get; set; }
        public string? img { get; set; }
    }
}
