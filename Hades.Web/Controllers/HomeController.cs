using Hades.Web.ViewModels;
using System.Web.Mvc;

namespace Hades.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult IndexAdm()
        {
            ViewBag.Title = "HADES";
            ViewBag.NomeUsuario = UsuarioLogadoViewModel.Nome;
            return View();
        }

        public ActionResult IndexUser()
        {
            ViewBag.Title = "HADES";
            ViewBag.NomeUsuario = UsuarioLogadoViewModel.Nome;
            return View();
        }
    }
}
