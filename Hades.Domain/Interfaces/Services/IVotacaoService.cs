﻿using System.Collections.Generic;
using Hades.Domain.Entities;

namespace Hades.Domain.Interfaces.Services
{
    public interface IVotacaoService
    {
        string Post(Votacao votacao);
    }
}
