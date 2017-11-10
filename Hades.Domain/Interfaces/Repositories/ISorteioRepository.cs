using Hades.Domain.Entities;
using System.Collections.Generic;

namespace Hades.Domain.Interfaces.Repositories
{
    public interface ISorteioRepository
    {
        void Post(SorteioDto sorteio);
        SorteioDto GetById(int id);
        IEnumerable<SorteioDto> GetAll(int idUsuario);
        void Put(SorteioDto sorteio);
        void Delete(int id);
    }
}
