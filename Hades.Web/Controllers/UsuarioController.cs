using System.Collections.Generic;
using System.Web.Mvc;
using Hades.Application.Interface;
using Hades.Web.ViewModels;
using Newtonsoft.Json;
using Hades.Domain.Entities;

namespace Hades.Web.Controllers
{
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioAppService _usuarioAppService;

        public UsuarioController(IUsuarioAppService usuarioAppService)
        {
            _usuarioAppService = usuarioAppService;
        }

        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int id)
        {
            var usuario = _usuarioAppService.GetById(id);
            if (!usuario.IsSuccessStatusCode)
                return View("Error");
            var mostraUsuario =
                JsonConvert.DeserializeObject<UsuarioViewModel>(usuario.Content.ReadAsStringAsync().Result);

            return View(mostraUsuario);
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        public ActionResult CreateConfirmed(Usuario usuario)
        {
            try
            {
                _usuarioAppService.Post(usuario);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            var response = _usuarioAppService.GetById(id);
            if (!response.IsSuccessStatusCode)
                return View("Error");
            var usuario = JsonConvert.DeserializeObject<UsuarioViewModel>(response.Content.ReadAsStringAsync().Result);
            return View(usuario);
        }

        public ActionResult BuscaGridUsuario()
        {
            var usuarioViewModel = _usuarioAppService.GetAll();
            if (!usuarioViewModel.IsSuccessStatusCode)
                return View("Error");
            var usuarios =
                JsonConvert.DeserializeObject<IEnumerable<UsuarioViewModel>>(usuarioViewModel.Content.ReadAsStringAsync().Result);

            return View("_TabelaUsuarios", usuarios);
        }

        // POST: Usuario/Edit/5
        public ActionResult EditConfirmed(Usuario usuario)
        {
            try
            {
                var response = _usuarioAppService.Put(usuario);
                if (response.IsSuccessStatusCode)
                    return Json("OK", JsonRequestBehavior.AllowGet);
                return View("Error");
            }
            catch
            {
                return View("Error");
            }
        }

        public ActionResult AtivarUsuario(int id)
        {
            var response = _usuarioAppService.StatusUsuario(id, true);
            if (!response.IsSuccessStatusCode)
                return View("Error");
            return RedirectToAction("Index");
        }

        public ActionResult DesativarUsuario(int id)
        {
            var response = _usuarioAppService.StatusUsuario(id, false);
            if (!response.IsSuccessStatusCode)
                return View("Error");
            return RedirectToAction("Index");
        }
    }
}
