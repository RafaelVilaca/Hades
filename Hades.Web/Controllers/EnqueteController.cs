using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Hades.Application.Interface;
using Hades.Domain.Entities;
using Hades.Web.ViewModels;
using Newtonsoft.Json;

namespace Hades.Web.Controllers
{
    public class EnqueteController : BaseController
    {
        private readonly IEnqueteAppService _enqueteAppService;
        private readonly IVotacaoAppService _votacaoAppService;

        public EnqueteController(IEnqueteAppService enqueteAppService, IVotacaoAppService votacaoAppService)
        {
            _enqueteAppService = enqueteAppService;
            _votacaoAppService = votacaoAppService;
        }

        // GET: Enquete
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BuscaGridEnquetes()
        {
            try
            {
                var usuarioViewModel = _enqueteAppService.GetAll();
                if (!usuarioViewModel.IsSuccessStatusCode)
                    return ErrorMessage("Erro ao trazer lista de enquetes.");
                var enquetes =
                    JsonConvert.DeserializeObject<IEnumerable<EnqueteViewModel>>(usuarioViewModel.Content.ReadAsStringAsync().Result);

                return View("_TabelaEnquetes", enquetes);
            }
            catch (Exception e)
            {
                return ErrorMessage("Erro ao montar lista de clientes, " + e.Message);
            }
        }

        // GET: Enquete/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var enquete = _enqueteAppService.GetById(id);
                if (!enquete.IsSuccessStatusCode)
                    return ErrorMessage("Erro ao buscar enquete");
                var mostraEnquete =
                    JsonConvert.DeserializeObject<EnqueteViewModel>(enquete.Content.ReadAsStringAsync().Result);

                return View(mostraEnquete);
            }
            catch (Exception e)
            {
                return ErrorMessage("Erro ao trazer usuario, " + e.Message);
            }

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
                var response = _enqueteAppService.Post(enquete);
                if (!response.IsSuccessStatusCode)
                    ErrorMessage("Erro ao criar enquete");
                return RedirectToAction("BuscaGridEnquetes");
            }
            catch (Exception e)
            {
                return ErrorMessage("Erro ao criar enquete, " + e.Message);
            }
        }

        // GET: Enquete/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var response = _enqueteAppService.GetById(id);
                if (!response.IsSuccessStatusCode)
                    return ErrorMessage("Erro ao buscar enquete.");
                var enquete = JsonConvert.DeserializeObject<EnqueteViewModel>(response.Content.ReadAsStringAsync().Result);
                return View(enquete);
            }
            catch (Exception e)
            {
                return ErrorMessage("Erro ao buscar usuario, " + e.Message);
            }
        }

        // POST: Enquete/Edit/5
        [HttpPost]
        public ActionResult EditConfirmed(Enquete enquete)
        {
            try
            {
                var response = _enqueteAppService.Put(enquete);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("BuscaGridEnquetes");
                return ErrorMessage("Erro ao editar Enquete");
            }
            catch (Exception e)
            {
                return ErrorMessage("Erro ao editar cliente, " + e.Message);
            }
        }

        public ActionResult DesativarEnquete(int id)
        {
            try
            {
                var response = _enqueteAppService.StatusEnquete(id, false);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("BuscaGridEnquetes");
                return ErrorMessage("Erro ao desativar Enquete");
            }
            catch (Exception e)
            {
                return ErrorMessage("Erro ao desativar Enquete, " + e.Message);
            }
            
        }

        public ActionResult Votar()
        {

            return View("");
        }
    }
}
