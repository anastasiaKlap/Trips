using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Projekt.DAL
{
    public class SiteInitializer : DropCreateDatabaseIfModelChanges<SiteContext>
    {

        protected override void Seed(SiteContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(new ApplicationDbContext()));

            roleManager.Create(new IdentityRole("Admin"));
            roleManager.Create(new IdentityRole("User"));


            var profils = new List<Profil>
            {
                new Profil { Login ="admin@gmail.com", Email ="admin@gmail.com", FirstName = "A", LastName = "B"},
                new Profil { Login ="admin@gmail.com", Email ="admin@gmail.com", FirstName = "C", LastName = "D"}
            };
            profils.ForEach(c => context.Profiles.Add(c));
            context.SaveChanges();

            var user = new ApplicationUser { UserName = "jan@gmail.com" };
            string passwor = "Qwert_123";
            IdentityResult u = userManager.Create(user, passwor);
            userManager.AddToRole(user.Id, "Admin");

            

            var user3 = new ApplicationUser { UserName = "kowalsi@gmail.com" };
            string passwor3 = "Qwert_123";
            IdentityResult u3 = userManager.Create(user3, passwor3);
            userManager.AddToRole(user3.Id, "User");

            context.Profiles.Add(new Profil { Login = "jan@gmail.com" });
            context.Profiles.Add(new Profil { Login = "kowalsi@gmail.com" });
            context.SaveChanges();




        }
    }
}