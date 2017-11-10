using System.Net.Http;
using Hades.Domain.Entities;

namespace Hades.Application.Interface
{
    public interface ISorteioAppService
    {
        HttpResponseMessage Post(SorteioDto sorteio);
        HttpResponseMessage GetById(int id);
        HttpResponseMessage GetAll(int idUsuario);
        HttpResponseMessage Put(SorteioDto sorteio);
        HttpResponseMessage Delete(int id);
    }
}
