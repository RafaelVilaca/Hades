using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hades.Application.Interface;
using Hades.Domain.Entities;
using Hades.Web.ViewModels;
using Newtonsoft.Json;

namespace Hades.Web.Controllers
{
    public class SorteioController : Controller
    {
        private readonly ISorteioAppService _sorteioAppService;

        public SorteioController(ISorteioAppService sorteioAppService)
        {
            _sorteioAppService = sorteioAppService;
        }

        // GET: Sorteio
        public ActionResult Index()
        {
            var sorteioViewModel = _sorteioAppService.GetAll();
            if (!sorteioViewModel.IsSuccessStatusCode)
                return View("Error");
            var sorteio =
                JsonConvert.DeserializeObject<IEnumerable<Sorteio>>(sorteioViewModel.Content.ReadAsStringAsync().Result);
            return View(sorteio);
        }

        // GET: Sorteio/Details/5
        public ActionResult Details(int id)
        {
            var sorteio = _sorteioAppService.GetById(id);
            if (!sorteio.IsSuccessStatusCode)
                return View("Error");
            var mostraSorteio =
                JsonConvert.DeserializeObject<SorteioViewModel>(sorteio.Content.ReadAsStringAsync().Result);

            return View(mostraSorteio);
        }

        // GET: Sorteio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sorteio/Create
        [HttpPost]
        public ActionResult CreateConfirmed(Sorteio sorteio)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _sorteioAppService.Post(sorteio);
                    return RedirectToAction("Index");
                }
                return View("Error");
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: Sorteio/Edit/5
        public ActionResult Edit(int id)
        {
            var response = _sorteioAppService.GetById(id);
            if (!response.IsSuccessStatusCode)
                return View("Error");
            var sorteio = JsonConvert.DeserializeObject<SorteioViewModel>(response.Content.ReadAsStringAsync().Result);

            return View(sorteio);
        }

        // POST: Sorteio/Edit/5
        [HttpPost]
        public ActionResult EditConfirmed(Sorteio sorteio)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _sorteioAppService.Put(sorteio);
                    return RedirectToAction("Index");
                }
                return View("Error");
            }
            catch
            {
                return View("Error");
            }
        }

        // POST: Sorteio/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _sorteioAppService.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Error");
            }
        }
    }
}
