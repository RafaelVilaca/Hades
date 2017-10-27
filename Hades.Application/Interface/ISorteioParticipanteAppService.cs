using System.Net.Http;

namespace Hades.Application.Interface
{
    public interface ISorteioParticipanteAppService
    {
        HttpResponseMessage Participar(int idSorteio, int idUsuario);
        HttpResponseMessage GetAll(int id);
        HttpResponseMessage DeletarParticipantesSorteio(int idSorteio, int idUsuario);
        HttpResponseMessage VencedorParticipantesSorteio(int idSorteio, int idUsuario);
    }
}
