using Hades.Domain.Entities;
using System.Collections.Generic;

namespace Hades.Domain.Interfaces.Services
{
    public interface IEnqueteService
    {
        string Post(EnqueteDto enquete);
        EnqueteDto GetById(int id);
        IEnumerable<EnqueteDto> GetAll();
        IEnumerable<EnqueteDto> GetTodasEnquetes();
        string Put(EnqueteDto enquete);
        void StatusEnquete(int id);
    }
}