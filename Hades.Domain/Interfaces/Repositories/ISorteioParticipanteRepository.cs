using System.Collections.Generic;
using Hades.Domain.Entities;

namespace Hades.Domain.Interfaces.Repositories
{
    public interface ISorteioParticipanteRepository
    {
        void Participar(SorteioParticipante sorteioParticipante);
        IEnumerable<SorteioParticipante> GetAll(int id);
        void DeletarParticipantesSorteio(int id);
    }
}
