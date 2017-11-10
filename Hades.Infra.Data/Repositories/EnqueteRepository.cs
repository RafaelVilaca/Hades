using Hades.Domain.Entities;
using Hades.Domain.Interfaces.Repositories;
using Hades.Infra.Data.Context;
using System.Collections.Generic;

namespace Hades.Infra.Data.Repositories
{
    public class EnqueteRepository : BaseRepository, IEnqueteRepository
    {
        private enum Procedures
        {
            SP_ListarEnquetes,
            SP_ListarEnquetePorId,
            SP_AddEnquete,
            SP_UpdEnquete,
            SP_AlteraStatusEnquete
        }

        public void Post(EnqueteDto enquete)
        {
            ExecuteProcedure(Procedures.SP_AddEnquete);
            AddParameter("@Titulo", enquete.Titulo);
            AddParameter("@Descricao", enquete.Assunto);
            AddParameter("@Nom_LocalCotado", enquete.Nom_LocalCotado);
            AddParameter("@Valor", enquete.Valor);
            AddParameter("@UsuarioId", enquete.UsuarioId);            
            ExecuteNonQuery();
        }

        public EnqueteDto GetById(int id)
        {
            ExecuteProcedure(Procedures.SP_ListarEnquetePorId);
            AddParameter("@Id", id);
            var enquete = new EnqueteDto();

            using (var r = ExecuteReader())
            {
                if (r.Read())
                {
                    enquete = new EnqueteDto
                    {
                        Id = r.GetInt32(r.GetOrdinal("Id")),
                        Titulo = r.GetString(r.GetOrdinal("Titulo")),
                        Assunto = r.GetString(r.GetOrdinal("Descricao")),
                        Ativo = r.GetBoolean(r.GetOrdinal("Ativo")),
                        DataEnquete = r.GetDateTime(r.GetOrdinal("DataEnquete")),
                        Criador = r.GetString(r.GetOrdinal("Criador")),
                        Valor = r.GetDecimal(r.GetOrdinal("Valor")),
                        Nom_LocalCotado = r.GetString(r.GetOrdinal("LocalCotado"))
                    };
                }
                if (r.NextResult())
                    while (r.Read())
                    {
                        enquete.ListaVotacao.Add(new Votacao
                        {
                            NomeUsuario = r.GetString(r.GetOrdinal("NomeUsuario")),
                            TipoVoto = r.GetBoolean(r.GetOrdinal("Voto")),
                            Justificativa = r.GetString(r.GetOrdinal("Justificativa"))
                        });
                    };

                return enquete;
            }
        }

        public IEnumerable<EnqueteDto> GetAll()
        {
            ExecuteProcedure(Procedures.SP_ListarEnquetes);
            var enquetes = new List<EnqueteDto>();
            using (var r = ExecuteReader())
            {
                while (r.Read())
                    enquetes.Add(new EnqueteDto()
                    {
                        Id = r.GetInt32(r.GetOrdinal("Id")),
                        Titulo = r.GetString(r.GetOrdinal("Titulo")),
                        Assunto = r.GetString(r.GetOrdinal("Descricao")),
                        Ativo = r.GetBoolean(r.GetOrdinal("Ativo")),
                        DataEnquete = r.GetDateTime(r.GetOrdinal("DataEnquete")),
                        Criador = r.GetString(r.GetOrdinal("Criador")),
                        Valor = r.GetDecimal(r.GetOrdinal("Valor")),
                        Nom_LocalCotado = r.GetString(r.GetOrdinal("LocalCotado")),
                        ////Votos
                        VotoFavor = r.GetInt32(r.GetOrdinal("VotoFavor")),
                        VotoContra = r.GetInt32(r.GetOrdinal("VotoContra"))
                    });
            }
            return enquetes;
        }

        public void Put(EnqueteDto enquete)
        {
            ExecuteProcedure(Procedures.SP_UpdEnquete);
            AddParameter("@IdEnq", enquete.Id);
            AddParameter("@Titulo", enquete.Titulo);
            AddParameter("@Descricao", enquete.Assunto);
            AddParameter("@Ativo", enquete.Ativo);
            AddParameter("@Valor", enquete.Valor);
            AddParameter("@LocalCotado", enquete.Nom_LocalCotado);
            ExecuteNonQuery();
        }

        public void StatusEnquete(int id)
        {
            ExecuteProcedure(Procedures.SP_AlteraStatusEnquete);
            AddParameter("@Id", id);
            AddParameter("@Ativo", false);//status?1:0);
            ExecuteNonQuery();
        }
    }
}