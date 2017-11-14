using Hades.Domain.Entities;
using System.Collections.Generic;

namespace Hades.Domain.Interfaces.Repositories
{
    public interface IEnqueteRepository
    {
        void Post(EnqueteDto enquete);
        EnqueteDto GetById(int id);
        IEnumerable<EnqueteDto> GetAll();
        IEnumerable<EnqueteDto> GetTodasEnquetes();
        void Put(EnqueteDto enquete);
        void StatusEnquete(int id);
    }
}
