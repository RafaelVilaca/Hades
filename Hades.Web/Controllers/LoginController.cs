using Hades.Application.Interface;
using Hades.Domain.Entities;
using Hades.Web.ViewModels;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;

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
                    var usuarioViewModel = _usuarioAppService.GetByName(nome);
                    if (!usuarioViewModel.IsSuccessStatusCode)
                    {
                        TempData["mensagem"] = "Erro ao fazer busca no banco de dados!";
                        return RedirectToAction("Index");
                    }
                    var mostraUsuario = JsonConvert.DeserializeObject<UsuarioViewModel>(usuarioViewModel.Content.ReadAsStringAsync().Result);

                    if (mostraUsuario == null)
                    {
                        TempData["mensagem"] = "Usuario Inexistente!";
                        return RedirectToAction("Index");
                    }

                    var verifica = _usuarioAppService.SenhaFormatada(senha);

                    if (!verifica.IsSuccessStatusCode)
                    {
                        TempData["mensagem"] = "Erro ao verificar Senha!";
                        return RedirectToAction("Index");
                    }

                    var verificaSenha = JsonConvert.DeserializeObject(verifica.Content.ReadAsStringAsync().Result).ToString();

                    if (mostraUsuario.Senha != verificaSenha)
                    {
                        TempData["mensagem"] = "Senha não confere com o Usuário";
                        return RedirectToAction("Index");
                    }

                    if (!mostraUsuario.Ativo)
                    {
                        TempData["mensagem"] = "Usuario desativado, contate o Administrador!";
                        return RedirectToAction("Index");
                    }

                    if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(senha))
                    {
                        TempData["mensagem"] = "Usuario Inexistente!";
                        return RedirectToAction("Index");
                    }

                    Session["IdUsuario"] = mostraUsuario.Id;
                    Session["NomeUsuario"] = mostraUsuario.Nome;
                    Session["Administrador"] = mostraUsuario.Administrador;

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
                var request = _usuarioAppService.GetByName(usuario.Nome);
                if (!request.IsSuccessStatusCode)
                    return ErrorMessage("Erro ao verificar se usuário já existe");

                var usuarioViewModel = JsonConvert.DeserializeObject<Usuario>(request.Content.ReadAsStringAsync().Result);

                if (usuarioViewModel != null)
                    if (usuario.Nome == usuarioViewModel.Nome)
                        return ErrorMessage("Usuario já existe, insira outro nome.");

                var response = _usuarioAppService.Post(usuario);
                if (response.IsSuccessStatusCode)
                    return Json("OK");
                return ErrorMessage($"Erro ao criar usuario: {response.Content.ReadAsStringAsync().Result}");
            }
            catch (Exception e)
            {
                return ErrorMessage($"Falha ao criar usuario: {e.Message}");
            }
        }
    }
}