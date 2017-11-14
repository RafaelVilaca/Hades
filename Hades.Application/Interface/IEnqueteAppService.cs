using Hades.Domain.Entities;
using System.Net.Http;

namespace Hades.Application.Interface
{
    public interface IEnqueteAppService
    {
        HttpResponseMessage Post(EnqueteDto enquete);
        HttpResponseMessage GetById(int id);
        HttpResponseMessage GetAll();
        HttpResponseMessage GetTodasEnquetes();
        HttpResponseMessage Put(EnqueteDto enquete);
        HttpResponseMessage StatusEnquete(int id);
    }
}
