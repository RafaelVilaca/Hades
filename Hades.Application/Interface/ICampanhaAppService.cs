using Hades.Domain.Entities;
using System.Net.Http;

namespace Hades.Application.Interface
{
    public interface ICampanhaAppService
    {
        HttpResponseMessage GetCampanhas(int idUsuario);
        HttpResponseMessage GetCampanha(int idCampanha);
        HttpResponseMessage Post(CampanhaDto campanha);
        HttpResponseMessage Put(CampanhaDto campanha);
        HttpResponseMessage Delete(int idCampanha);
    }
}
