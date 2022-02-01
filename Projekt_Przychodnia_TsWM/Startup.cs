using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using Projekt_Przychodnia_TsWM.Models;
using System;

[assembly: OwinStartupAttribute(typeof(Projekt_Przychodnia_TsWM.Startup))]
namespace Projekt_Przychodnia_TsWM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createAdminUser();
        }

        private void createAdminUser()
        {
            using (var context = new ApplicationDbContext())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                if (!roleManager.RoleExists("Admin"))
                {
                    var role = new IdentityRole
                    {
                        Name = "Admin"
                    };

                    roleManager.Create(role);
                }

                if (userManager.FindByName("admin@sytkal.pl") == null)
                {
                    var user = new ApplicationUser
                    {
                        FirstName = "Wiktor",
                        LastName = "Kalaga",
                        UserName = "admin@sytkal.pl",
                        Email = "admin@sytkal.pl",
                        BirthDate = new DateTime(2000, 02, 11)
                    };

                    string userPass = "1234@Aa";

                    var chkUser = userManager.Create(user, userPass);

                    if (chkUser.Succeeded)
                    {
                        _ = userManager.AddToRole(user.Id, "Admin");
                    }
                }
            }
        }
    }
}
