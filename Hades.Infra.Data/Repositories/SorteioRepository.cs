using Hades.Domain.Entities;
using Hades.Domain.Interfaces.Repositories;
using Hades.Infra.Data.Context;
using System.Collections.Generic;

namespace Hades.Infra.Data.Repositories
{
    public class SorteioRepository : BaseRepository, ISorteioRepository
    {
        private enum Procedures
        {
            SP_AddSorteio,
            SP_UpdSorteio,
            SP_ListarSorteio,
            SP_ListarSorteioPorId,
            SP_DelSorteio
        }

        public void Post(Sorteio sorteio)
        {
            ExecuteProcedure(Procedures.SP_AddSorteio);
            AddParameter("@Nome", sorteio.Nome);
            AddParameter("@QtdItens", sorteio.QtdeItens);
            AddParameter("@DataSorteio", sorteio.DataSorteio);
            AddParameter("@IdCriador", sorteio.IdUsuario);
            ExecuteNonQuery();
        }

        public Sorteio GetById(int id)
        {
            ExecuteProcedure(Procedures.SP_ListarSorteioPorId);
            AddParameter("@Id", id);
            using (var r = ExecuteReader())
                if (r.Read())
                    return new Sorteio { Nome = r.GetString(r.GetOrdinal("Nome")) };
            return null;
        }

        public IEnumerable<Sorteio> GetAll()
        {
            ExecuteProcedure(Procedures.SP_ListarSorteio);
            var sorteios = new List<Sorteio>();
            using (var r = ExecuteReader())
                while (r.Read())
                    sorteios.Add(new Sorteio
                    {
                        Id = r.GetInt32(r.GetOrdinal("Id")),
                        Nome = r["Nome"].ToString(),
                        QtdParticipantes = r.GetInt32(r.GetOrdinal("NumeroParticipantes")),
                        IdUsuario = r.GetInt32(r.GetOrdinal("IdUsuario"))
                    });
            return sorteios;
        }

        public void Put(Sorteio sorteio)
        {
            ExecuteProcedure(Procedures.SP_UpdSorteio);
            AddParameter("@Id", sorteio.Id);
            AddParameter("@Nome", sorteio.Nome);
            ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            ExecuteProcedure(Procedures.SP_DelSorteio);
            AddParameter("@Id", id);
            ExecuteNonQuery();
        }
    }
}
