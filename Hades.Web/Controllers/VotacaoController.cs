﻿using System.Web.Mvc;
using Hades.Application.Interface;
using Hades.Domain.Entities;
using Hades.Web.ViewModels;
using Newtonsoft.Json;

namespace Hades.Web.Controllers
{
    public class VotacaoController : Controller
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
                _votacaoAppService.Post(votacao);
                return RedirectToAction("Index", "Sorteio");
            }
            catch
            {
                return View("Error");
            }
        }
    }
}
