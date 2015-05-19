using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace DevOpsFeed.Auth.Controllers
{
    public struct User {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class AuthController : Controller
    {
        private const string testToken = "testToken123456789";
        
        [HttpPost]
        [Route("api/auth")]
        public ActionResult Auth([FromBody]User auth)
        {
            if (auth.Username == "test" && auth.Password == "test") {
                return Content(testToken);
            }

            return new HttpUnauthorizedResult();
        }
        
        [HttpGet]
        [Route("api/auth/claim")]
        public ActionResult Claim(string token, string claim)
        {
            if (token == testToken && claim == "CanAccessFeed")
            {
                return Content("true");
            }

            return new HttpUnauthorizedResult();
        }
    }
}
