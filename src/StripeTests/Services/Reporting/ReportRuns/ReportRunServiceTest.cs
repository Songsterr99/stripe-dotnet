namespace StripeTests.Reporting
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Stripe.Reporting;
    using Xunit;

    public class ReportRunServiceTest : BaseStripeTest
    {
        private const string ReportRunId = "frr_123";

        private ReportRunService service;
        private ReportRunCreateOptions createOptions;
        private ReportRunListOptions listOptions;

        public ReportRunServiceTest()
        {
            this.service = new ReportRunService();

            this.createOptions = new ReportRunCreateOptions
            {
                Parameters = new ParametersOptions
                {
                    ConnectedAccount = "acct_123",
                    IntervalStart = DateTime.Now,
                },
                ReportType = "activity.summary.1",
            };

            this.listOptions = new ReportRunListOptions
            {
            };
        }

        [Fact]
        public void Create()
        {
            var reportRun = this.service.Create(this.createOptions);
            this.AssertRequest(HttpMethod.Post, "/v1/reporting/report_runs");
            Assert.NotNull(reportRun);
        }

        [Fact]
        public async Task CreateAsync()
        {
            var reportRun = await this.service.CreateAsync(this.createOptions);
            this.AssertRequest(HttpMethod.Post, "/v1/reporting/report_runs");
            Assert.NotNull(reportRun);
        }

        [Fact]
        public void Get()
        {
            var reportRun = this.service.Get(ReportRunId);
            this.AssertRequest(HttpMethod.Get, "/v1/reporting/report_runs/frr_123");
            Assert.NotNull(reportRun);
        }

        [Fact]
        public async Task GetAsync()
        {
            var reportRun = await this.service.GetAsync(ReportRunId);
            this.AssertRequest(HttpMethod.Get, "/v1/reporting/report_runs/frr_123");
            Assert.NotNull(reportRun);
        }

        [Fact]
        public void List()
        {
            var reportRuns = this.service.List(this.listOptions);
            this.AssertRequest(HttpMethod.Get, "/v1/reporting/report_runs");
            Assert.NotNull(reportRuns);
        }

        [Fact]
        public async Task ListAsync()
        {
            var reportRuns = await this.service.ListAsync(this.listOptions);
            this.AssertRequest(HttpMethod.Get, "/v1/reporting/report_runs");
            Assert.NotNull(reportRuns);
        }
    }
}
