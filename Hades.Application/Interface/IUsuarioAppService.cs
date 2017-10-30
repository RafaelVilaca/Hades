using Hades.Domain.Entities;
using System.Net.Http;

namespace Hades.Application.Interface
{
    public interface IUsuarioAppService
    {
        HttpResponseMessage Post(Usuario usuario);
        HttpResponseMessage GetById(int id);
        HttpResponseMessage GetAll();
        HttpResponseMessage Put(Usuario usuario);
        HttpResponseMessage StatusUsuario(int id, bool status);
        HttpResponseMessage GetByName(string nome);
        HttpResponseMessage SenhaFormatada(string senha);
    }
}
