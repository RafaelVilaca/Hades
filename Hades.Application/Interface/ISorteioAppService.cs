using System.Net.Http;
using Hades.Domain.Entities;

namespace Hades.Application.Interface
{
    public interface ISorteioAppService
    {
        HttpResponseMessage Post(Sorteio sorteio);
        HttpResponseMessage GetById(int id);
        HttpResponseMessage GetAll();
        HttpResponseMessage Put(Sorteio sorteio);
        HttpResponseMessage Delete(int id);
    }
}
