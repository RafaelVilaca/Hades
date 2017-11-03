﻿using Hades.Application.Interface;
using Hades.Domain.Entities;
using Hades.Web.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

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
            ViewBag.NomeUsuario = Session["Nome"].ToString();
            return View();
        }

        public ActionResult BuscaGridSorteios()
        {
            var sorteioViewModel = _sorteioAppService.GetAll((int)Session["IdUsuario"]);
            if (!sorteioViewModel.IsSuccessStatusCode)
                return ErrorMessage("Erro ao trazer sorteios");
            var sorteio = JsonConvert.DeserializeObject<IEnumerable<SorteioViewModel>>(sorteioViewModel.Content.ReadAsStringAsync().Result);
            return View("_TabelaSorteio", sorteio);
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
            var response = _sorteioAppService.Post(sorteio);
            if (response.IsSuccessStatusCode)
                return Json("OK");
            return ErrorMessage("Erro ao criar Sorteio");
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
            var response = _sorteioAppService.Put(sorteio);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");
            return ErrorMessage("Erro ao editar sorteio");
        }

        public ActionResult Delete(int id)
        {
            var retorno = _sorteioAppService.Delete(id);
            if (!retorno.IsSuccessStatusCode)
                return ErrorMessage("Erro ao deletar sorteio");
            var response = _sorteioAppService.GetAll((int)Session["IdUsuario"]);
            var sorteio = JsonConvert.DeserializeObject<IEnumerable<SorteioViewModel>>(response.Content.ReadAsStringAsync().Result);
            return View("_TabelaSorteio", sorteio);
        }

        public ActionResult Drawlots(int idSorteio)
        {
            var responseParticipantes = _sorteioAppService.GetById(idSorteio);

            if (!responseParticipantes.IsSuccessStatusCode)
                return ErrorMessage("Erro ao trazer participantes");


            var dadosParticipantesSorteio = JsonConvert.DeserializeObject<SorteioViewModel>
                        (responseParticipantes.Content.ReadAsStringAsync().Result);

            if (!dadosParticipantesSorteio.SorteioParticipantes.Any())
                return ErrorMessage("Não foi encontrado nenhum participante");

            if (dadosParticipantesSorteio.QtdParticipantes <= dadosParticipantesSorteio.QtdeItens)
            {
                foreach (var vencedor in dadosParticipantesSorteio.SorteioParticipantes)
                    _sorteioParticipanteAppService.VencedorParticipantesSorteio(idSorteio, vencedor.Id_Participante);

                return View(dadosParticipantesSorteio); ;
            }

            var aleatorio = new Random();
            var participantes = dadosParticipantesSorteio.SorteioParticipantes;
            var vencedores = new string[dadosParticipantesSorteio.QtdeItens];
            var contadorRotina = 0;
            var posicoesSorteadas = new int?[dadosParticipantesSorteio.QtdeItens];

            var sortearNovamente = _sorteioParticipanteAppService.SortearNovamente(idSorteio);
            if (!sortearNovamente.IsSuccessStatusCode)
                return ErrorMessage("Erro ao zerar Vencedores");

            do
            {
                var posicao = aleatorio.Next(0, participantes.Count);
                if (!posicoesSorteadas.Contains(posicao))
                {
                    posicoesSorteadas[contadorRotina] = posicao;
                    vencedores[contadorRotina] = participantes[posicao].Nome_Participante;
                    dadosParticipantesSorteio.QtdeItens -= 1;
                    _sorteioParticipanteAppService.VencedorParticipantesSorteio(idSorteio, participantes[posicao].Id_Participante);
                    contadorRotina++;
                }
            } while (dadosParticipantesSorteio.QtdeItens > 0);

            var getVencedores = new SorteioViewModel { Nome = dadosParticipantesSorteio.Nome };
            foreach (var vencedor in vencedores)
            {
                getVencedores.SorteioParticipantes.Add(new SorteioParticipanteViewModel { Nome_Participante = vencedor });
            }

            return View(getVencedores);
        }

        public ActionResult Winners(int idSorteio)
        {
            var vencedores = _sorteioParticipanteAppService.GetVencedores(idSorteio);
            if (!vencedores.IsSuccessStatusCode)
                return ErrorMessage("Erro ao buscar vencedores");

            var sorteio = JsonConvert.DeserializeObject<IEnumerable<SorteioParticipanteViewModel>>(vencedores.Content.ReadAsStringAsync().Result);
            return View("Winners", sorteio);
        }

        public ActionResult WinnersUsuarios(int idSorteio)
        {
            var vencedores = _sorteioParticipanteAppService.GetVencedores(idSorteio);
            if (!vencedores.IsSuccessStatusCode)
                return ErrorMessage("Erro ao buscar vencedores");

            var sorteio = JsonConvert.DeserializeObject<IEnumerable<SorteioParticipanteViewModel>>(vencedores.Content.ReadAsStringAsync().Result);
            return View("WinnersUsers", sorteio);
        }
    }
}
