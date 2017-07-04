﻿using System;
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
            AddParameter("@DataEnquete", DateTime.Now);
            AddParameter("@UsuarioId", enquete.Usuario.Id);
            ExecuteNonQuery();
        }

        public Enquete GetById(int id)
        {
            ExecuteProcedure(Procedures.sp_ListarEnquetePorId);
            AddParameter("@Id", id);
            var enquete = new Enquete();

            using (var r = ExecuteReader())
            {
                if (r.Read())
                {
                    enquete = new Enquete
                    {
                        Id = r.GetInt32(r.GetOrdinal("Id")),
                        Titulo = r.GetString(r.GetOrdinal("Titulo")),
                        Assunto = r.GetString(r.GetOrdinal("Assunto")),
                        Ativo = r.GetBoolean(r.GetOrdinal("Ativo")),
                        DataEnquete = r.GetDateTime(r.GetOrdinal("DataEnquete")),
                        Criador = r.GetString(r.GetOrdinal("Criador")),
                    };
                }
                if (r.NextResult())
                    while (r.Read())
                    {
                        enquete.ListaVotacao.Add(new Votacao
                        {
                            Votador = r.GetString(r.GetOrdinal("NomeUsuario")),
                            TipoVoto = r.GetBoolean(r.GetOrdinal("Voto")),
                            Justificativa = r.GetString(r.GetOrdinal("Justificativa"))
                        });
                    };

                return enquete;
            }
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
                        Id = r.GetInt32(r.GetOrdinal("Id")),
                        Titulo = r.GetString(r.GetOrdinal("Titulo")),
                        Assunto = r.GetString(r.GetOrdinal("Assunto")),
                        Ativo = r.GetBoolean(r.GetOrdinal("Ativo")),
                        DataEnquete = r.GetDateTime(r.GetOrdinal("DataEnquete")),
                        Criador = r.GetString(r.GetOrdinal("Criador")),
                        ////Votos
                        VotoFavor = r.GetInt32(r.GetOrdinal("VotoFavor")),
                        VotoContra = r.GetInt32(r.GetOrdinal("VotoContra"))
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

        public void StatusEnquete(int id)
        {
            ExecuteProcedure(Procedures.sp_AlteraStatusEnquete);
            AddParameter("@Id", id);
            AddParameter("@Ativo", false);//status?1:0);
            ExecuteNonQuery();
        }
    }
}