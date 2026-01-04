namespace offer_manager.Models.Offers.DtoObjects
{
    public class ScrappedOffersResponseDto
    {
        public DatabaseOffersResponseDto DatabaseOffersResponse { get; set; } = new DatabaseOffersResponseDto();
        public ScrappingStatus ScrappingStatus { get; set; } = new ScrappingStatus();
    }
}
