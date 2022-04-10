using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Projekt.DAL;
using Projekt.Models;

namespace Projekt.Controllers
{
    public class PostsController : Controller
    {
        private SiteContext db = new SiteContext();

        // GET: Posts
        public ActionResult Index(string sortPosts, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortPosts;
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortPosts) ? "Title_desc" : "";
            ViewBag.TextSortParm = String.IsNullOrEmpty(sortPosts) ? "Text_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var posts = from s in db.Posts
                        select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                posts = posts.Where(s => s.Title.Contains(searchString));
            }
            switch (sortPosts)
            {

                case "Title_desc":
                    posts = posts.OrderByDescending(s => s.Title);
                    break;
                case "Text_desc":
                    posts = posts.OrderByDescending(s => s.Title);
                    break;
                default:
                    posts = posts.OrderBy(s => s.ID);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(posts.ToPagedList(pageNumber, pageSize));
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }
         
        // GET: Posts/Create
        public ActionResult Create()
        {
            Profil myProfil = db.Profiles.Single(p => p.Login == User.Identity.Name);
            ViewBag.Posts = myProfil.Posts.ToList();
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Text,Image, ProfileId")] Post post)
        {
            if (ModelState.IsValid)
            {
                Profil myProfile = db.Profiles.Single(p => p.Login == User.Identity.Name);
                post.Profil = myProfile;
                 
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }
         
        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Text,Image, ProfileId")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified; 
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
