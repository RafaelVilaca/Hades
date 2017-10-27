using Hades.Domain.Entities;
using Hades.Domain.Interfaces.Repositories;
using Hades.Infra.Data.Context;
using System.Collections.Generic;

namespace Hades.Infra.Data.Repositories
{
    public class SorteioParticipanteRepository : BaseRepository, ISorteioParticipanteRepository
    {
        private enum Procedures
        {
            SP_GetParticipantes,
            SP_AddParticipante,
            SP_DeletarParticipantesSorteio,
            SP_UpdVencedoresSorteios
        }

        public void Participar(int idSorteio, int idUsuario)
        {
            ExecuteProcedure(Procedures.SP_AddParticipante);
            AddParameter("@IdUsuario", idUsuario);
            AddParameter("@IdSorteio", idSorteio);
            ExecuteNonQuery();
        }

        public IEnumerable<SorteioParticipante> GetAll(int id)
        {
            ExecuteProcedure(Procedures.SP_GetParticipantes);
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

        public void DeletarParticipantesSorteio(int idSorteio, int idUsuario)
        {
            ExecuteProcedure(Procedures.SP_DeletarParticipantesSorteio);
            AddParameter("@IdSorteio", idSorteio);
            AddParameter("@IdUsuario", idUsuario);
            ExecuteNonQuery();
        }

        public void VencedorParticipantesSorteio(int idSorteio, int idUsuario)
        {
            ExecuteProcedure(Procedures.SP_UpdVencedoresSorteios);
            AddParameter("@IdSorteio", idSorteio);
            AddParameter("@IdUsuario", idUsuario);
            ExecuteNonQuery();
        }
    }
}
