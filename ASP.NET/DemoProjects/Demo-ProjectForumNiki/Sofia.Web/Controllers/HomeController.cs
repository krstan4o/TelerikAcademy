using Sofia.Models;
using Sofia.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sofia.Web.Controllers
{
    public class HomeController : Controller
    {
        SofiaDb db = new SofiaDb();

        public ActionResult Index()
        {
            var latestForumPosts = db.ForumPosts
                .OrderByDescending(x => x.CreationTime)
                .Take(10)
                .Select(x => new ForumPostViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    CreationTime = x.CreationTime,
                    Username = x.Author.UserName,
                    Rating =
                        db.ForumPostVotes.Count(y => y.Post.Id == x.Id && y.IsUpVote) -
                        db.ForumPostVotes.Count(y => y.Post.Id == x.Id && !y.IsUpVote)
                });            

            return View(latestForumPosts);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
