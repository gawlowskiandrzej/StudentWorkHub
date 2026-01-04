using offer_manager.Models.Offers.dtoObjects;
using offer_manager.Models.PaginationService;
using shared_models;

namespace offer_manager.Models.Offers.DtoObjects
{
    public class DatabaseOffersResponseDto
    {
        public PaginationResponse<OfferDTO> Pagination { get; set; } = new PaginationResponse<OfferDTO>();
        public DynamicFilter DynamicFilter { get; set; } = new DynamicFilter();
        public IEnumerable<string>? Error { get; set; } = null;
    }
}
