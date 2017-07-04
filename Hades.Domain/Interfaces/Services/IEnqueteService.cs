using System.Collections.Generic;
using Hades.Domain.Entities;

namespace Hades.Domain.Interfaces.Services
{
    public interface IEnqueteService
    {
        string Post(Enquete enquete);
        Enquete GetById(int id);
        IEnumerable<Enquete> GetAll();
        string Put(Enquete enquete);
        void StatusEnquete(int id);
    }
}