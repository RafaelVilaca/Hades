using Hades.Domain.Entities;
using System.Collections.Generic;

namespace Hades.Domain.Interfaces.Services
{
    public interface ISorteioService
    {
        string Post(SorteioDto sorteio);
        SorteioDto GetById(int id);
        IEnumerable<SorteioDto> GetAll(int idUsuario);
        IEnumerable<SorteioDto> GetTodosSorteios(int idUsuario);
        string Put(SorteioDto sorteio);
        void Delete(int id);
    }
}
