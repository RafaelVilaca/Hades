using Hades.Domain.Entities;
using Hades.Domain.Interfaces.Repositories;
using Hades.Infra.Data.Context;

namespace Hades.Infra.Data.Repositories
{
    public class VotacaoRepository : BaseRepository, IVotacaoRepository
    {
        private enum Procedures
        {
            sp_AddVoto,
            sp_UpdateVoto
        }

        public void Put(Votacao votacao)
        {
            ExecuteProcedure(Procedures.sp_UpdateVoto);
            AddParameter("@UsuarioId", votacao.Usuarios.Id);
            AddParameter("@EnqueteId", votacao.Enquetes.Id);
            AddParameter("@Justificativa", votacao.Justificativa);
            AddParameter("@TipoVoto", votacao.TipoVoto);
            ExecuteNonQuery();
        }

        public void Post(Votacao votacao)
        {
            ExecuteProcedure(Procedures.sp_AddVoto);
            AddParameter("@Id", votacao.Id);
            AddParameter("@Justificativa", votacao.Justificativa);
            AddParameter("@TipoVoto", votacao.TipoVoto);
            ExecuteNonQuery();
        }
    }
}
