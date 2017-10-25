using Hades.Domain.Entities;
using System.Collections.Generic;

namespace Hades.Domain.Interfaces.Repositories
{
    public interface ISorteioParticipanteRepository
    {
        void Participar(SorteioParticipante sorteioParticipante);
        IEnumerable<SorteioParticipante> GetAll(int id);
        void DeletarParticipantesSorteio(int idSorteio, int idUsuario);
        void VencedorParticipantesSorteio(int idSorteio, int idUsuario);
    }
}
