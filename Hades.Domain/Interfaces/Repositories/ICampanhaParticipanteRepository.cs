using Hades.Domain.Entities;
using System.Collections.Generic;

namespace Hades.Domain.Interfaces.Repositories
{
    public interface ICampanhaParticipanteRepository
    {
        IEnumerable<CampanhaParticipantesDto> GetParticipantesCampanha(int campanha);
        void InsParticipanteCampanha(int idUsuario, int idCampanha);
        void DelParticipanteCampanha(int idUsuario, int idCampanha);
    }
}
