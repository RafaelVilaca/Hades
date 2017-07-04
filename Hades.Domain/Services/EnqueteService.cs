using System.Collections.Generic;
using Hades.Domain.Entities;
using Hades.Domain.Interfaces.Repositories;
using Hades.Domain.Interfaces.Services;

namespace Hades.Domain.Services
{
    public class EnqueteService : IEnqueteService
    {
        private readonly IEnqueteRepository _enqueteRepository;

        public EnqueteService(IEnqueteRepository enqueteRepository)
        {
            _enqueteRepository = enqueteRepository;
        }

        public string Post(Enquete enquete)
        {
            if (enquete.IsValid() != 0)
                switch (enquete.IsValid())
                {
                    case 1: return "Título deve conter de 5 à 20 caracteres";
                    case 2: return "Assunto deve conter de 10 à 150 caracteres";
                }

            _enqueteRepository.Post(enquete);
            return string.Empty;
        }

        public Enquete GetById(int id)
        {
            return _enqueteRepository.GetById(id);
        }

        public IEnumerable<Enquete> GetAll()
        {
            return _enqueteRepository.GetAll();
        }

        public string Put(Enquete enquete)
        {
            if (enquete.IsValid() != 0)
            {
                switch (enquete.IsValid())
                {
                    case 1: return "Título deve conter de 5 à 20 caracteres";
                    case 2: return "Assunto deve conter de 10 à 150 caracteres";
                }
            }

            _enqueteRepository.Put(enquete);
            return string.Empty;
        }

        public void StatusEnquete(int id)
        {
            _enqueteRepository.StatusEnquete(id);
        }
    }
}
