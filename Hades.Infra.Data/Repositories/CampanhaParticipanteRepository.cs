using Hades.Domain.Entities;
using Hades.Domain.Interfaces.Repositories;
using Hades.Infra.Data.Context;
using System.Collections.Generic;

namespace Hades.Infra.Data.Repositories
{
    public class CampanhaParticipanteRepository : BaseRepository, ICampanhaParticipanteRepository
    {
        private enum Procedures
        {
            SP_GetParticipantesCampanha,
            SP_InsParticipanteCampanha,
            SP_DelParticipanteCampanha

        }

        public IEnumerable<CampanhaParticipantesDto> GetParticipantesCampanha(int campanha)
        {
            ExecuteProcedure(Procedures.SP_GetParticipantesCampanha);
            AddParameter("@idCampanha", campanha);
            var participantes = new List<CampanhaParticipantesDto>();
            using (var r = ExecuteReader())
                while(r.Read())
                    participantes.Add(new CampanhaParticipantesDto
                    {
                        IdUsuario = r.GetInt32(r.GetOrdinal("IdUsuario")),
                        IdCampanha = r.GetInt32(r.GetOrdinal("IdCampanha")),
                        NomParticipante = r["NomParticipante"].ToString()
                    });
            return participantes;
        }

        public void InsParticipanteCampanha(int idUsuario, int idCampanha)
        {
            ExecuteProcedure(Procedures.SP_InsParticipanteCampanha);
            AddParameter("@IdUsuario", idUsuario);
            AddParameter("@IdCampanha", idCampanha);
            ExecuteNonQuery();
        }

        public void DelParticipanteCampanha(int idUsuario, int idCampanha)
        {
            ExecuteProcedure(Procedures.SP_DelParticipanteCampanha);
            AddParameter("@IdUsuario", idUsuario);
            AddParameter("@IdCampanha", idCampanha);
            ExecuteNonQuery();
        }
    }
}
