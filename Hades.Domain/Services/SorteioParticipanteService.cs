using System.Collections.Generic;
using Hades.Domain.Entities;
using Hades.Domain.Interfaces.Repositories;
using Hades.Domain.Interfaces.Services;

namespace Hades.Domain.Services
{
    public class SorteioParticipanteService : ISorteioParticipanteService
    {
        private readonly ISorteioParticipanteRepository _sorteioParticipanteRepository;

        public SorteioParticipanteService(ISorteioParticipanteRepository sorteioParticipanteRepository)
        {
            _sorteioParticipanteRepository = sorteioParticipanteRepository;
        }

        public void Participar(SorteioParticipante sorteioParticipante)
        {
            _sorteioParticipanteRepository.Participar(sorteioParticipante);
        }

        public IEnumerable<SorteioParticipante> GetAll(int id)
        {
            return _sorteioParticipanteRepository.GetAll(id);
        }
    }
}
