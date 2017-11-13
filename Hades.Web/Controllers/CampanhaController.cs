using Hades.Application.Interface;
using Hades.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hades.Web.Controllers
{
    public class CampanhaController : BaseController
    {
        private readonly ICampanhaAppService _campanhaAppService;
        private readonly ICampanhaParticipanteAppService _campanhaParticipanteAppService;
        private readonly IUsuarioAppService _usuarioAppService;

        public CampanhaController(ICampanhaAppService campanhaAppService, ICampanhaParticipanteAppService campanhaParticipanteAppService, IUsuarioAppService usuarioAppService)
        {
            _campanhaAppService = campanhaAppService;
            _campanhaParticipanteAppService = campanhaParticipanteAppService;
            _usuarioAppService = usuarioAppService;
        }

        public ActionResult Index()
        {
            ViewBag.NomeUsuario = Session["Nome"].ToString();
            return View();
        }

        public ActionResult BuscaGridCampanhas()
        {
            var campanhaViewModel = _campanhaAppService.GetCampanhas();
            if (!campanhaViewModel.IsSuccessStatusCode)
                return ErrorMessage("Erro ao trazer campanhas");
            var campanha = JsonConvert.DeserializeObject<IEnumerable<CampanhaViewModel>>(campanhaViewModel.Content.ReadAsStringAsync().Result);
            return View("_TabelaCampanha", campanha);
        }

        public ActionResult Details(int id)
        {
            var campanha = _campanhaAppService.GetCampanha(id);
            if (!campanha.IsSuccessStatusCode)
                return ErrorMessage("Erro ao trazer Campanha");
            var mostraCampanha =
                JsonConvert.DeserializeObject<CampanhaViewModel>(campanha.Content.ReadAsStringAsync().Result);

            return View(mostraCampanha);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult CreateConfirmed(CampanhaDto campanha)
        {
            var response = _campanhaAppService.Post(campanha);
            if (response.IsSuccessStatusCode)
                return Json("OK");
            return ErrorMessage("Erro ao criar Campanha");
        }

        public ActionResult Edit(int id)
        {
            var response = _campanhaAppService.GetCampanha(id);
            if (!response.IsSuccessStatusCode)
                return ErrorMessage("Erro ao trazer campanha");
            var campanha = JsonConvert.DeserializeObject<CampanhaViewModel>(response.Content.ReadAsStringAsync().Result);

            return View(campanha);
        }

        public ActionResult EditConfirmed(CampanhaDto campanha)
        {
            var response = _campanhaAppService.Put(campanha);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");
            return ErrorMessage("Erro ao editar campanha");
        }

        public ActionResult Delete(int id)
        {
            var retorno = _campanhaAppService.Delete(id);
            if (!retorno.IsSuccessStatusCode)
                return ErrorMessage("Erro ao deletar campanha");
            var response = _campanhaAppService.GetCampanhas();
            var campanha = JsonConvert.DeserializeObject<IEnumerable<CampanhaViewModel>>(response.Content.ReadAsStringAsync().Result);
            return View("_TabelaCampanha", campanha);
        }
    }
}