using System.Collections.Generic;
using System.Security.Claims;
using Thinktecture.IdentityServer.Core;
using Thinktecture.IdentityServer.Core.Models;

namespace AspNet5Host.Configuration
{
    public class Clients
    {
        public static List<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientName = "MVC6 Client",
                    Enabled = true,

                    ClientId = "mvc6",
                    Flow = Flows.Implicit,

                    RequireConsent = true,
                    AllowRememberConsent = true,

                    RedirectUris = new List<string>
                    {
                        "http://localhost:2334/",
                    },
                }
            };
        }
    }
}