using Hades.Domain.Entities;
using System.Collections.Generic;

namespace Hades.Domain.Interfaces.Services
{
    public interface ICampanhaParticipanteService
    {
        IEnumerable<CampanhaParticipantesDto> Get(int campanha);
        void Post(int idUsuario, int idCampanha);
        void Delete(int idUsuario, int idCampanha);
    }
}
