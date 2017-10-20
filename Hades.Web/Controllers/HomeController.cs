using Hades.Web.ViewModels;
using System.Web.Mvc;

namespace Hades.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = "HADES";
            ViewBag.IdUsuario = UsuarioLogadoViewModel.Id;
            ViewBag.NomeUsuario = UsuarioLogadoViewModel.Nome;
            return View();
        }
    }
}
