using Hades.Application.Interface;
using Hades.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Hades.Web.Controllers
{
    public class CampanhaParticipanteController : BaseController
    {
        private readonly ICampanhaParticipanteAppService _campanhaParticipanteAppService;

        public CampanhaParticipanteController(ICampanhaParticipanteAppService campanhaParticipanteAppService)
        {
            _campanhaParticipanteAppService = campanhaParticipanteAppService;
        }

        public ActionResult Participar(int idCampanha, int idUsuario)
        {
            var response = _campanhaParticipanteAppService.Post(idUsuario, idCampanha);
            if (!response.IsSuccessStatusCode)
                return ErrorMessage("Erro ao ativar participante, tente novamente");

            return Json("Sucesso, participante incluido com sucesso!", JsonRequestBehavior.AllowGet);
        }

        public IEnumerable<CampanhaParticipanteViewModel> Get(int idCampanha)
        {
            var response = _campanhaParticipanteAppService.Get(idCampanha);
            var participantes =
                JsonConvert.DeserializeObject<IEnumerable<CampanhaParticipanteViewModel>>(response.Content.ReadAsStringAsync().Result);
            return participantes;
        }

        public ActionResult DeletarParticipante(int idCampanha, int idUsuario)
        {
            var response = _campanhaParticipanteAppService.Delete(idUsuario, idCampanha);
            if (!response.IsSuccessStatusCode)
                return ErrorMessage("Erro ao alterar o usuario");
            return Json("Sucesso, participante excluido com sucesso!", JsonRequestBehavior.AllowGet);
        }
    }
}