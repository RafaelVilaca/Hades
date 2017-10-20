using Hades.Domain.Entities;
using Hades.Domain.Interfaces.Repositories;
using Hades.Infra.Data.Context;

namespace Hades.Infra.Data.Repositories
{
    public class VotacaoRepository : BaseRepository, IVotacaoRepository
    {
        private enum Procedures
        {
            SP_AddVoto,
            SP_GetVotos
        }

        public Votacao GetVotos(Votacao votacao)
        {
            ExecuteProcedure(Procedures.SP_GetVotos);
            AddParameter("@UsuarioId", votacao.IdUsuario);
            AddParameter("@EnqueteId", votacao.IdEnquete);
            AddParameter("@Justificativa", votacao.Justificativa);
            AddParameter("@TipoVoto", votacao.TipoVoto);
            using (var r = ExecuteReader())
            {
                return new Votacao
                {
                    IdEnquete = r.GetInt32(r.GetOrdinal("IdEnquete")),
                    IdUsuario = r.GetInt32(r.GetOrdinal("IdUsuario")),
                    Enquete = r.GetString(r.GetOrdinal("Titulo")),
                    NomeUsuario = r.GetString(r.GetOrdinal("NomeUsuario")),
                    Justificativa = r.GetString(r.GetOrdinal("Justificativa")),
                    TipoVoto = r.GetBoolean(r.GetOrdinal("TipoVoto"))
                };
            }
        }

        public void Post(Votacao votacao)
        {
            ExecuteProcedure(Procedures.SP_AddVoto);
            AddParameter("@UsuarioId", votacao.IdUsuario);
            AddParameter("@EnqueteId", votacao.IdEnquete);
            AddParameter("@Justificativa", votacao.Justificativa);
            AddParameter("@TipoVoto", votacao.TipoVoto);
            ExecuteNonQuery();
        }
    }
}
