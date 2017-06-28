using System;
using System.Collections.Generic;
using Hades.Domain.Entities;
using Hades.Domain.Interfaces.Repositories;
using Hades.Infra.Data.Context;

namespace Hades.Infra.Data.Repositories
{
    public class SorteioParticipanteRepository : BaseRepository, ISorteioParticipanteRepository
    {
        private enum Procedures
        {
            sp_GetParticipantes,
            sp_AddParticipante,
            sp_DeletarParticipantesSorteio
        }

        public void Participar(SorteioParticipante sorteioParticipante)
        {
            ExecuteProcedure(Procedures.sp_AddParticipante);
            AddParameter("@UsuarioId", sorteioParticipante.Usuario.Id);
            AddParameter("@SorteioId", sorteioParticipante.Sorteio.Id);
            ExecuteNonQuery();
        }

        public IEnumerable<SorteioParticipante> GetAll(int id)
        {
            ExecuteProcedure(Procedures.sp_GetParticipantes);
            AddParameter("@Id", id);
            var sorteios = new List<SorteioParticipante>();
            using (var r = ExecuteReader())
                while (r.Read())
                    sorteios.Add(new SorteioParticipante
                    {
                        NomeUsuario = r.GetString(r.GetOrdinal("NomeUsuario"))
                    });
            return sorteios;
        }

        public void DeletarParticipantesSorteio(int id)
        {
            ExecuteProcedure(Procedures.sp_DeletarParticipantesSorteio);
            AddParameter("@Id", id);
            ExecuteNonQuery();
        }
    }
}
