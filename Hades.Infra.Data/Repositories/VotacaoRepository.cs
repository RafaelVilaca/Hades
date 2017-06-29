using Hades.Domain.Entities;
using Hades.Domain.Interfaces.Repositories;
using Hades.Infra.Data.Context;

namespace Hades.Infra.Data.Repositories
{
    public class VotacaoRepository : BaseRepository, IVotacaoRepository
    {
        private enum Procedures
        {
            sp_AddVoto
        }

        public void Post(Votacao votacao)
        {
            ExecuteProcedure(Procedures.sp_AddVoto);
            AddParameter("@UsuarioId", votacao.Usuarios.Id);
            AddParameter("@EnqueteId", votacao.Enquetes.Id);
            AddParameter("@Justificativa", votacao.Justificativa);
            AddParameter("@TipoVoto", votacao.TipoVoto);
            ExecuteNonQuery();
        }
    }
}
