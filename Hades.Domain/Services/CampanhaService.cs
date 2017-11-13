using Hades.Domain.Entities;
using Hades.Domain.Interfaces.Repositories;
using Hades.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace Hades.Domain.Services
{
    public class CampanhaService : ICampanhaService
    {
        private readonly ICampanhaRepository _campanhaRepository;

        public CampanhaService(ICampanhaRepository campanhaRepository)
        {
            _campanhaRepository = campanhaRepository;
        }
        
        public IEnumerable<CampanhaDto> GetCampanhas(int idUsuario)
        {
            return _campanhaRepository.GetCampanhas(idUsuario);
        }

        public CampanhaDto GetCampanha(int idCampanha)
        {
            return _campanhaRepository.GetCampanha(idCampanha);
        }

        public void Post(CampanhaDto campanha) => _campanhaRepository.InsCampanha(campanha);

        public void Put(CampanhaDto campanha) => _campanhaRepository.UpdCampanha(campanha);

        public void Delete(int idCampanha) => _campanhaRepository.DelCampanha(idCampanha);
    }
}
