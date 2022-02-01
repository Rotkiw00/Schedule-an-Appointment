using Microsoft.AspNet.Identity;
using Projekt_Przychodnia_TsWM.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Projekt_Przychodnia_TsWM.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var user = dbContext.Users.FirstOrDefault(x => x.Id == userId);
                Func<Event, bool> p;
                if (User.IsInRole("Admin"))
                {
                    p = x => true;
                }
                else
                {
                    p = x => x.UserId == userId;
                }
                var events = dbContext.Events.Where(p).ToList();
                var userIds = events.Select(x => x.UserId);
                var users = dbContext.Users.Where(x => userIds.Any(y => y == x.Id)).ToDictionary(x => x.Id, x => x.FirstName + " " + x.LastName);

                foreach (Event e in events)
                {
                    e.PatientName = users[e.UserId];
                }

                UserDetailsViewModel u = new UserDetailsViewModel
                {
                    user = user,
                    events = events
                };
                return View(u);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}