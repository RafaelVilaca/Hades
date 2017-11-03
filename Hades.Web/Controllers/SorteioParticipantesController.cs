using Hades.Application.Interface;
using Hades.Web.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Hades.Web.Controllers
{
    public class SorteioParticipantesController : BaseController
    {
        private readonly ISorteioParticipanteAppService _sorteioParticipanteAppService;

        public SorteioParticipantesController(ISorteioParticipanteAppService sorteioParticipanteAppService)
        {
            _sorteioParticipanteAppService = sorteioParticipanteAppService;
        }

        // GET: SorteioParticipantes
        public ActionResult Participar(int idSorteio, int idUsuario)
        {
            var response = _sorteioParticipanteAppService.Participar(idSorteio, idUsuario);
            if (!response.IsSuccessStatusCode)
                return ErrorMessage("Erro ao ativar participante, tente novamente");

            return Json("Sucesso, participante incluido com sucesso!", JsonRequestBehavior.AllowGet);
        }

        // GET: SorteioParticipantes/Create
        public IEnumerable<SorteioParticipanteViewModel> GetAll(int id)
        {
            var response = _sorteioParticipanteAppService.GetAll(id);
            var participantes =
                JsonConvert.DeserializeObject<IEnumerable<SorteioParticipanteViewModel>>(response.Content.ReadAsStringAsync().Result);
            return participantes;
        }

        public ActionResult DeletarParticipante(int idSorteio, int idUsuario)
        {
            var response = _sorteioParticipanteAppService.DeletarParticipantesSorteio(idSorteio, idUsuario);
            if (!response.IsSuccessStatusCode)
                return ErrorMessage("Erro ao alterar o usuario");
            return Json("Sucesso, participante excluido com sucesso!", JsonRequestBehavior.AllowGet);
        }

    }
}
