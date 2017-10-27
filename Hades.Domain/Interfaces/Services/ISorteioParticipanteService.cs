using Hades.Domain.Entities;
using System.Collections.Generic;

namespace Hades.Domain.Interfaces.Services
{
    public interface ISorteioParticipanteService
    {
        void Participar(int idSorteio, int idUsuario);
        IEnumerable<SorteioParticipante> GetAll(int id);
        void VencedorParticipantesSorteio(int idSorteio, int idUsuario);
        void DeletarParticipantesSorteio(int idSorteio, int idUsuario);
    }
}
