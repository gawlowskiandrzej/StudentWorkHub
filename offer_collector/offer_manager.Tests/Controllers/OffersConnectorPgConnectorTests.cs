using FluentAssertions;
using OffersConnector;
using System;
using Xunit;

namespace offer_manager.Tests.Controllers;

public class OffersConnectorPgConnectorTests
{
    [Fact]
    public void Constructor_WithInvalidPort_ThrowsPgConnectorException()
    {
        Action act = () => new PgConnector("user", "pass", "localhost", 99999, "offers");

        act.Should().Throw<PgConnectorException>();
    }

    [Fact]
    public void Constructor_WithEmptyUsername_ThrowsPgConnectorException()
    {
        Action act = () => new PgConnector(string.Empty, "pass", "localhost", 5432, "offers");

        act.Should().Throw<PgConnectorException>();
    }
}
