﻿using Hades.Domain.Entities;
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
            SP_UpdVencedoresSorteios,
            SP_UpdVencedoresSorteiosNovamente,
            SP_GetVencedores
        }

        public void Participar(int idSorteio, int idUsuario)
        {
            ExecuteProcedure(Procedures.SP_AddParticipante);
            AddParameter("@IdUsuario", idUsuario);
            AddParameter("@IdSorteio", idSorteio);
            ExecuteNonQuery();
        }

        public IEnumerable<SorteioParticipanteDto> GetAll(int id)
        {
            ExecuteProcedure(Procedures.SP_GetParticipantes);
            AddParameter("@Id", id);
            var sorteios = new List<SorteioParticipanteDto>();
            using (var r = ExecuteReader())
                while (r.Read())
                    sorteios.Add(new SorteioParticipanteDto
                    {
                        Id_Participante = r.GetInt32(r.GetOrdinal("Id_Participante")),
                        Nome_Participante = r["Nome_Participante"].ToString()
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

        public void SortearNovamente(int idSorteio)
        {
            ExecuteProcedure(Procedures.SP_UpdVencedoresSorteiosNovamente);
            AddParameter("@idSorteio", idSorteio);
            ExecuteNonQuery();
        }

        public IEnumerable<SorteioParticipanteDto> GetVencedores(int idSorteio)
        {
            ExecuteProcedure(Procedures.SP_GetVencedores);
            AddParameter("@idSorteio", idSorteio);
            var sorteios = new List<SorteioParticipanteDto>();
            using (var r = ExecuteReader())
                while (r.Read())
                    sorteios.Add(new SorteioParticipanteDto
                    {
                        Nome_Participante = r["Nom_Vencedor"].ToString(),
                        Sorteio = new SorteioDto
                        {
                            Id = r.GetInt32(r.GetOrdinal("IdSorteio")),
                            Nome = r["Nom_Sorteio"].ToString()
                        }
                    });
            return sorteios;
        }
    }
}
