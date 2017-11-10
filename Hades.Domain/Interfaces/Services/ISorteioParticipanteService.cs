using Hades.Domain.Entities;
using System.Collections.Generic;

namespace Hades.Domain.Interfaces.Services
{
    public interface ISorteioParticipanteService
    {
        void Participar(int idSorteio, int idUsuario);
        IEnumerable<SorteioParticipanteDto> GetAll(int id);
        void VencedorParticipantesSorteio(int idSorteio, int idUsuario);
        void SortearNovamente(int idSorteio);
        IEnumerable<SorteioParticipanteDto> GetVencedores(int idSorteio);
        void DeletarParticipantesSorteio(int idSorteio, int idUsuario);
    }
}
