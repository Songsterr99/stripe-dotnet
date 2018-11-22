namespace StripeTests
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Stripe;
    using Xunit;

    public class AccountServiceTest : BaseStripeTest
    {
        private const string AccountId = "acct_123";

        private AccountService service;
        private AccountCreateOptions createOptions;
        private AccountUpdateOptions updateOptions;
        private AccountListOptions listOptions;
        private AccountRejectOptions rejectOptions;

        public AccountServiceTest()
        {
            this.service = new AccountService();

            this.createOptions = new AccountCreateOptions
            {
                Type = AccountType.Custom,
                BusinessProfile = new AccountBusinessProfileOptions
                {
                    Logo = "file_123",
                    Name = "My Business Name",
                    ProductDescription = "Payments APIs",
                },
                BusinessType = "company",
                Company = new AccountCompanyOptions
                {
                    Address = new AddressOptions
                    {
                        State = "CA",
                        City = "City",
                        Line1 = "Line1",
                        Line2 = "Line2",
                        PostalCode = "90210",
                        Country = "US",
                    },
                    Name = "My Business Name",
                },
                ExternalAccountId = "tok_visa_debit",
                RequestedCapabilities = new List<string>
                {
                    "platform_payments",
                },
                Settings = new AccountSettingsOptions
                {
                    Charge = new AccountSettingsChargeOptions
                    {
                        DeclineOn = new AccountSettingsChargeDeclineOnOptions
                        {
                            AvsFailure = false,
                            CvcFailure = true,
                        },
                        StatementDescriptorPrefix = "Stripe",
                    },
                    Payout = new AccountSettingsPayoutOptions
                    {
                        DebitNegativeBalances = true,
                        Schedule = new AccountSettingsPayoutScheduleOptions
                        {
                            Interval = "month",
                            MonthlyAnchor = "1",
                        },
                    },
                },
            };

            this.updateOptions = new AccountUpdateOptions()
            {
                Metadata = new Dictionary<string, string>()
                {
                    { "key", "value" },
                },
            };

            this.rejectOptions = new AccountRejectOptions
            {
                Reason = "terms_of_service"
            };

            this.listOptions = new AccountListOptions()
            {
                Limit = 1,
            };
        }

        [Fact]
        public void Create()
        {
            var account = this.service.Create(this.createOptions);
            this.AssertRequest(HttpMethod.Post, "/v1/accounts");
            Assert.NotNull(account);
            Assert.Equal("account", account.Object);
        }

        [Fact]
        public async Task CreateAsync()
        {
            var account = await this.service.CreateAsync(this.createOptions);
            this.AssertRequest(HttpMethod.Post, "/v1/accounts");
            Assert.NotNull(account);
            Assert.Equal("account", account.Object);
        }

        [Fact]
        public void Delete()
        {
            var deleted = this.service.Delete(AccountId);
            this.AssertRequest(HttpMethod.Delete, "/v1/accounts/acct_123");
            Assert.NotNull(deleted);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            var deleted = await this.service.DeleteAsync(AccountId);
            this.AssertRequest(HttpMethod.Delete, "/v1/accounts/acct_123");
            Assert.NotNull(deleted);
        }

        [Fact]
        public void Get()
        {
            var account = this.service.Get(AccountId);
            this.AssertRequest(HttpMethod.Get, "/v1/accounts/acct_123");
            Assert.NotNull(account);
            Assert.Equal("account", account.Object);
        }

        [Fact]
        public async Task GetAsync()
        {
            var account = await this.service.GetAsync(AccountId);
            this.AssertRequest(HttpMethod.Get, "/v1/accounts/acct_123");
            Assert.NotNull(account);
            Assert.Equal("account", account.Object);
        }

        [Fact]
        public void GetSelf()
        {
            var account = this.service.GetSelf();
            this.AssertRequest(HttpMethod.Get, "/v1/account");
            Assert.NotNull(account);
            Assert.Equal("account", account.Object);
        }

        [Fact]
        public async Task GetSelfAsync()
        {
            var account = await this.service.GetSelfAsync();
            this.AssertRequest(HttpMethod.Get, "/v1/account");
            Assert.NotNull(account);
            Assert.Equal("account", account.Object);
        }

        [Fact]
        public void List()
        {
            var accounts = this.service.List(this.listOptions);
            this.AssertRequest(HttpMethod.Get, "/v1/accounts");
            Assert.NotNull(accounts);
            Assert.Equal("list", accounts.Object);
            Assert.Single(accounts.Data);
            Assert.Equal("account", accounts.Data[0].Object);
        }

        [Fact]
        public async Task ListAsync()
        {
            var accounts = await this.service.ListAsync(this.listOptions);
            this.AssertRequest(HttpMethod.Get, "/v1/accounts");
            Assert.NotNull(accounts);
            Assert.Equal("list", accounts.Object);
            Assert.Single(accounts.Data);
            Assert.Equal("account", accounts.Data[0].Object);
        }

        [Fact]
        public void Reject()
        {
            var account = this.service.Reject(AccountId, this.rejectOptions);
            this.AssertRequest(HttpMethod.Post, "/v1/accounts/acct_123/reject");
            Assert.NotNull(account);
            Assert.Equal("account", account.Object);
        }

        [Fact]
        public async Task RejectAsync()
        {
            var account = await this.service.RejectAsync(AccountId, this.rejectOptions);
            this.AssertRequest(HttpMethod.Post, "/v1/accounts/acct_123/reject");
            Assert.NotNull(account);
            Assert.Equal("account", account.Object);
        }

        [Fact]
        public void Update()
        {
            var account = this.service.Update(AccountId, this.updateOptions);
            this.AssertRequest(HttpMethod.Post, "/v1/accounts/acct_123");
            Assert.NotNull(account);
            Assert.Equal("account", account.Object);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            var account = await this.service.UpdateAsync(AccountId, this.updateOptions);
            this.AssertRequest(HttpMethod.Post, "/v1/accounts/acct_123");
            Assert.NotNull(account);
            Assert.Equal("account", account.Object);
        }
    }
}
