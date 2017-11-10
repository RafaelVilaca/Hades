using Hades.Application;
using Hades.Application.Interface;
using Hades.Domain.Entities;
using Hades.Web.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;

namespace Hades.Web.Controllers
{
    public class SorteioController : BaseController
    {
        private readonly ISorteioAppService _sorteioAppService;
        private readonly ISorteioParticipanteAppService _sorteioParticipanteAppService;
        private readonly IUsuarioAppService _usuarioAppService;

        public SorteioController(ISorteioAppService sorteioAppService, ISorteioParticipanteAppService sorteioParticipanteAppService, UsuarioAppService usuarioAppService)
        {
            _sorteioAppService = sorteioAppService;
            _sorteioParticipanteAppService = sorteioParticipanteAppService;
            _usuarioAppService = usuarioAppService;
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

        public ActionResult CreateConfirmed(SorteioDto sorteio)
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

        public ActionResult EditConfirmed(SorteioDto sorteio)
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
                {
                    _sorteioParticipanteAppService.VencedorParticipantesSorteio(idSorteio, vencedor.Id_Participante);

                    var sorteado = _usuarioAppService.GetByName(vencedor.Nome_Participante);
                    var sorteadoEmail = JsonConvert.DeserializeObject<UsuarioViewModel>(sorteado.Content.ReadAsStringAsync().Result);

                    EnviarEmail(sorteadoEmail.Email, sorteadoEmail.Nome, dadosParticipantesSorteio.Nome, dadosParticipantesSorteio.NomeCriador);
                }
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

                    var sorteado = _usuarioAppService.GetByName(participantes[posicao].Nome_Participante);
                    var sorteadoEmail = JsonConvert.DeserializeObject<UsuarioViewModel>(sorteado.Content.ReadAsStringAsync().Result);

                    EnviarEmail(sorteadoEmail.Email, sorteadoEmail.Nome, dadosParticipantesSorteio.Nome, participantes[posicao].Sorteio.NomeCriador);
                }
            } while (dadosParticipantesSorteio.QtdeItens > 0);

            var getVencedores = new SorteioViewModel { Nome = dadosParticipantesSorteio.Nome };
            foreach (var vencedor in vencedores)
            {
                getVencedores.SorteioParticipantes.Add(new SorteioParticipanteViewModel { Nome_Participante = vencedor });
            }

            return View(getVencedores);
        }

        //private void EnviarEmail(string email, string nomeGanhador, string nomSorteio, string nomeCriadorSorteio)
        //{
        //    var client = new SmtpClient("smtp.gmail.com", 587)
        //    {
        //        Host = "smtp.gmail.com",
        //        EnableSsl = true,
        //        Credentials = new System.Net.NetworkCredential("hades.suporte.2017@gmail.com", "hades2017")
        //    };

        //    var mail = new MailMessage
        //    {
        //        Sender = new MailAddress($"{email}", $"{nomeGanhador}"),
        //        From = new MailAddress("hades.suporte.2017@gmail.com", "HADES")
        //    };

        //    mail.To.Add(new MailAddress($"{email}", $"{nomeGanhador}"));
        //    mail.Subject = "Contato Sorteio";
        //    mail.Body = $"Mensagem do site:<br/> HADES<br/><br/> Mensagem : <br>   Parabéns {nomeGanhador}, você acabou de ganhar no sorteio: {nomSorteio}, " +
        //        $"um(a) ótimo(a) item, entre em contato com o {nomeCriadorSorteio}, e resgate seu prêmio." +
        //        "<br/><br/><br/><br/><br/><br/><br/> Atenciosamente, Equipe HADES.";
        //    mail.IsBodyHtml = true;

        //    client.Send(mail);
        //}

        private void EnviarEmail(string email, string nomeGanhador, string nomSorteio, string nomeCriadorSorteio)
        {
            var body = $"Mensagem do site:<br/> HADES<br/><br/> Mensagem : <br>   Parabéns {nomeGanhador}, você acabou de ganhar no sorteio: {nomSorteio}, " +
               $"um(a) ótimo(a) item, entre em contato com o {nomeCriadorSorteio}, e resgate seu prêmio." +
               "<br/><br/><br/><br/><br/><br/> Atenciosamente,<br/> Equipe HADES.";

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new System.Net.NetworkCredential("hades.suporte.2017@gmail.com", "hades2017")
            };
            client.Send(new MailMessage(new MailAddress($"{email}", "HADES"), new MailAddress($"{email}", $"{nomeGanhador}"))
            {
                Subject = "Contato Sorteio",
                Body = body,
                IsBodyHtml = true
            });
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
