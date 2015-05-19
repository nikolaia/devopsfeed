using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using DevOpsFeed.Feed;

namespace DevOpsFeed.Status.Controllers
{
    [Route("api/[controller]")]
    public class StatusController : Controller
    {
        private readonly IAuthAPI _authAPI;

        public StatusController(IAuthAPI authAPI)
        {
            _authAPI = authAPI;
        }

        [HttpGet]
        public async Task<ActionResult> Get(string token)
        {
            if (await _authAPI.CheckClaim(token, "CanAccessStatus"))
            {
                return Json(new Status[] {
                    new Status()
                    {
                        LastChecked = DateTime.Now.AddDays(-1),
                        Healthy = true,
                        System = "DevOpsFeed.Auth"
                    },
                    new Status()
                    {
                        LastChecked = DateTime.Now.AddDays(-3),
                        Healthy = false,
                        System = "DevOpsFeed.Feed"
                    },
                    new Status()
                    {
                        LastChecked = DateTime.Now.AddDays(-5),
                        Healthy = true,
                        System = "DevOpsFeed.Status"
                    }

                });
            }

            return new HttpUnauthorizedResult();
        }
    }
}
