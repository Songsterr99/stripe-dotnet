namespace StripeTests
{
    using Newtonsoft.Json;
    using Stripe;
    using Xunit;

    public class FileLinkTest : BaseStripeTest
    {
        [Fact]
        public void Deserialize()
        {
            string json = GetFixture("/v1/file_links/link_123");
            var fileLink = JsonConvert.DeserializeObject<FileLink>(json);
            Assert.NotNull(fileLink);
            Assert.IsType<FileLink>(fileLink);
            Assert.NotNull(fileLink.Id);
            Assert.Equal("file_link", fileLink.Object);
        }

        [Fact]
        public void DeserializeWithExpansions()
        {
            string[] expansions =
            {
              "file",
            };

            string json = GetFixture("/v1/file_links/link_123", expansions);
            var fileLink = JsonConvert.DeserializeObject<FileLink>(json);
            Assert.NotNull(fileLink);
            Assert.IsType<FileLink>(fileLink);
            Assert.NotNull(fileLink.Id);
            Assert.Equal("file_link", fileLink.Object);

            Assert.NotNull(fileLink.File);
            Assert.Equal("file", fileLink.File.Object);
        }
    }
}
