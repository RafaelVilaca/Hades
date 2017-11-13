using Hades.Domain.Entities;
using System.Net.Http;

namespace Hades.Application.Interface
{
    public interface ICampanhaParticipanteAppService
    {
        HttpResponseMessage Get(int idCampanha);
        HttpResponseMessage Post(CampanhaDto campanha);
        HttpResponseMessage Delete(int idUsuario, int idCampanha);
    }
}
