using Hades.Domain.Entities;
using Hades.Domain.Interfaces.Repositories;
using Hades.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace Hades.Domain.Services
{
    public class CampanhaParticipanteService : ICampanhaParticipanteService
    {
        private readonly ICampanhaParticipanteRepository _campanhaParticipanteRepository;

        public CampanhaParticipanteService(ICampanhaParticipanteRepository campanhaParticipanteRepository)
        {
            _campanhaParticipanteRepository = campanhaParticipanteRepository;
        }

        public IEnumerable<CampanhaParticipantesDto> Get(int campanha)
        {
            return _campanhaParticipanteRepository.GetParticipantesCampanha(campanha);
        }

        public void Post(int idUsuario, int idCampanha) => _campanhaParticipanteRepository.InsParticipanteCampanha(idUsuario, idCampanha);

        public void Delete(int idUsuario, int idCampanha) => _campanhaParticipanteRepository.DelParticipanteCampanha(idUsuario, idCampanha);
    }
}
