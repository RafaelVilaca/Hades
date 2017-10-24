using Hades.Domain.Entities;
using Hades.Domain.Interfaces.Repositories;
using Hades.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace Hades.Domain.Services
{
    public class SorteioService : ISorteioService
    {
        private readonly ISorteioRepository _sorteioRepository;

        public SorteioService(ISorteioRepository sorteioRepository)
        {
            _sorteioRepository = sorteioRepository;
        }

        public string Post(Sorteio sorteio)
        {
            _sorteioRepository.Post(sorteio);
            return string.Empty;
        }

        public Sorteio GetById(int id)
        {
            return _sorteioRepository.GetById(id);
        }

        public IEnumerable<Sorteio> GetAll()
        {
            return _sorteioRepository.GetAll();
        }

        public string Put(Sorteio sorteio)
        {
            _sorteioRepository.Put(sorteio);
            return string.Empty;
        }

        public void Delete(int id)
        {
            _sorteioRepository.Delete(id);
        }
    }
}
