using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.DependencyInjection;

namespace DevOpsFeed.Auth
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDataProtection();
        }

        public void Configure(IApplicationBuilder app, IApplicationEnvironment env)
        {
            var certFile = env.ApplicationBasePath + "\\idsrv3test.pfx";

            app.Map("/core", core =>
            {
                var factory = InMemoryFactory.Create(
                                users: Users.Get(),
                                clients: Clients.Get(),
                                scopes: Scopes.Get());

                var idsrvOptions = new IdentityServerOptions
                {
                    Factory = factory,
                    RequireSsl = false,
                    SigningCertificate = new X509Certificate2(certFile, "idsrv3test")
                };

                core.UseIdentityServer(idsrvOptions);
            });
        }
    }
}
