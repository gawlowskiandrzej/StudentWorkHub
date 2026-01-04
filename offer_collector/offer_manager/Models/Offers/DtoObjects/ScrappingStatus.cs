using worker.Models.DTO;

namespace offer_manager.Models.Offers.DtoObjects
{
    public class ScrappingStatus
    {
        public List<JobInfoDto> jobInfos { get; set; } = new List<JobInfoDto>();
        public bool ScrappingDone { get; set; } = false;
        public string? ScrappingDuration { get; set; } = null;
    }
}
