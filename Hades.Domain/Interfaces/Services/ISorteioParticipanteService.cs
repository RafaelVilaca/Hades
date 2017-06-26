using System.Collections.Generic;
using Hades.Domain.Entities;

namespace Hades.Domain.Interfaces.Services
{
    public interface ISorteioParticipanteService
    {
        void Participar(SorteioParticipante sorteioParticipante);
        IEnumerable<SorteioParticipante> GetAll(int id);
    }
}
