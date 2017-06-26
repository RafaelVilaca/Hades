using System.Net.Http;
using Hades.Domain.Entities;

namespace Hades.Application.Interface
{
    public interface ISorteioParticipanteAppService
    {
        void Participar(SorteioParticipante sorteioParticipante);
        HttpResponseMessage GetAll(int id);
    }
}
