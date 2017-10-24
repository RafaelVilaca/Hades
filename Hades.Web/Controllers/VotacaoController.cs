using Hades.Application.Interface;
using Hades.Domain.Entities;
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
            return View(votacao);
        }

        // POST: Votacao/Create
        [HttpPost]
        public ActionResult CreateConfirmed(Votacao votacao)
        {
            try
            {
                var response = votacao.IndicadorCadastro == "S" ? _votacaoAppService.Post(votacao) : _votacaoAppService.Put(votacao);

                return !response.IsSuccessStatusCode 
                    ? ErrorMessage("Erro ao efetuar votação, tente novamente") 
                    : Json("Ok");
            }
            catch(Exception e)
            {
                return ErrorMessage("Falha ao efetuar votação, " + e.Message);
            }
        }
    }
}
