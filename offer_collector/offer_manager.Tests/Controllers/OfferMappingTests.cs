using AutoMapper;
using FluentAssertions;
using offer_manager.Models.Offers.dtoObjects;
using offer_manager.Models.Others.AutoMapper;
using UnifiedOfferSchema;

namespace offer_manager.Tests.Controllers;

public class OfferMappingTests
{
    [Fact]
    public void OfferProfile_Configuration_IsValid()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<OfferProfile>());

        config.AssertConfigurationIsValid();
    }
}
