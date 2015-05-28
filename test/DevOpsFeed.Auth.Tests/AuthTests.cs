using Microsoft.AspNet.TestHost;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Xunit;

namespace DevOpsFeed.Auth.Tests
{
    public class AuthTests
    {
        private TestServer CreateTestServer()
        {
            return TestServer.Create(app =>
            {
                var logging = app.ApplicationServices.GetRequiredService<ILoggerFactory>();
                new Startup().Configure(app, logging);
            }, services =>
            {
                services.AddAuthentication();
            });
        }

        [Fact]
        public async void ValidUsernameAndPasswordShouldReturnToken()
        {
            var server = CreateTestServer();

            var response = await server.CreateClient().GetAsync("/");
            
            Assert.Equal("Hello First timer", await response.Content.ReadAsStringAsync());
        }
    }
}
