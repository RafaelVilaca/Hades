using System.Collections.Generic;
using Hades.Domain.Entities;
using Hades.Domain.Interfaces.Repositories;
using Hades.Infra.Data.Context;

namespace Hades.Infra.Data.Repositories
{
    public class EnqueteRepository : BaseRepository, IEnqueteRepository
    {
        private enum Procedures
        {
            sp_ListarEnquetes,
            sp_ListarEnquetePorId,
            sp_AddEnquete,
            sp_UpdateEnquete,
            sp_AlteraStatusEnquete
        }

        public void Post(Enquete enquete)
        {
            ExecuteProcedure(Procedures.sp_AddEnquete);
            AddParameter("@Titulo", enquete.Titulo);
            AddParameter("@Assunto", enquete.Assunto);
            AddParameter("@DataEnquete", enquete.DataEnquete);
            AddParameter("@UsuarioId", enquete.Usuario.Id);
            ExecuteNonQuery();
        }

        public Enquete GetById(int id)
        {
            ExecuteProcedure(Procedures.sp_ListarEnquetePorId);
            AddParameter("@Id", id);
            using (var r = ExecuteReader())
                if (r.Read())
                    return new Enquete
                    {
                        Titulo = r.GetString(r.GetOrdinal("Titulo")),
                        Assunto = r.GetString(r.GetOrdinal("Assunto")),
                        Ativo = r.GetBoolean(r.GetOrdinal("Ativo")),
                        DataEnquete = r.GetDateTime(r.GetOrdinal("DataEnquete")),
                        Usuario = new Usuario { Nome = r.GetString(r.GetOrdinal("Nome")) }
                    };

            return null;
        }

        public IEnumerable<Enquete> GetAll()
        {
            ExecuteProcedure(Procedures.sp_ListarEnquetes);
            var enquetes = new List<Enquete>();
            using (var r = ExecuteReader())
            {
                while (r.Read())
                    enquetes.Add(new Enquete()
                    {
                        Titulo = r.GetString(r.GetOrdinal("Titulo")),
                        Assunto = r.GetString(r.GetOrdinal("Assunto")),
                        Ativo = r.GetBoolean(r.GetOrdinal("Ativo")),
                        DataEnquete = r.GetDateTime(r.GetOrdinal("DataEnquete")),
                        Usuario = new Usuario { Nome = r.GetString(r.GetOrdinal("Nome")) }
                    });
            }
            return enquetes;
        }

        public void Put(Enquete enquete)
        {
            ExecuteProcedure(Procedures.sp_UpdateEnquete);
            AddParameter("@Id", enquete.Id);
            AddParameter("@Titulo", enquete.Titulo);
            AddParameter("@Assunto", enquete.Assunto);
            AddParameter("@Ativo", enquete.Ativo);
            ExecuteNonQuery();
        }

        public void StatusEnquete(int id, bool status)
        {
            ExecuteProcedure(Procedures.sp_AlteraStatusEnquete);
            AddParameter("@Id", id);
            AddParameter("@Ativo", status?1:0);
            ExecuteNonQuery();
        }
    }
}