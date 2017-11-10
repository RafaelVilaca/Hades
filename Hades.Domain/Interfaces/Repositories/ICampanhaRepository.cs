﻿using Hades.Domain.Entities;
using System.Collections.Generic;

namespace Hades.Domain.Interfaces.Repositories
{
    public interface ICampanhaRepository
    {
        IEnumerable<CampanhaDto> GetCampanhas();
        CampanhaDto GetCampanha(int idCampanha);
        void InsCampanha(CampanhaDto campanha);
        void UpdCampanha(CampanhaDto campanha);
        void DelCampanha(int idCampanha);
    }
}
