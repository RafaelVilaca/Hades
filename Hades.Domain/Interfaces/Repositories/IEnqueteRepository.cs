using System.Collections.Generic;
using Hades.Domain.Entities;

namespace Hades.Domain.Interfaces.Repositories
{
    public interface IEnqueteRepository
    {
        void Post(EnqueteDto enquete);
        EnqueteDto GetById(int id);
        IEnumerable<EnqueteDto> GetAll();
        void Put(EnqueteDto enquete);
        void StatusEnquete(int id);
    }
}
