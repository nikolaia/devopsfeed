using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace DevOpsFeed.Feed.Controllers
{
    [Route("api/[controller]")]
    public class FeedController : Controller
    {
        private readonly IAuthAPI _authAPI;

        public FeedController(IAuthAPI authAPI)
        {
            _authAPI = authAPI;
        }

        [HttpGet]
        public async Task<ActionResult> Get(string token)
        {
            if (await _authAPI.CheckClaim(token, "CanAccessFeed"))
            {
                return Json(new FeedItem[] {
                    new FeedItem()
                    {
                        Time = DateTime.Now.AddDays(-1),
                        Message = "Deploy Failed to Production",
                        System = "Octopus Deploy"
                    },
                    new FeedItem()
                    {
                        Time = DateTime.Now.AddDays(-3),
                        Message = "DevOpsFeed.Feed build 1234 successful.",
                        System = "AppVeyour"
                    },
                    new FeedItem()
                    {
                        Time = DateTime.Now.AddDays(-5),
                        Message = "New SSH key added to repo DevOpsFeed.Auth.",
                        System = "Github"
                    }

                });
            }

            return new HttpUnauthorizedResult();
        }
    }
}
