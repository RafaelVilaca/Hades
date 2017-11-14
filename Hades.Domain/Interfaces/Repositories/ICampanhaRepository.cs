using Hades.Domain.Entities;
using System.Collections.Generic;

namespace Hades.Domain.Interfaces.Repositories
{
    public interface ICampanhaRepository
    {
        IEnumerable<CampanhaDto> GetCampanhas(int idUsuario);
        IEnumerable<CampanhaDto> GetTodasCampanhas(int idUsuario);
        CampanhaDto GetCampanha(int idCampanha);
        void InsCampanha(CampanhaDto campanha);
        void UpdCampanha(CampanhaDto campanha);
        void DelCampanha(int idCampanha);
    }
}
