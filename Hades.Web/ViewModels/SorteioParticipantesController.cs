using System.Collections.Generic;
using System.Web.Mvc;
using Hades.Application.Interface;
using Hades.Domain.Entities;
using Newtonsoft.Json;

namespace Hades.Web.ViewModels
{
    public class SorteioParticipantesController : Controller
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
    }
}
