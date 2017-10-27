using Hades.Domain.Entities;
using System.Collections.Generic;

namespace Hades.Domain.Interfaces.Repositories
{
    public interface ISorteioParticipanteRepository
    {
        void Participar(int idSorteio, int idUsuario);
        IEnumerable<SorteioParticipante> GetAll(int id);
        void DeletarParticipantesSorteio(int idSorteio, int idUsuario);
        void VencedorParticipantesSorteio(int idSorteio, int idUsuario);
    }
}
