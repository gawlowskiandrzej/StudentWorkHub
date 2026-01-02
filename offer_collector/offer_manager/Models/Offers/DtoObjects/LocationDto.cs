using System.Text.Json.Serialization;

namespace offer_manager.Models.Offers.dtoObjects
{
    public class LocationDto
    {
        public string? City { get; set; }
        public bool? IsRemote { get; set; }
        public string? PostalCode { get; set; }
        public string? Street { get; set; }
        public string? BuildingNumber { get; set; }
        public bool? IsHybrid { get; set; }
    }
}