using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.TestHost;
using Microsoft.Framework.DependencyInjection;
using Xunit;

namespace DevOpsFeed.Feed.IntegrationTests
{
    public class StatusTests
    {
        private TestServer CreateTestServer()
        {
            return TestServer.Create(app =>
            {
                var env = app.ApplicationServices.GetRequiredService<IHostingEnvironment>();
                new Startup(env).Configure(app, env);
            }, services =>
            {
                services.AddMvc();
                services.AddInstance(typeof(IAuthAPI), new TestAuthAPI());
            });
        }

        private const string testToken = "testToken123456789";

        public class TestAuthAPI : IAuthAPI
        {
            public Task<bool> CheckClaim(string token, string claim)
            {
                if (token == testToken && claim == "CanAccessStatus") return Task.FromResult(true);
                else return Task.FromResult(false);
            }
        }

        [Fact]
        public async void ValidUsernameAndPasswordShouldReturnToken()
        {
            var server = CreateTestServer();

            var response = await server.CreateClient().GetAsync("/api/status/?token=" + testToken);

            var status = await response.Content.ReadAsStringAsync();

            Assert.True(status.Contains("DevOpsFeed.Auth"));
        }
    }
}
