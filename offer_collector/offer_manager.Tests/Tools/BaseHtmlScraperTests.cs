using System.Net;
using FluentAssertions;
using Microsoft.Extensions.Hosting;
using Moq;
using Moq.Protected;
using Offer_collector.Models.OfferScrappers;
using Offer_collector.Models.Tools;
using worker.Models.Tools;
using Xunit;

namespace offer_manager.Tests.Tools
{
    public class BaseHtmlScraperTests
    {
        public BaseHtmlScraperTests() {
            HeadlessbrowserSettings.Port = 9222;
            HeadlessbrowserSettings.Host = "localhost";
        }

        private class TestScraper : BaseHtmlScraper
        {
            public TestScraper(Offer_collector.Models.ClientType clientType) : base(clientType) 
            {
            }

            public override IAsyncEnumerable<(string, string, List<string>)> GetOfferAsync(StackExchange.Redis.IDatabase redisDB, CancellationToken cancellationToken, string url = "", int batchSize = 5, int offset = 0)
            {
                throw new NotImplementedException();
            }

            public string PublicDecodeUnicodeStrict(string input) => DecodeUnicodeStrict(input);
            public string PublicGetJsonFragment(string html, string pattern, int index) => GetJsonFragment(html, pattern, index);
        }

        [Fact]
        public async Task DecodeUnicodeStrict_ShouldDecodeUnicodeSequences()
        {
            var scraper = new TestScraper(0);
            string input = @"Test \u0024 value"; // \u0024 = $
            string expected = "Test $ value";

            string result = scraper.PublicDecodeUnicodeStrict(input);

            result.Should().Be(expected);
        }

        [Fact]
        public void DecodeUnicodeStrict_InvalidSequence_ShouldLeaveAsIs_OrThrow()
        {
            var scraper = new TestScraper(0);
            string input = @"Test \u002 value";
            
            string result = scraper.PublicDecodeUnicodeStrict(input);
            
            result.Should().Be(input);
        }

        [Fact]
        public void GetJsonFragment_NoMatch_ShouldReturnEmptyString()
        {
            var scraper = new TestScraper(0);
            string html = "<html><body>No match here</body></html>";
            string pattern = @"script>(.*?)</script";

            string result = scraper.PublicGetJsonFragment(html, pattern, 0);

            result.Should().BeEmpty();
        }

         [Fact]
        public void GetJsonFragment_IndexOutOfBounds_ShouldHandleGracefully()
        {
            var scraper = new TestScraper(0);
            string html = "<script>one</script>";
            string pattern = @"<script>(.*?)</script>";
            
            string result = scraper.PublicGetJsonFragment(html, pattern, 1);

            result.Should().BeEmpty();
        }
    }
}
