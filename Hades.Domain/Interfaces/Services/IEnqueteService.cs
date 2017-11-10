using System.Collections.Generic;
using Hades.Domain.Entities;

namespace Hades.Domain.Interfaces.Services
{
    public interface IEnqueteService
    {
        string Post(EnqueteDto enquete);
        EnqueteDto GetById(int id);
        IEnumerable<EnqueteDto> GetAll();
        string Put(EnqueteDto enquete);
        void StatusEnquete(int id);
    }
}