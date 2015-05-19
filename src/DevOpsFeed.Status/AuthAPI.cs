using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DevOpsFeed.Feed
{
    public interface IAuthAPI
    {
        Task<bool> CheckClaim(string token, string claim);
    }

    public class AuthAPI : IAuthAPI
    {
        private readonly string _apiAddress;

        public AuthAPI(string apiAddress)
        {
            _apiAddress = apiAddress;
        }

        public async Task<bool> CheckClaim(string token, string claim)
        {
            var response = await new HttpClient().GetAsync(_apiAddress + "/api/auth/claim?token=" + token + "&claim=" + claim);
            var result = await response.Content.ReadAsStringAsync();

            return result == "true";
        }
    }
}