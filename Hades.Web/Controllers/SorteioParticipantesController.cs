using Hades.Application.Interface;
using Hades.Domain.Entities;
using Hades.Web.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Hades.Web.ViewModels
{
    public class SorteioParticipantesController : BaseController
    {
        private readonly ISorteioParticipanteAppService _sorteioParticipanteAppService;

        public SorteioParticipantesController(ISorteioParticipanteAppService sorteioParticipanteAppService)
        {
            _sorteioParticipanteAppService = sorteioParticipanteAppService;
        }

        // GET: SorteioParticipantes
        public ActionResult Participar(SorteioParticipante sorteio)
        {
            _sorteioParticipanteAppService.Participar(sorteio);
            return RedirectToAction("Index", "Sorteio");
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
            try
            {
                var response = _sorteioParticipanteAppService.DeletarParticipantesSorteio(idSorteio, idUsuario);
                if (!response.IsSuccessStatusCode)
                    return ErrorMessage("Erro ao alterar o usuario");
                return Json("OK");
            }
            catch (Exception e)
            {

                return ErrorMessage($"Falha ao deletar participante, {e.Message}");
            }
        }
    }
}
