using Projekt.DAL;
using Projekt.Models;
using Projekt.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projekt.Controllers
{
    public class HomeController : Controller
    {
        SiteContext db = new SiteContext();
        public ActionResult Index()
        {
            Profil profile = db.Profiles.FirstOrDefault(p => p.Login == User.Identity.Name);

            ViewBag.Profil = profile;

            return View();
        }

        public ActionResult About()
        {
            IQueryable<EnrollmentDateGroup> data = from Post in db.Posts
                                                   group Post by Post.Title into Group
                                                   select new EnrollmentDateGroup()
                                                   {
                                                       Title = Group.Key,
                                                       TitleCount = Group.Count()
                                                   };
            return View(data.ToList());
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}