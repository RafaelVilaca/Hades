using Hades.Domain.Entities;
using System.Collections.Generic;

namespace Hades.Domain.Interfaces.Services
{
    public interface ICampanhaService
    {
        IEnumerable<CampanhaDto> GetCampanhas(int idUsuario);
        CampanhaDto GetCampanha(int idCampanha);
        void Post(CampanhaDto campanha);
        void Put(CampanhaDto campanha);
        void Delete(int idCampanha);
    }
}
