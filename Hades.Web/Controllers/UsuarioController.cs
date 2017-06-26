using System.Collections.Generic;
using System.Web.Mvc;
using Hades.Application.Interface;
using Hades.Web.ViewModels;
using Newtonsoft.Json;
using Hades.Domain.Entities;

namespace Hades.Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioAppService _usuarioAppService;

        public UsuarioController(IUsuarioAppService usuarioAppService)
        {
            _usuarioAppService = usuarioAppService;
        }

        // GET: Usuario
        public ActionResult Index()
        {
            var usuarioViewModel = _usuarioAppService.GetAll();
            if (!usuarioViewModel.IsSuccessStatusCode)
                return View("Error");
            var usuarios = 
                JsonConvert.DeserializeObject<IEnumerable<UsuarioViewModel>>(usuarioViewModel.Content.ReadAsStringAsync().Result);
            
            return View(usuarios);
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
        [HttpPost]
        public ActionResult CreateConfirmed(Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _usuarioAppService.Post(usuario);
                    return RedirectToAction("Index");
                }
                return View("Error");
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

        // POST: Usuario/Edit/5
        [HttpPut]
        public ActionResult EditConfirmed(Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _usuarioAppService.Put(usuario);
                    return RedirectToAction("Index");
                }
                return View("Error");
            }
            catch
            {
                return View("Error");
            }
        }

        public ActionResult AtivarUsuario(int id)
        {
            _usuarioAppService.StatusUsuario(id, true);
            return RedirectToAction("Index");
        }

        public ActionResult DesativarUsuario(int id)
        {
            _usuarioAppService.StatusUsuario(id, false);
            return RedirectToAction("Index");
        }
    }
}
