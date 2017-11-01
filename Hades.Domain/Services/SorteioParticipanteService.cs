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

        public void Participar(int idSorteio, int idUsuario)
        {
            _sorteioParticipanteRepository.Participar(idSorteio, idUsuario);
        }

        public IEnumerable<SorteioParticipante> GetAll(int id)
        {
            return _sorteioParticipanteRepository.GetAll(id);
        }

        public void VencedorParticipantesSorteio(int idSorteio, int idUsuario)
        {
            _sorteioParticipanteRepository.VencedorParticipantesSorteio(idSorteio, idUsuario);
        }

        public void SortearNovamente(int idSorteio)
        {
            _sorteioParticipanteRepository.SortearNovamente(idSorteio);
        }

        public IEnumerable<SorteioParticipante> GetVencedores(int idSorteio)
        {
            return _sorteioParticipanteRepository.GetVencedores(idSorteio);
        } 

        public void DeletarParticipantesSorteio(int idSorteio, int idUsuario)
        {
            _sorteioParticipanteRepository.DeletarParticipantesSorteio(idSorteio, idUsuario);
        }
    }
}
