using Hades.Application.Interface;
using Hades.Domain.Entities;
using Hades.Web.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

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
            ViewBag.NomeUsuario = Session["Nome"];
            ViewBag.Adm = Session["Administrador"];
            return View();
        }

        public ActionResult BuscaGridEnquetes()
        {
            var usuarioViewModel = _enqueteAppService.GetAll();
            if (!usuarioViewModel.IsSuccessStatusCode)
                return ErrorMessage("Erro ao trazer lista de enquetes.");

            var enquetes = JsonConvert.DeserializeObject<IEnumerable<EnqueteViewModel>>(usuarioViewModel.Content.ReadAsStringAsync().Result);

            return View("_TabelaEnquetes", enquetes);
        }

        public ActionResult BuscaGridTodasEnquetes()
        {
            var usuarioViewModel = _enqueteAppService.GetTodasEnquetes();
            if (!usuarioViewModel.IsSuccessStatusCode)
                return ErrorMessage("Erro ao trazer lista de enquetes.");

            var enquetes = JsonConvert.DeserializeObject<IEnumerable<EnqueteViewModel>>(usuarioViewModel.Content.ReadAsStringAsync().Result);

            return View("_TabelaEnquetes", enquetes);
        }

        // GET: Enquete/Details/5
        public ActionResult Details(int id)
        {
            var enquete = _enqueteAppService.GetById(id);
            if (!enquete.IsSuccessStatusCode)
                return ErrorMessage("Erro ao buscar enquete");

            var mostraEnquete =
                JsonConvert.DeserializeObject<EnqueteViewModel>(enquete.Content.ReadAsStringAsync().Result);

            return View(mostraEnquete);
        }

        // GET: Enquete/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Enquete/Create
        public ActionResult CreateConfirmed(EnqueteDto enquete)
        {
            var request = _enqueteAppService.GetAll();
            if (!request.IsSuccessStatusCode)
                return ErrorMessage("Erro ao verificar se usuário já existe");

            var enqueteViewModel = JsonConvert.DeserializeObject<IEnumerable<EnqueteViewModel>>(request.Content.ReadAsStringAsync().Result);

            var verifica = enqueteViewModel.Select(x => x.Assunto.Contains(enquete.Assunto)).ToString();

            if (string.IsNullOrEmpty(verifica))
                return ErrorMessage("Enquete já existe, insira outro título.");

            var response = _enqueteAppService.Post(enquete);
            if (!response.IsSuccessStatusCode)
                ErrorMessage("Erro ao criar enquete");

            return Json("Cadastro Efetuado com sucesso");
        }

        // GET: Enquete/Edit/5
        public ActionResult Edit(int id)
        {
            var response = _enqueteAppService.GetById(id);
            if (!response.IsSuccessStatusCode)
                return ErrorMessage("Erro ao buscar enquete.");
            var enquete = JsonConvert.DeserializeObject<EnqueteViewModel>(response.Content.ReadAsStringAsync().Result);
            return View(enquete);
        }

        // POST: Enquete/Edit/5
        public ActionResult EditConfirmed(EnqueteDto enquete)
        {
            //Valor com . precisa conter o ModelBinder, senão, ele nao salvará corretamente
            var response = _enqueteAppService.Put(enquete);
            if (response.IsSuccessStatusCode)
                return Json("Enquete Atualizada com sucesso");
            return ErrorMessage($"Erro ao editar enquete: {response.Content.ReadAsStringAsync().Result}");
        }

        public ActionResult DesativarEnquete(int id)
        {
            var response = _enqueteAppService.StatusEnquete(id);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("BuscaGridEnquetes");
            return ErrorMessage("Erro ao desativar Enquete");

        }

        public ActionResult GetVotacao(Votacao votacao)
        {
            var response = _votacaoAppService.GetVotos(votacao.IdUsuario, votacao.IdEnquete);

            if (!response.IsSuccessStatusCode)
                return ErrorMessage("Não foi possível efetuar votação. Faça o Log In novamente!");

            var voto = JsonConvert.DeserializeObject<Votacao>(response.Content.ReadAsStringAsync().Result);

            if (voto == null)
                voto = new Votacao { Enquete = votacao.Enquete, IdEnquete = votacao.IdEnquete, IndicadorCadastro = "S" };
            else
            {
                voto.Enquete = votacao.Enquete;
                voto.IdEnquete = votacao.IdEnquete;
                voto.IndicadorCadastro = "N";
            }

            return RedirectToAction("Create", "Votacao", voto);
        }
    }
}
