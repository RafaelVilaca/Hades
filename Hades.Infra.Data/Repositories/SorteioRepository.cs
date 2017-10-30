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
            AddParameter("@IdCriador", sorteio.IdCriador);
            ExecuteNonQuery();
        }

        public Sorteio GetById(int id)
        {
            ExecuteProcedure(Procedures.SP_ListarSorteioPorId);
            AddParameter("@Id", id);
            var sorteio = new Sorteio();
            using (var r = ExecuteReader())
            {
                if (r.Read())
                    sorteio = new Sorteio
                    {
                        Id = r.GetInt32(r.GetOrdinal("Id")),
                        Nome = r["Nome"].ToString(),
                        QtdeItens = r.GetInt32(r.GetOrdinal("QtdItens")),
                        QtdParticipantes = r.GetInt32(r.GetOrdinal("NumeroParticipantes")),
                        IdCriador = r.GetInt32(r.GetOrdinal("IdCriador")),
                        DataSorteio = r.GetDateTime(r.GetOrdinal("DataSorteio")),
                        NomeCriador = r["NomeCriador"].ToString()
                    };
                if (r.NextResult())
                    while (r.Read())
                    {
                        sorteio.SorteioParticipantes.Add(new SorteioParticipante
                        {
                            Id_Participante = r.GetInt32(r.GetOrdinal("Id_Participante")),
                            Nome_Participante = r["Nome_Participante"].ToString()
                        });
                    };
            }
            return sorteio;
        }

        public IEnumerable<Sorteio> GetAll(int idUsuario)
        {
            var listaCompleta = new List<Sorteio>();

            ExecuteProcedure(Procedures.SP_ListarSorteio);
            AddParameter("@CodUsua", idUsuario);
            
            using (var r = ExecuteReader())
            {
                while (r.Read())
                    listaCompleta.Add(new Sorteio
                    {
                        Id = r.GetInt32(r.GetOrdinal("Id")),
                        Nome = r["Nome"].ToString(),
                        QtdeItens = r.GetInt32(r.GetOrdinal("QtdItens")),
                        QtdParticipantes = r.GetInt32(r.GetOrdinal("NumeroParticipantes")),
                        IdCriador = r.GetInt32(r.GetOrdinal("IdCriador")),
                        DataSorteio = r.GetDateTime(r.GetOrdinal("DataSorteio")),
                        NomeCriador = r["NomeCriador"].ToString(),
                        IndParticipante = r["IndParticipa"].ToString()
                    });
            }
            return listaCompleta;
        }

        public void Put(Sorteio sorteio)
        {
            ExecuteProcedure(Procedures.SP_UpdSorteio);
            AddParameter("@Id", sorteio.Id);
            AddParameter("@Nome", sorteio.Nome);
            AddParameter("@QtdItens", sorteio.QtdeItens);
            AddParameter("@DataSorteio", sorteio.DataSorteio);
            AddParameter("@Ativo", sorteio.Ativo);
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
