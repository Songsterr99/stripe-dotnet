namespace StripeTests
{
    using Newtonsoft.Json;
    using Stripe;
    using Xunit;

    public class ExchangeRateTest : BaseStripeTest
    {
        [Fact]
        public void Deserialize()
        {
            string json = GetFixture("/v1/exchange_rates/usd");
            var exchangeRate = JsonConvert.DeserializeObject<ExchangeRate>(json);
            Assert.NotNull(exchangeRate);
            Assert.IsType<ExchangeRate>(exchangeRate);
            Assert.NotNull(exchangeRate.Id);
            Assert.Equal("exchange_rate", exchangeRate.Object);
        }
    }
}
