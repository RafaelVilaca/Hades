using Hades.Application;
using System.Web.Mvc;

namespace Hades.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "HADES";

            return View();
        }
    }
}
