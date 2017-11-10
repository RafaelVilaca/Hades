﻿using Hades.Domain.Entities;
using System.Collections.Generic;

namespace Hades.Domain.Interfaces.Repositories
{
    public interface ISorteioParticipanteRepository
    {
        void Participar(int idSorteio, int idUsuario);
        IEnumerable<SorteioParticipanteDto> GetAll(int id);
        void DeletarParticipantesSorteio(int idSorteio, int idUsuario);
        void VencedorParticipantesSorteio(int idSorteio, int idUsuario);
        void SortearNovamente(int idSorteio);
        IEnumerable<SorteioParticipanteDto> GetVencedores(int idSorteio);
    }
}
