using System.Net.Http;
using Hades.Domain.Entities;

namespace Hades.Application.Interface
{
    public interface IEnqueteAppService
    {
        HttpResponseMessage Post(EnqueteDto enquete);
        HttpResponseMessage GetById(int id);
        HttpResponseMessage GetAll();
        HttpResponseMessage Put(EnqueteDto enquete);
        HttpResponseMessage StatusEnquete(int id);
    }
}
