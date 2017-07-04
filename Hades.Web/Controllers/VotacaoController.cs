using System;
using System.Web.Mvc;
using Hades.Application.Interface;
using Hades.Domain.Entities;

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
        public ActionResult Create()
        {
            return View();
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

                return RedirectToAction("Index", "Enquete");
            }
            catch(Exception e)
            {
                return ErrorMessage("Erro ao trazer postar voto, " + e.Message);
            }
        }
    }
}
