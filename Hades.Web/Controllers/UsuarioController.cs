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
            return View();
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var usuario = _usuarioAppService.GetById(id);
                if (!usuario.IsSuccessStatusCode)
                    return ErrorMessage("Erro ao trazer Usuario");
                var mostraUsuario =
                    JsonConvert.DeserializeObject<UsuarioViewModel>(usuario.Content.ReadAsStringAsync().Result);

                return View(mostraUsuario);
            }
            catch (Exception e)
            {

                return ErrorMessage("Erro ao trazer Usuario, " + e.Message);
            }

        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        public ActionResult CreateConfirmed(Usuario usuario)
        {
            try
            {
                var response = _usuarioAppService.Post(usuario);
                if (response.IsSuccessStatusCode)
                    return Json("Cadastro Efetuado com sucesso" );
                return ErrorMessage($"Erro ao criar usuario: {response.Content.ReadAsStringAsync().Result}");
            }
            catch (Exception e)
            {
                return ErrorMessage($"Falha ao criar usuario: {e.Message}");
            }
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var response = _usuarioAppService.GetById(id);
                if (!response.IsSuccessStatusCode)
                    return ErrorMessage("Erro ao buscar usuario");
                var usuario = JsonConvert.DeserializeObject<UsuarioViewModel>(response.Content.ReadAsStringAsync().Result);
                return View(usuario);
            }
            catch (Exception e)
            {

                return ErrorMessage("Erro ao trazer Usuario, " + e.Message);
            }

        }

        // PUT: Usuario/Edit/5
        public ActionResult EditConfirmed(Usuario usuario)
        {
            try
            {
                var response = _usuarioAppService.Put(usuario);
                if (response.IsSuccessStatusCode)
                    return Json("Cadastro Atualizado com sucesso");
                return ErrorMessage($"Erro ao editar usuario: {response.Content.ReadAsStringAsync().Result}");
            }
            catch (Exception e)
            {
                return ErrorMessage($"Falha ao editar Usuario, " + e.Message);
            }
        }

        public ActionResult BuscaGridUsuario()
        {
            try
            {
                var usuarioViewModel = _usuarioAppService.GetAll();
                if (!usuarioViewModel.IsSuccessStatusCode)
                    return ErrorMessage("Erro ao buscar usuarios.");
                var usuarios =
                    JsonConvert.DeserializeObject<IEnumerable<UsuarioViewModel>>(usuarioViewModel.Content.ReadAsStringAsync().Result);

                return View("_TabelaUsuarios", usuarios);
            }
            catch (Exception e)
            {
                return ErrorMessage("Erro ao montar tabela de usuarios, " + e.Message);
            }
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
            try
            {
                var response = _usuarioAppService.StatusUsuario(id, false);
                if (!response.IsSuccessStatusCode)
                    return ErrorMessage("Erro ao alterar o usuario");
                return RedirectToAction("BuscaGridUsuario");
            }

            catch (Exception e)
            {

                return ErrorMessage("Erro ao alterar o usuario, " + e.Message);
            }
        }
    }
}
