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

        public string Post(SorteioDto sorteio)
        {
            _sorteioRepository.Post(sorteio);
            return string.Empty;
        }

        public SorteioDto GetById(int id)
        {
            return _sorteioRepository.GetById(id);
        }

        public IEnumerable<SorteioDto> GetAll(int idUsuario)
        {
            return _sorteioRepository.GetAll(idUsuario);
        }

        public IEnumerable<SorteioDto> GetTodosSorteios(int idUsuario)
        {
            return _sorteioRepository.GetTodosSorteios(idUsuario);
        }

        public string Put(SorteioDto sorteio)
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
