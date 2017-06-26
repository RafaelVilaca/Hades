using System.Collections.Generic;
using Hades.Domain.Entities;

namespace Hades.Domain.Interfaces.Repositories
{
    public interface IEnqueteRepository
    {
        void Post(Enquete enquete);
        Enquete GetById(int id);
        IEnumerable<Enquete> GetAll();
        void Put(Enquete enquete);
        void StatusEnquete(int id, bool status);
    }
}
