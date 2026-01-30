using FluentAssertions;
using OffersConnector;
using Xunit;

namespace offer_manager.Tests.Controllers;

public class OffersConnectorAvailabilityTests
{
    [Fact]
    public void PgConnector_TypeIsAvailable()
    {
        typeof(PgConnector).Should().NotBeNull();
    }
}
