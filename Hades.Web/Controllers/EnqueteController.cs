using System.Collections.Generic;
using System.Web.Mvc;
using Hades.Application.Interface;
using Hades.Domain.Entities;
using Hades.Web.ViewModels;
using Newtonsoft.Json;

namespace Hades.Web.Controllers
{
    public class EnqueteController : Controller
    {
        private readonly IEnqueteAppService _enqueteAppService;

        public EnqueteController(IEnqueteAppService enqueteAppService)
        {
            _enqueteAppService = enqueteAppService;
        }

        // GET: Enquete
        public ActionResult Index()
        {
            var usuarioViewModel = _enqueteAppService.GetAll();
            if (!usuarioViewModel.IsSuccessStatusCode)
                return View("Error");
            var enquetes =
                JsonConvert.DeserializeObject<IEnumerable<EnqueteViewModel>>(usuarioViewModel.Content.ReadAsStringAsync().Result);

            return View(enquetes);
        }

        // GET: Enquete/Details/5
        public ActionResult Details(int id)
        {
            var enquete = _enqueteAppService.GetById(id);
            if (!enquete.IsSuccessStatusCode)
                return View("Error");
            var mostraEnquete =
                JsonConvert.DeserializeObject<EnqueteViewModel>(enquete.Content.ReadAsStringAsync().Result);

            return View(mostraEnquete);
        }

        // GET: Enquete/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Enquete/Create
        [HttpPost]
        public ActionResult CreateConfirmed(Enquete enquete)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _enqueteAppService.Post(enquete);
                    return RedirectToAction("Index");
                }
                return View("Error");
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: Enquete/Edit/5
        public ActionResult Edit(int id)
        {
            var response = _enqueteAppService.GetById(id);
            if (!response.IsSuccessStatusCode)
                return View("Error");
            var enquete = JsonConvert.DeserializeObject<EnqueteViewModel>(response.Content.ReadAsStringAsync().Result);

            return View(enquete);
        }

        // POST: Enquete/Edit/5
        [HttpPost]
        public ActionResult EditConfirmed(Enquete enquete)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _enqueteAppService.Put(enquete);
                    return RedirectToAction("Index");
                }
                return View("Error");
            }
            catch
            {
                return View("Error");
            }
        }

        public ActionResult DesativarEnquete(int id)
        {
            var response = _enqueteAppService.StatusEnquete(id, false);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View("Error");
        }
    }
}
