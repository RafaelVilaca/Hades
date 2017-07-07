using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Hades.Application.Interface;
using Hades.Domain.Entities;
using Hades.Web.ViewModels;
using Newtonsoft.Json;

namespace Hades.Web.Controllers
{
    public class LoginController : BaseController
    {
        private readonly IUsuarioAppService _usuarioAppService;

        public LoginController(IUsuarioAppService usuarioAppService)
        {
            _usuarioAppService = usuarioAppService;
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult VerificarLogin(string nome, string senha)
        {
            try
            {
                if (!string.IsNullOrEmpty(nome) || !string.IsNullOrEmpty(senha))
                {
                    var usuarioViewModel = _usuarioAppService.GetAll();
                    if (!usuarioViewModel.IsSuccessStatusCode)
                        return ErrorMessage("Erro ao buscar usuarios.");
                    var mostraUsuario =
                        JsonConvert.DeserializeObject<IEnumerable<UsuarioViewModel>>(usuarioViewModel.Content.ReadAsStringAsync().Result).First(x => x.Nome == nome && x.Senha == senha);

                    if (mostraUsuario == null)
                    {
                        TempData["mensagem"] = "Usuario Inexistente!";
                        return RedirectToAction("Index");
                    }

                    if (!mostraUsuario.Ativo)
                    {
                        TempData["mensagem"] = "Usuario desativado, contate o Administrador!";
                        return RedirectToAction("Index");
                    }

                    if (!string.IsNullOrEmpty(nome) || !string.IsNullOrEmpty(senha))
                    {
                        TempData["mensagem"] = "Usuario Inexistente!";
                        return RedirectToAction("Index");
                    }
                    return RedirectToAction("Index", "Home");
                }
                TempData["mensagem"] = "Login e Senha Incorretos!";
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["mensagem"] = "Erro ao tentar Logar, " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult CadastrarUsuario()
        {
            return RedirectToAction("Create", "Usuario");
        }

        public ActionResult CreateUsuario()
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
                    //return Json("OK", JsonRequestBehavior.AllowGet);
                    return RedirectToAction("Index", "Home");
                return ErrorMessage("Erro ao criar usuario.");
            }
            catch (Exception e)
            {
                return ErrorMessage("Erro ao criar usuario, " + e.Message);
            }
        }
    }
}