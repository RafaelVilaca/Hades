using System.Net.Http;

namespace Hades.Application.Interface
{
    public interface ICampanhaParticipanteAppService
    {
        HttpResponseMessage Get(int idCampanha);
        HttpResponseMessage Post(int idUsuario, int idCampanha);
        HttpResponseMessage Delete(int idUsuario, int idCampanha);
    }
}
