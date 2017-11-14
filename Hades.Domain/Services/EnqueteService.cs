using Hades.Domain.Entities;
using Hades.Domain.Interfaces.Repositories;
using Hades.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace Hades.Domain.Services
{
    public class EnqueteService : IEnqueteService
    {
        private readonly IEnqueteRepository _enqueteRepository;

        public EnqueteService(IEnqueteRepository enqueteRepository)
        {
            _enqueteRepository = enqueteRepository;
        }

        public string Post(EnqueteDto enquete)
        {
            _enqueteRepository.Post(enquete);
            return string.Empty;
        }

        public EnqueteDto GetById(int id)
        {
            return _enqueteRepository.GetById(id);
        }

        public IEnumerable<EnqueteDto> GetAll()
        {
            return _enqueteRepository.GetAll();
        }

        public IEnumerable<EnqueteDto> GetTodasEnquetes()
        {
            return _enqueteRepository.GetTodasEnquetes();
        }

        public string Put(EnqueteDto enquete)
        {
            _enqueteRepository.Put(enquete);
            return string.Empty;
        }

        public void StatusEnquete(int id)
        {
            _enqueteRepository.StatusEnquete(id);
        }
    }
}
