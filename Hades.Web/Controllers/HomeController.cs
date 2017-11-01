using System.Web.Mvc;

namespace Hades.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = "HADES";
            ViewBag.IdUsuario = Session["IdUsuario"];
            ViewBag.NomeUsuario = Session["Nome"];
            return View();
        }
    }
}
