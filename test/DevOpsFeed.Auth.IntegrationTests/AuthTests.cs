using System.Net.Http;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.TestHost;
using Microsoft.Framework.DependencyInjection;
using Xunit;
using System.Net.Http.Formatting;
using DevOpsFeed.Auth.Controllers;

namespace DevOpsFeed.Auth.IntegrationTests
{
    public class AuthTests
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
            });
        }

        private const string testToken = "testToken123456789";

        [Fact]
        public async void ValidUsernameAndPasswordShouldReturnToken()
        {
            var server = CreateTestServer();

            var user = new ObjectContent<User>(new User {            
                Username = "test",
                Password = "test"
            }, new JsonMediaTypeFormatter());

            var response = await server.CreateClient().PostAsync("/api/auth/", user);

            var token = await response.Content.ReadAsStringAsync();
            
            Assert.Equal(testToken, token);
        }

        [Fact]
        public async void ValidTokenShouldReturnClaims()
        {
            var server = CreateTestServer();
            
            var response = await server.CreateClient().GetAsync("/api/auth/claim?token="+testToken+"&claim=CanAccessFeed");

            var claim = await response.Content.ReadAsStringAsync();
            
            Assert.Equal("true", claim);
        }
    }
}
