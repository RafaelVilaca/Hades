using Hades.Domain.Entities;
using System.Net.Http;

namespace Hades.Application.Interface
{
    public interface ISorteioAppService
    {
        HttpResponseMessage Post(SorteioDto sorteio);
        HttpResponseMessage GetById(int id);
        HttpResponseMessage GetAll(int idUsuario);
        HttpResponseMessage GetTodosSorteios(int idUsuario);
        HttpResponseMessage Put(SorteioDto sorteio);
        HttpResponseMessage Delete(int id);
    }
}
