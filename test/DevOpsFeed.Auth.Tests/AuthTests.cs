using Microsoft.AspNet.TestHost;
using System.Linq;
using System.Net.Http;
using System.Net;
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
        public async void FirstRequestShouldReturnHelloFirstTimer()
        {
            var server = CreateTestServer();

            var response = await server.CreateClient().GetAsync("/");
            
            Assert.Equal("Hello First timer", await response.Content.ReadAsStringAsync());
        }
        
        [Fact]
        public async void SecondRequestShouldReturnHelloOldTimer()
        {
            var server = CreateTestServer();
            var client = server.CreateClient();
            
            var initialResponse = await client.GetAsync("/");

            var request = new HttpRequestMessage(HttpMethod.Get, "/");
            var setCookie = initialResponse.Headers.GetValues("Set-Cookie").SingleOrDefault();
            var cookieName = setCookie.Split(new[] { ';' }, 2).First();
            
            request.Headers.Add("Set-Cookie", setCookie);
            request.Headers.Add("Cookie", cookieName);

            var response = await client.SendAsync(request);
            Assert.Equal("Hello old timer", await response.Content.ReadAsStringAsync());
        }
        
        [Fact]
        public async void SecondRequestShouldReturnHelloFirstTimerIfNotAuthorized()
        {
            var server = CreateTestServer();
            var client = server.CreateClient();
            
            var initialResponse = await client.GetAsync("/");
            var secondResponse = await client.GetAsync("/");
            
            Assert.Equal("Hello First timer", await secondResponse.Content.ReadAsStringAsync());
        }
    }
}
