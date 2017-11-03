﻿using Hades.Application.Interface;
using Hades.Domain.Entities;
using Hades.Web.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Hades.Web.Controllers
{
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioAppService _usuarioAppService;

        public UsuarioController(IUsuarioAppService usuarioAppService)
        {
            _usuarioAppService = usuarioAppService;
        }

        // GET: Usuario
        public ActionResult Index()
        {
            ViewBag.NomeUsuario = Session["Nome"];
            return View();
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int id)
        {
            var usuario = _usuarioAppService.GetById(id);
            if (!usuario.IsSuccessStatusCode)
                return ErrorMessage("Erro ao trazer Usuario");
            var mostraUsuario =
                JsonConvert.DeserializeObject<UsuarioViewModel>(usuario.Content.ReadAsStringAsync().Result);

            return View(mostraUsuario);
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        public ActionResult CreateConfirmed(Usuario usuario)
        {
            var request = _usuarioAppService.GetByName(usuario.Nome);
            if (!request.IsSuccessStatusCode)
                return ErrorMessage("Erro ao verificar se usuário já existe");

            var usuarioViewModel = JsonConvert.DeserializeObject<Usuario>(request.Content.ReadAsStringAsync().Result);

            if (usuarioViewModel != null)
                if (usuario.Nome == usuarioViewModel.Nome)
                    return ErrorMessage("Usuario já existe, insira outro nome.");

            var response = _usuarioAppService.Post(usuario);
            return response.IsSuccessStatusCode
                ? Json("Cadastro Efetuado com sucesso")
                : ErrorMessage($"Erro ao criar usuario: {response.Content.ReadAsStringAsync().Result}");
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            var response = _usuarioAppService.GetById(id);
            if (!response.IsSuccessStatusCode)
                return ErrorMessage("Erro ao buscar usuario");
            var usuario = JsonConvert.DeserializeObject<Usuario>(response.Content.ReadAsStringAsync().Result);
            return View(new UsuarioViewModel(usuario));
        }

        // PUT: Usuario/Edit/5
        public ActionResult EditConfirmed(Usuario usuario)
        {
            var response = _usuarioAppService.Put(usuario);
            return response.IsSuccessStatusCode
                ? Json("Cadastro Atualizado com sucesso")
                : ErrorMessage($"Erro ao editar usuario: {response.Content.ReadAsStringAsync().Result}");
        }

        public ActionResult BuscaGridUsuario()
        {
            var usuarioViewModel = _usuarioAppService.GetAll();
            if (!usuarioViewModel.IsSuccessStatusCode)
                return ErrorMessage("Erro ao buscar usuarios.");
            var usuarios =
                JsonConvert.DeserializeObject<IEnumerable<UsuarioViewModel>>(usuarioViewModel.Content.ReadAsStringAsync().Result);

            ViewBag.Title = "HADES";
            ViewBag.NomeUsuario = Session["Nome"];

            return View("_TabelaUsuarios", usuarios);
        }

        public ActionResult AtivarUsuario(int id)
        {
            try
            {
                var response = _usuarioAppService.StatusUsuario(id, true);
                if (!response.IsSuccessStatusCode)
                    return ErrorMessage("Erro ao alterar o usuario");
                return RedirectToAction("BuscaGridUsuario");
            }
            catch (Exception e)
            {

                return ErrorMessage("Erro ao alterar o usuario, " + e.Message);
            }
        }

        public ActionResult DesativarUsuario(int id)
        {
            var response = _usuarioAppService.StatusUsuario(id, false);
            if (!response.IsSuccessStatusCode)
                return ErrorMessage("Erro ao alterar o usuario");
            return RedirectToAction("BuscaGridUsuario");
        }
    }
}
