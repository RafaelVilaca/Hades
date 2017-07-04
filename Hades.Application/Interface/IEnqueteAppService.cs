using System.Net.Http;
using Hades.Domain.Entities;

namespace Hades.Application.Interface
{
    public interface IEnqueteAppService
    {
        HttpResponseMessage Post(Enquete enquete);
        HttpResponseMessage GetById(int id);
        HttpResponseMessage GetAll();
        HttpResponseMessage Put(Enquete enquete);
        HttpResponseMessage StatusEnquete(int id);
    }
}
