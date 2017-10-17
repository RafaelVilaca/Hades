using Hades.Domain.Entities;
using Hades.Domain.Interfaces.Repositories;
using Hades.Infra.Data.Context;
using System.Collections.Generic;

namespace Hades.Infra.Data.Repositories
{
    public class UsuarioRepository : BaseRepository, IUsuarioRepository
    {
        private enum Procedures
        {
            SP_ListarUsuarios,
            SP_ListarUsuarioPorId,
            SP_AddUsuario,
            SP_UpdUsuario,
            SP_AlteraStatusUsuario,
            SP_ListarUsuarioPorNome
        }

        public void Post(Usuario usuario)
        {
            ExecuteProcedure(Procedures.SP_AddUsuario);
            AddParameter("@Nome", usuario.Nome);
            AddParameter("@Email", usuario.Email);
            AddParameter("@Senha", usuario.Senha);
            ExecuteNonQuery();
        }

        public Usuario GetById(int id)
        {
            ExecuteProcedure(Procedures.SP_ListarUsuarioPorId);
            AddParameter("@Id", id);
            using (var r = ExecuteReader())
                if (r.Read())
                    return new Usuario
                    {
                        Nome = r.GetString(r.GetOrdinal("Nome")),
                        Email = r.GetString(r.GetOrdinal("Email")),
                        Ativo = r.GetBoolean(r.GetOrdinal("Ativo")),
                        Administrador = r.GetBoolean(r.GetOrdinal("Administrador")),
                        DataCadastro = r.GetDateTime(r.GetOrdinal("DataCadastro")),
                        Senha = r.GetString(r.GetOrdinal("Senha"))
                    };
            return null;
        }

        public IEnumerable<Usuario> GetAll()
        {
            ExecuteProcedure(Procedures.SP_ListarUsuarios);
            var usuarios = new List<Usuario>();
            using (var r = ExecuteReader())
                while (r.Read())
                    usuarios.Add(new Usuario
                    {
                        Id = r.GetInt32(r.GetOrdinal("Id")),
                        Nome = r.GetString(r.GetOrdinal("Nome")),
                        Email = r.GetString(r.GetOrdinal("Email")),
                        Ativo = r.GetBoolean(r.GetOrdinal("Ativo")),
                        Administrador = r.GetBoolean(r.GetOrdinal("Administrador")),
                        DataCadastro = r.GetDateTime(r.GetOrdinal("DataCadastro")),
                        Senha = r.GetString(r.GetOrdinal("Senha"))
                    });
            return usuarios;
        }

        public Usuario GetByName(string nome)
        {
            ExecuteProcedure(Procedures.SP_ListarUsuarioPorNome);
            AddParameter("@Nome", nome);
            using (var r = ExecuteReader())
                if (r.Read())
                    return new Usuario
                    {
                        Id = r.GetInt32(r.GetOrdinal("Id")),
                        Nome = r.GetString(r.GetOrdinal("Nome")),
                        Email = r.GetString(r.GetOrdinal("Email")),
                        Ativo = r.GetBoolean(r.GetOrdinal("Ativo")),
                        Administrador = r.GetBoolean(r.GetOrdinal("Administrador")),
                        DataCadastro = r.GetDateTime(r.GetOrdinal("DataCadastro")),
                        Senha = r.GetString(r.GetOrdinal("Senha"))
                    };
            return null;
        }

        public void Put(Usuario usuario)
        {
            ExecuteProcedure(Procedures.SP_UpdUsuario);
            AddParameter("@IdUsua", usuario.Id);
            AddParameter("@Nome", usuario.Nome);
            AddParameter("@Email", usuario.Email);
            AddParameter("@Senha", usuario.Senha);
            AddParameter("@Administrador", usuario.Administrador);
            AddParameter("@Ativo", usuario.Ativo);
            ExecuteNonQuery();
        }

        public void StatusUsuario(int id, bool status)
        {
            ExecuteProcedure(Procedures.SP_AlteraStatusUsuario);
            AddParameter("@Id", id);
            AddParameter("@Ativo", status ? 1 : 0);
            ExecuteNonQuery();
        }
    }
}
