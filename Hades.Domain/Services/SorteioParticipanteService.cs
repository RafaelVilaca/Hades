using Hades.Domain.Entities;
using Hades.Domain.Interfaces.Repositories;
using Hades.Domain.Interfaces.Services;
using System.Collections.Generic;

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

        public void VencedorParticipantesSorteio(int idSorteio, int idUsuario)
        {
            _sorteioParticipanteRepository.VencedorParticipantesSorteio(idSorteio, idUsuario);
        }

        public void DeletarParticipantesSorteio(int idSorteio, int idUsuario)
        {
            _sorteioParticipanteRepository.DeletarParticipantesSorteio(idSorteio, idUsuario);
        }
    }
}
