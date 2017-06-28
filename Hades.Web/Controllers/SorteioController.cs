using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Hades.Application.Interface;
using Hades.Domain.Entities;
using Hades.Web.ViewModels;
using Newtonsoft.Json;

namespace Hades.Web.Controllers
{
    public class SorteioController : BaseController
    {
        private readonly ISorteioAppService _sorteioAppService;
        private readonly ISorteioParticipanteAppService _sorteioParticipanteAppService;

        public SorteioController(ISorteioAppService sorteioAppService, ISorteioParticipanteAppService sorteioParticipanteAppService)
        {
            _sorteioAppService = sorteioAppService;
            _sorteioParticipanteAppService = sorteioParticipanteAppService;
        }

        public ActionResult Index()
        {
            var sorteioViewModel = _sorteioAppService.GetAll();
            if (!sorteioViewModel.IsSuccessStatusCode)
                return ErrorMessage("Erro ao trazer sorteios");
            var sorteio =
                JsonConvert.DeserializeObject<IEnumerable<SorteioViewModel>>(sorteioViewModel.Content.ReadAsStringAsync().Result);
            return View(sorteio);
        }
        
        public ActionResult Details(int id)
        {
            var sorteio = _sorteioAppService.GetById(id);
            if (!sorteio.IsSuccessStatusCode)
                return ErrorMessage("Erro ao trazer sorteio");
            var mostraSorteio =
                JsonConvert.DeserializeObject<SorteioViewModel>(sorteio.Content.ReadAsStringAsync().Result);

            return View(mostraSorteio);
        }
        
        public ActionResult Create()
        {
            return View();
        }
        
        public ActionResult CreateConfirmed(Sorteio sorteio)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _sorteioAppService.Post(sorteio);
                    return RedirectToAction("Index");
                }
                return ErrorMessage("Erro ao criar Sorteio");
            }
            catch (Exception e)
            {
                return ErrorMessage(e.Message);
            }
        }
        
        public ActionResult Edit(int id)
        {
            var response = _sorteioAppService.GetById(id);
            if (!response.IsSuccessStatusCode)
                return ErrorMessage("Erro ao trazer sorteio");
            var sorteio = JsonConvert.DeserializeObject<SorteioViewModel>(response.Content.ReadAsStringAsync().Result);

            return View(sorteio);
        }
        
        public ActionResult EditConfirmed(Sorteio sorteio)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _sorteioAppService.Put(sorteio);
                    return RedirectToAction("Index");
                }
                return ErrorMessage("Erro ao editar sorteio");
            }
            catch (Exception e)
            {
                return ErrorMessage(e.Message);
            }
        }
        
        public ActionResult Delete(int id)
        {
            try
            {
                _sorteioAppService.Delete(id);
                var response = _sorteioAppService.GetAll();
                var sorteio = JsonConvert.DeserializeObject<IEnumerable<SorteioViewModel>>(response.Content.ReadAsStringAsync().Result);
                return View("_TabelaSorteio", sorteio);
            }
            catch (Exception e)
            {
                return ErrorMessage(e.Message);
            }
        }
        
        public ActionResult Drawlots(int id)
        {
            var responseParticipantes = _sorteioParticipanteAppService.GetAll(id);

            if (!responseParticipantes.IsSuccessStatusCode)
            {
                return ErrorMessage("Erro ao trazer participantes");
            }

            var participantes = JsonConvert.DeserializeObject<IEnumerable<SorteioParticipanteViewModel>>(responseParticipantes.Content.ReadAsStringAsync().Result).ToList();

            if (!participantes.Any())
            {
                return ErrorMessage("Não foi encontrado nenhum participante");
            }

            var aleatorio = new Random();

            var posicao = aleatorio.Next(0, participantes.Count);

            var nome = participantes[posicao].NomeUsuario;
            
            return Json(new { message = $"O Ganhador é {nome}... Parabéns!!!" }, JsonRequestBehavior.AllowGet);
        }
    }
}
