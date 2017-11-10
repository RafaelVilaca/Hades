using Hades.Domain.Entities;
using Hades.Domain.Interfaces.Repositories;
using Hades.Infra.Data.Context;
using System.Collections.Generic;

namespace Hades.Infra.Data.Repositories
{
    public class CampanhaRepository : BaseRepository, ICampanhaRepository
    {
        private enum Procedures
        {
            SP_ListarCampanhas,
            SP_ListarCampanha,
            SP_InsCampanha,
            SP_UpdCampanha,
            SP_DelCampanha
        }

        public IEnumerable<CampanhaDto> GetCampanhas()
        {
            ExecuteProcedure(Procedures.SP_ListarCampanhas);
            var campanhas = new List<CampanhaDto>();
            using (var r = ExecuteReader())
                while (r.Read())
                campanhas.Add(new CampanhaDto
                {
                    //IdCampanha
                    DescCampanha = r["DescCampanha"].ToString(),
                    DataCadastro = r.GetDateTime(r.GetOrdinal("DataCadastro")),
                    DataLimite = r.GetDateTime(r.GetOrdinal("DataLimite")),
                    ValorCampanha = r.GetDecimal(r.GetOrdinal("ValorCampanha")),
                    IndAtivo  = r.GetBoolean(r.GetOrdinal("IndAtivo")),
                    IdCriador = r.GetInt32(r.GetOrdinal("IdCriador"))
                });
            return campanhas;
        }

        public CampanhaDto GetCampanha(int idCampanha)
        {
            ExecuteProcedure(Procedures.SP_ListarCampanha);
            AddParameter("@idCampanha", idCampanha);
            var campanha = new CampanhaDto();
            using (var r = ExecuteReader())
            {
                while (r.Read())
                    campanha = new CampanhaDto
                    {
                        IdCampanha = r.GetInt32(r.GetOrdinal("IdCampanha")),
                        DescCampanha = r["DescCampanha"].ToString(),
                        DataCadastro = r.GetDateTime(r.GetOrdinal("DataCadastro")),
                        DataLimite = r.GetDateTime(r.GetOrdinal("DataLimite")),
                        ValorCampanha = r.GetDecimal(r.GetOrdinal("ValorCampanha")),
                        IndAtivo = r.GetBoolean(r.GetOrdinal("IndAtivo")),
                        IdCriador = r.GetInt32(r.GetOrdinal("IdCriador"))
                    };
                if (!r.NextResult()) return campanha;
                while (r.NextResult())
                {
                    campanha.Participantes.Add(new CampanhaParticipantesDto
                    {
                        IdUsuario = r.GetInt32(r.GetOrdinal("IdUsuario")),
                        IdCampanha = r.GetInt32(r.GetOrdinal("IdCampanha")),
                        DataCadastro = r.GetDateTime(r.GetOrdinal("DataCadastro"))
                    });
                }
            }
            return campanha;
        }

        public void InsCampanha(CampanhaDto campanha)
        {
            ExecuteProcedure(Procedures.SP_InsCampanha);
            AddParameter("@DescCampanha", campanha.DescCampanha);
            AddParameter("@DataLimite", campanha.DataLimite);
            AddParameter("@ValorCampanha", campanha.ValorCampanha);
            AddParameter("@IdCriador", campanha.IdCriador);
            ExecuteNonQuery();
        }

        public void UpdCampanha(CampanhaDto campanha)
        {
            ExecuteProcedure(Procedures.SP_UpdCampanha);
            AddParameter("@IdCampanha", campanha.IdCampanha);
            AddParameter("@DescCampanha", campanha.DescCampanha);
            AddParameter("@DataLimite", campanha.DataLimite);
            AddParameter("@ValorCampanha", campanha.ValorCampanha);
            AddParameter("@IdCriador", campanha.IdCriador);
            ExecuteNonQuery();
        }

        public void DelCampanha(int idCampanha)
        {
            ExecuteProcedure(Procedures.SP_DelCampanha);
            AddParameter("@idCampanha", idCampanha);
            ExecuteNonQuery();
        }
    }
}
