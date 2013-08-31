using System;
using System.Linq;
using System.Web.Mvc;

namespace BlogSystem.Services.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
