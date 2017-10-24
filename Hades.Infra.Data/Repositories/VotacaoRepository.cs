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
            SP_GetVotos,
            SP_UpdVoto
        }

        public Votacao GetVotos(int idUsuario, int idEnquete)
        {
            ExecuteProcedure(Procedures.SP_GetVotos);
            AddParameter("@UsuarioId", idUsuario);
            AddParameter("@EnqueteId", idEnquete);
            using (var r = ExecuteReader())
                return !r.Read()
                    ? null
                    : new Votacao
                    {
                        IdEnquete = idEnquete,
                        IdUsuario = idUsuario,
                        Enquete = r["Titulo"].ToString(),
                        NomeUsuario = r["NomeUsuario"].ToString(),
                        Justificativa = r["Justificativa"].ToString(),
                        TipoVoto = r.GetBoolean(r.GetOrdinal("TipoVoto"))
                    };
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

        public void Put(Votacao votacao)
        {
            ExecuteProcedure(Procedures.SP_UpdVoto);
            AddParameter("@IdUsua", votacao.IdUsuario);
            AddParameter("@IdEnq", votacao.IdEnquete);
            AddParameter("@Justificativa", votacao.Justificativa);
            AddParameter("@TipoVoto", votacao.TipoVoto);
            ExecuteNonQuery();
        }
    }
}

