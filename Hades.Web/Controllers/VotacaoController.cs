using Hades.Application.Interface;
using Hades.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;

namespace Hades.Web.Controllers
{
    public class VotacaoController : BaseController
    {
        private readonly IVotacaoAppService _votacaoAppService;

        public VotacaoController(IVotacaoAppService votacaoAppService)
        {
            _votacaoAppService = votacaoAppService;
        }

        // GET: Votacao/Create
        public ActionResult Create(Votacao votacao)
        {
            var voto = _votacaoAppService.GetVoto(votacao);
            if (!voto.IsSuccessStatusCode)
                return ErrorMessage("Erro ao trazer Votação");
            var getVotacao = JsonConvert.DeserializeObject<Votacao>(voto.Content.ReadAsStringAsync().Result);
            return View(getVotacao);
        }

        // POST: Votacao/Create
        [HttpPost]
        public ActionResult CreateConfirmed(Votacao votacao)
        {
            try
            {
                var voto = _votacaoAppService.Post(votacao);
                if (!voto.IsSuccessStatusCode)
                    return ErrorMessage("Erro ao trazer postar voto");

                return Json("Ok");
            }
            catch(Exception e)
            {
                return ErrorMessage("Erro ao trazer postar voto, " + e.Message);
            }
        }
    }
}
