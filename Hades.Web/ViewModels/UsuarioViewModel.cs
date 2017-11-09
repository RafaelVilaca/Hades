﻿using Hades.Domain.Entities;
using System;
using System.Text.RegularExpressions;

namespace Hades.Web.ViewModels
{
    public class UsuarioViewModel
    {
        public UsuarioViewModel()
        {

        }

        public UsuarioViewModel(Usuario user)
        {
            Id = user.Id;
            Nome = user.Nome;
            Email = user.Email;
            DataCadastro = user.DataCadastro;
            Senha = user.Senha;
            Administrador = user.Administrador;
            Ativo = user.Ativo;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Senha { get; set; }
        public bool Administrador { get; set; }
        public bool Ativo { get; set; }

        public int IsValid()
        {
            string email = this.Email;
            Regex rg =
                new Regex(
                    @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            if (!rg.IsMatch(email) || string.IsNullOrEmpty(email))
                return 1;

            return 0;
        }
    }
}