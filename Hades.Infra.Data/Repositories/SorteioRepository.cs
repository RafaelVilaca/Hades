using System.Collections.Generic;
using Hades.Domain.Entities;
using Hades.Domain.Interfaces.Repositories;
using Hades.Infra.Data.Context;

namespace Hades.Infra.Data.Repositories
{
    public class SorteioRepository : BaseRepository, ISorteioRepository
    {
        private enum Procedures
        {
            sp_AddSorteio,
            sp_UpdateSorteio,
            sp_ListarSorteio,
            sp_ListarSorteioPorId,
            sp_DeletarSorteio
        }

        public void Post(Sorteio sorteio)
        {
            ExecuteProcedure(Procedures.sp_AddSorteio);
            AddParameter("@Nome", sorteio.Nome);
            ExecuteNonQuery();
        }

        public Sorteio GetById(int id)
        {
            ExecuteProcedure(Procedures.sp_ListarSorteioPorId);
            AddParameter("@Id", id);
            using (var r = ExecuteReader())
                if (r.Read())
                    return new Sorteio{ Nome = r.GetString(r.GetOrdinal("Nome")) };
            return null;
        }

        public IEnumerable<Sorteio> GetAll()
        {
            ExecuteProcedure(Procedures.sp_ListarSorteio);
            var sorteios = new List<Sorteio>();
            using (var r = ExecuteReader())
                while (r.Read())
                    sorteios.Add(new Sorteio
                    {
                        Id = r.GetInt32(r.GetOrdinal("Id")),
                        Nome = r.GetString(r.GetOrdinal("Nome")),
                        QtdParticipantes = r.GetInt32(r.GetOrdinal("NumeroParticipantes"))
                    });
            return sorteios;
        }

        public void Put(Sorteio sorteio)
        {
            ExecuteProcedure(Procedures.sp_UpdateSorteio);
            AddParameter("@Id", sorteio.Id);
            AddParameter("@Nome", sorteio.Nome);
            ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            ExecuteProcedure(Procedures.sp_DeletarSorteio);
            AddParameter("@Id", id);
            ExecuteNonQuery();
        }
    }
}
