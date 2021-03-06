﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using DIVULGA_SERVICOS.Models;
using System.Net;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Spatial;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace DIVULGA_SERVICOS.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private PRINCIPAL db;

        public ManageController()
        {
            db = new PRINCIPAL();
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Sua senha foi alterada com sucesso."
                : message == ManageMessageId.SetPasswordSuccess ? "Sua senha foi definida com sucesso."
                : message == ManageMessageId.SetTwoFactorSuccess ? "O seu provedor de autenticação de dois fatores foi definido."
                : message == ManageMessageId.Error ? "Ocorreu um erro."
                : message == ManageMessageId.AddPhoneSuccess ? "Seu número de telefone fo adicionado com sucesso"
                : message == ManageMessageId.RemovePhoneSuccess ? "Seu número de telefone foi removido com sucesso."
                : "";

            var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };
            return View(model);
        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        //
        // GET: /Manage/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Generate the token and send it
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Seu código de segurança é: " + code
                };
                await UserManager.SmsService.SendAsync(message);
            }
            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
            // Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Falha para verificar o número");
            return View(model);
        }

        //
        // GET: /Manage/RemovePhoneNumber
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "O login externo foi removido com sucesso."
                : message == ManageMessageId.Error ? "Ocorreu um erro."
                : "";
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        //Início dos métodos para o gerenciamento dos clientes
        //public ActionResult Cliente()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var userName = User.Identity.Name;
        //        var usuario = db.CAD_PESSOA.Where(x => x.UserName == userName).FirstOrDefault();
        //        var cliente = db.CAD_CLIENTE.Where(x => x.CD_PESSOA == usuario.Id);

        //        if (cliente != null)
        //        {
        //            //var cliente = db.CAD_CLIENTE.Include(x => x.CD_PESSOA == pes_juridica.CD_PESSOA);
        //            return View("Gerenciamento_Clientes", cliente.ToList());
        //        }
        //        return View();
        //    }
        //    else
        //    {
        //        ViewBag.errorMessage = "Você precisa ser um prestador de serviço e deve estar logado para acessar essa página.";
        //        return View("Error");
        //    }
        //    //var cAD_CLIENTE = db.CAD_CLIENTE.Include(c => c.CAD_PES_JURIDICA);
        //}

        // GET: CAD_CLIENTE/Details/5
        //public ActionResult Detalhes(int id)
        //{
        //    if (id < 1)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CAD_CLIENTE cAD_CLIENTE = db.CAD_CLIENTE.Where(x => x.SQ_CLIENTE == id).FirstOrDefault();
        //    if (cAD_CLIENTE == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View("Detalhes", cAD_CLIENTE);
        //}

        //// GET: CAD_CLIENTE/Create
        //public ActionResult Criar()
        //{
        //    ViewBag.CD_PESSOA = new SelectList(db.CAD_PES_JURIDICA, "CD_PESSOA", "CD_CNPJ");
        //    return View("Criar");
        //}

        // POST: CAD_CLIENTE/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Criar([Bind(Include = "NM_NOME")] CAD_CLIENTE cAD_CLIENTE)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        cAD_CLIENTE.CD_PESSOA = User.Identity.GetUserId();
        //        db.CAD_CLIENTE.Add(cAD_CLIENTE);
        //        db.SaveChanges();
        //        return RedirectToAction("Cliente");
        //    }

        //    ViewBag.CD_PESSOA = new SelectList(db.CAD_PES_JURIDICA, "CD_PESSOA", "CD_CNPJ", cAD_CLIENTE.CD_PESSOA);
        //    return View(cAD_CLIENTE);
        //}

        //// GET: CAD_CLIENTE/Edit/5
        //public ActionResult Editar(int id)
        //{
        //    if (id < 0)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CAD_CLIENTE cAD_CLIENTE = db.CAD_CLIENTE.Where(x => x.SQ_CLIENTE == id).FirstOrDefault();
        //    if (cAD_CLIENTE == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.CD_PESSOA = new SelectList(db.CAD_PES_JURIDICA, "CD_PESSOA", "CD_CNPJ", cAD_CLIENTE.CD_PESSOA);
        //    return View(cAD_CLIENTE);
        //}

        // POST: CAD_CLIENTE/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Editar([Bind(Include = "SQ_CLIENTE, NM_NOME")] CAD_CLIENTE cAD_CLIENTE)
        //{
        //    cAD_CLIENTE.CD_PESSOA = User.Identity.GetUserId();
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(cAD_CLIENTE).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Cliente");
        //    }
        //    ViewBag.CD_PESSOA = new SelectList(db.CAD_PES_JURIDICA, "CD_PESSOA", "CD_CNPJ", cAD_CLIENTE.CD_PESSOA);
        //    return View(cAD_CLIENTE);
        //}
        //Fim dos métodos para o gerenciamento dos clientes


        //Início dos métodos para o gerenciamento das dicas
        //public ActionResult Dicas()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var userName = User.Identity.Name;
        //        var usuario = db.CAD_PESSOA.Where(x => x.UserName == userName).FirstOrDefault();
        //        var dica = db.CAD_DICA.Where(x => x.CD_PESSOA == usuario.Id);

        //        if (dica != null)
        //        {
        //            //var cliente = db.CAD_CLIENTE.Include(x => x.CD_PESSOA == pes_juridica.CD_PESSOA);
        //            return View("Gerenciamento_Dicas", dica.ToList());
        //        }
        //        return View();
        //    }
        //    else
        //    {
        //        ViewBag.errorMessage = "Você precisa ser um prestador de serviço e deve estar logado para acessar essa página.";
        //        return View("Error");
        //    }
        //}

        // GET: CAD_DICA/Create
        //public ActionResult CriarDicas()
        //{
        //    ViewBag.CD_PESSOA = new SelectList(db.CAD_PES_JURIDICA, "CD_PESSOA", "CD_CNPJ");
        //    return View("CriarDicas");
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CriarDicas([Bind(Include = "NM__NOME,DS_DESCRICAO")] CAD_DICA cAD_DICA)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        cAD_DICA.CD_PESSOA = User.Identity.GetUserId();
        //        db.CAD_DICA.Add(cAD_DICA);
        //        db.SaveChanges();
        //        return RedirectToAction("Dicas");
        //    }

        //    ViewBag.CD_PESSOA = new SelectList(db.CAD_PES_JURIDICA, "CD_PESSOA", "CD_CNPJ", cAD_DICA.CD_PESSOA);
        //    return View(cAD_DICA);
        //}


        // GET: CAD_DICA/Details/5
        //public ActionResult DetalhesDicas(int id)
        //{
        //    if (id < 1)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CAD_DICA cAD_DICA = db.CAD_DICA.Where(x => x.SQ_DICA == id).FirstOrDefault();
        //    if (cAD_DICA == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View("DetalhesDica", cAD_DICA);
        //}


        //// GET: CAD_DICA/Edit/5
        //public ActionResult EditarDicas(int id)
        //{
        //    if (id < 1)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CAD_DICA cAD_DICA = db.CAD_DICA.Where(x => x.SQ_DICA == id).FirstOrDefault();
        //    if (cAD_DICA == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.CD_PESSOA = new SelectList(db.CAD_PES_JURIDICA, "CD_PESSOA", "CD_CNPJ", cAD_DICA.CD_PESSOA);
        //    return View("EditarDicas",cAD_DICA);
        //}

        // POST: CAD_DICA/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult EditarDicas([Bind(Include = "NM__NOME,DS_DESCRICAO, SQ_DICA")] CAD_DICA cAD_DICA, int id)
        //{
        //    cAD_DICA.CD_PESSOA = User.Identity.GetUserId();
        //    if (ModelState.IsValid)
        //    {
        //        cAD_DICA.SQ_DICA = id;
        //        db.Entry(cAD_DICA).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Dicas");
        //    }
        //    ViewBag.CD_PESSOA = new SelectList(db.CAD_PES_JURIDICA, "CD_PESSOA", "CD_CNPJ", cAD_DICA.CD_PESSOA);
        //    return View(cAD_DICA);
        //}
        //Fim dos métodos para o gerenciamento das dicas



        //Início dos métodos para o gerenciamento dos serviços
        [Authorize(Roles = "Prestador, Admin")]
        public ActionResult Servicos()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var servico = db.CAD_CATEGORIA.Where(x => x.CD_PES_JURIDICA == userId).ToList();

                if (servico != null)
                {
                    //var cliente = db.CAD_CLIENTE.Include(x => x.CD_PESSOA == pes_juridica.CD_PESSOA);
                    return View("Servicos", servico);
                }
                return View();
            }
            else
            {
                ViewBag.errorMessage = "Você precisa ser um prestador de serviço e deve estar logado para acessar essa página.";
                return View("Error");
            }
        }

        // GET: CAD_CATEGORIA/Create
        [Authorize(Roles = "Prestador, Admin")]
        public ActionResult CriarServico()
        {
            //ViewBag.CD_PES_JURIDICA = new SelectList(db.CAD_PES_JURIDICA, "CD_PESSOA", "CD_CNPJ");
            return View("CriarServico");
        }

        // POST: CAD_CATEGORIA/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CriarServico([Bind(Include = "NM_NOME, DS_DESCRICAO")] CAD_CATEGORIA cAD_CATEGORIA)
        {
            if (ModelState.IsValid)
            {
                var nome_servico = "";
                var ds_descricao = "";
                StringBuilder texto_final = new StringBuilder();

                nome_servico = RemoveAcento(cAD_CATEGORIA.NM_NOME);
                ds_descricao = RemoveAcento(cAD_CATEGORIA.DS_DESCRICAO);
                texto_final = texto_final.Append(cAD_CATEGORIA.NM_NOME).Append(", ").Append(cAD_CATEGORIA.DS_DESCRICAO).Append(", ").Append(nome_servico).Append(", ").Append(ds_descricao);

                cAD_CATEGORIA.CD_PES_JURIDICA = User.Identity.GetUserId();
                cAD_CATEGORIA.DS_DESCRICAO = texto_final.ToString();
                db.CAD_CATEGORIA.Add(cAD_CATEGORIA);
                db.SaveChanges();
                return RedirectToAction("Servicos");
            }

            ViewBag.CD_PES_JURIDICA = new SelectList(db.CAD_PES_JURIDICA, "CD_PESSOA", "CD_CNPJ", cAD_CATEGORIA.CD_PES_JURIDICA);
            return View(cAD_CATEGORIA);
        }

        // GET: CAD_CATEGORIA/Details/5
        [Authorize(Roles = "Prestador, Admin")]
        public ActionResult DetalhesServico(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAD_CATEGORIA cAD_CATEGORIA = db.CAD_CATEGORIA.Where(x => x.SQ_CATEGORIA == id).FirstOrDefault();
            if (cAD_CATEGORIA == null)
            {
                return HttpNotFound();
            }
            return View("DetalhesServico", cAD_CATEGORIA);
        }

        // GET: CAD_CATEGORIA/Edit/5
        [Authorize(Roles = "Prestador, Admin")]
        public ActionResult EditarServico(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAD_CATEGORIA cAD_CATEGORIA = db.CAD_CATEGORIA.Where(x => x.SQ_CATEGORIA == id).FirstOrDefault();
            if (cAD_CATEGORIA == null)
            {
                return HttpNotFound();
            }
            ViewBag.CD_PES_JURIDICA = new SelectList(db.CAD_PES_JURIDICA, "CD_PESSOA", "CD_CNPJ", cAD_CATEGORIA.CD_PES_JURIDICA);
            return View("EditarServico", cAD_CATEGORIA);
        }

        // POST: CAD_CATEGORIA/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarServico([Bind(Include = "SQ_CATEGORIA,NM_NOME,SHOW,DS_DESCRICAO")] CAD_CATEGORIA cAD_CATEGORIA)
        {
            if (ModelState.IsValid)
            {
                var nome_servico = "";
                var ds_descricao = "";
                StringBuilder texto_final = new StringBuilder();

                nome_servico = RemoveAcento(cAD_CATEGORIA.NM_NOME);
                ds_descricao = RemoveAcento(cAD_CATEGORIA.DS_DESCRICAO);
                texto_final = texto_final.Append(cAD_CATEGORIA.NM_NOME).Append(", ").Append(cAD_CATEGORIA.DS_DESCRICAO).Append(", ").Append(nome_servico).Append(", ").Append(ds_descricao);

                cAD_CATEGORIA.CD_PES_JURIDICA = User.Identity.GetUserId();
                cAD_CATEGORIA.DS_DESCRICAO = texto_final.ToString();
                db.Entry(cAD_CATEGORIA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Servicos");
            }
            ViewBag.CD_PES_JURIDICA = new SelectList(db.CAD_PES_JURIDICA, "CD_PESSOA", "CD_CNPJ", cAD_CATEGORIA.CD_PES_JURIDICA);
            return View(cAD_CATEGORIA);
        }


        // GET: CAD_CATEGORIA/Delete/5
        [Authorize(Roles = "Prestador, Admin")]
        public ActionResult DeletarServico(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAD_CATEGORIA cAD_CATEGORIA = db.CAD_CATEGORIA.Where(x => x.SQ_CATEGORIA == id).FirstOrDefault();
            if (cAD_CATEGORIA == null)
            {
                return HttpNotFound();
            }
            return View("DeletarServico", cAD_CATEGORIA);
        }

        // POST: CAD_CATEGORIA/Delete/5
        [HttpPost, ActionName("DeletarServico")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletarServicoOk(int id)
        {
            CAD_CATEGORIA cAD_CATEGORIA = db.CAD_CATEGORIA.Where(x => x.SQ_CATEGORIA == id).FirstOrDefault();
            db.CAD_CATEGORIA.Remove(cAD_CATEGORIA);
            db.SaveChanges();
            return RedirectToAction("Servicos");
        }

        //Fim dos métodos para o gerenciamento dos serviços


        //Inicio dos métodos para gerenciamento do Perfil
        [Authorize(Roles = "Prestador, Admin")]
        public ActionResult EditPerfilJuridico()
        {
            if (User.Identity.GetUserId() == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAD_PESSOA cAD_PES_JURIDICA = db.CAD_PESSOA.Find(User.Identity.GetUserId());
            if (cAD_PES_JURIDICA == null)
            {
                return HttpNotFound();
            }
            //CAD_PES_JURIDICA cAD_CATEGORIA = db.CAD_CATEGORIA.Where(x => x.SQ_CATEGORIA == id).FirstOrDefault();
            return View("EditarPerfil", cAD_PES_JURIDICA);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPerfilJuridico(EditPerfilJuridico cAD_PES_JURIDICA)
        {
            CAD_PESSOA pessoajuridica = db.CAD_PESSOA.Find(User.Identity.GetUserId());
            var newsletter = true;
            var contaativa = true;
            var divulgacao = true;
            if (ModelState.IsValid)
            {
                CAD_PES_JURIDICA usuario = db.CAD_PES_JURIDICA.Find(User.Identity.GetUserId());

                if (cAD_PES_JURIDICA.NEWSLETTER == true)
                {
                    if (pessoajuridica.NEWSLETTER == true)
                    {
                        newsletter = false;
                    }
                    else
                    {
                        newsletter = true;
                    }
                }
                else
                {
                    newsletter = pessoajuridica.NEWSLETTER;
                }

                if (cAD_PES_JURIDICA.ATIVO == true)
                {
                    if (usuario.ATIVO == true)
                    {
                        contaativa = false;
                    }
                    else
                    {
                        contaativa = true;
                    }
                }
                else
                {
                    contaativa = usuario.ATIVO;
                }

                if (cAD_PES_JURIDICA.DIVULGACAO == true)
                {
                    if (usuario.DIVULGACAO == true)
                    {
                        divulgacao = false;
                    }
                    else
                    {
                        divulgacao = true;
                    }
                }
                else
                {
                    divulgacao = usuario.DIVULGACAO;
                }

                CAD_PES_JURIDICA PesJuridica = new CAD_PES_JURIDICA
                {
                    CD_PESSOA = User.Identity.GetUserId(),
                    NM_NOME_PRESTADOR = cAD_PES_JURIDICA.NM_NOME_PRESTADOR,
                    DS_QUEM_SOMOS = cAD_PES_JURIDICA.DS_QUEM_SOMOS,
                    DS_SOBRE = cAD_PES_JURIDICA.DS_SOBRE,
                    DS_O_QUE_FAZEMOS = cAD_PES_JURIDICA.DS_O_QUE_FAZEMOS,
                    CD_CNPJ = cAD_PES_JURIDICA.CD_CNPJ,
                    TODO_DIA = usuario.TODO_DIA,
                    DIVULGACAO = divulgacao,
                    ATIVO = contaativa,
                };
                db.CAD_PES_JURIDICA.AddOrUpdate(PesJuridica);

                CAD_PESSOA user = new CAD_PESSOA
                {
                    Id = User.Identity.GetUserId(),
                    Email = cAD_PES_JURIDICA.Email,
                    UserName = cAD_PES_JURIDICA.Email,
                    NM_NOME_PESSOA = cAD_PES_JURIDICA.NM_NOME_PESSOA,
                    DT_DATA_CADASTRO = pessoajuridica.DT_DATA_CADASTRO,
                    EmailConfirmed = pessoajuridica.EmailConfirmed,
                    PasswordHash = pessoajuridica.PasswordHash,
                    SecurityStamp = pessoajuridica.SecurityStamp,
                    PhoneNumber = pessoajuridica.PhoneNumber,
                    PhoneNumberConfirmed = pessoajuridica.PhoneNumberConfirmed,
                    TwoFactorEnabled = pessoajuridica.TwoFactorEnabled,
                    LockoutEndDateUtc = pessoajuridica.LockoutEndDateUtc,
                    AccessFailedCount = pessoajuridica.AccessFailedCount,
                    LockoutEnabled = pessoajuridica.LockoutEnabled,
                    NEWSLETTER = newsletter
                };
                db.CAD_PESSOA.AddOrUpdate(user);
                db.SaveChanges();
                return RedirectToAction("Index");

                //db.Entry(cAD_PES_JURIDICA).State = EntityState.Modified;

            }
            return View("EditarPerfil", pessoajuridica);
        }

        //Fim dos métodos para gerenciamento do Perfil


        /*Início dos métodos para gerenciamento de endereços*/
        [Authorize]
        public ActionResult Enderecos()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var usuario = db.CAD_PESSOA.Where(x => x.UserName == userName).FirstOrDefault();
                var endereco = db.CAD_PES_ENDERECO.Where(x => x.CD_PESSOA == usuario.Id).ToList();
                if (endereco != null)
                {
                    //var cliente = db.CAD_CLIENTE.Include(x => x.CD_PESSOA == pes_juridica.CD_PESSOA);
                    return View("Enderecos", endereco);
                }
                else
                {
                    ViewBag.errorMessage = "Você precisa ser um prestador de serviço e deve estar logado para acessar essa página.";
                    return View("Error");
                }
            }
            else
            {
                ViewBag.errorMessage = "Você precisa ser um prestador de serviço e deve estar logado para acessar essa página.";
                return View("Error");
            }
        }


        // GET: CAD_PES_ENDERECO/Details/5
        [Authorize(Roles = "Prestador, Fornecedor, Admin")]
        public ActionResult DetalhesEndereco(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var usuario = db.CAD_PESSOA.Where(x => x.UserName == userName).FirstOrDefault();
                var endereco = db.CAD_PES_ENDERECO.Where(x => x.CD_PESSOA == usuario.Id && x.SQ_ENDERECO == id).FirstOrDefault();

                if (endereco != null)
                {
                    //var cliente = db.CAD_CLIENTE.Include(x => x.CD_PESSOA == pes_juridica.CD_PESSOA);
                    return View("DetalhesEndereco", endereco);
                }
                else
                {
                    ViewBag.errorMessage = "Não conseguimos identificar o endereço. Por favor tente novamente com um endereço válido";
                    return View("Error");
                }
            }
            else
            {
                ViewBag.errorMessage = "Você precisa ser um prestador de serviço e deve estar logado para acessar essa página.";
                return View("Error");
            }
        }

        [Authorize(Roles = "Prestador, Fornecedor, Admin")]
        public ActionResult CriarEndereco()
        {
            //ViewBag.CD_PES_JURIDICA = new SelectList(db.CAD_PES_JURIDICA, "CD_PESSOA", "CD_CNPJ");
            return View("CriarEndereco");
        }

        // POST: CAD_CATEGORIA/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CriarEndereco(CAD_PES_ENDERECO cAD_PES_ENDERECO, string LAT, string LONG)
        {
            if (ModelState.IsValid)
            {
                var userName = User.Identity.Name;
                var usuario = db.CAD_PESSOA.Where(x => x.UserName == userName).FirstOrDefault();

                //for(var i = 0; i < 1000000; i++)
                //{
                CAD_PES_ENDERECO endereco = new CAD_PES_ENDERECO
                {
                    CD_PESSOA = usuario.Id,
                    NM_CIDADE = cAD_PES_ENDERECO.NM_CIDADE,
                    NM_LOGRADOURO = cAD_PES_ENDERECO.NM_LOGRADOURO,
                    NM_BAIRRO = "NULL",
                    NUMERO = cAD_PES_ENDERECO.NUMERO,
                    NM_ESTADO = cAD_PES_ENDERECO.NM_ESTADO,
                    CD_CEP = cAD_PES_ENDERECO.CD_CEP,
                    localizacao = DbGeography.FromText("POINT(" + LAT + " " + LONG + ")")
                };
                db.CAD_PES_ENDERECO.Add(endereco);
                db.SaveChanges();
                //}
                return RedirectToAction("Enderecos");
            }

            //ViewBag.CD_PESSOA = new SelectList(db.CAD_PES_JURIDICA, "CD_PESSOA", "CD_CNPJ", cAD_PES_ENDERECO.CD_PESSOA);
            ViewBag.errorMessage = "Não foi possível adicionar o endereço. Tenha certeza de que você escolheu um endereço válido e tente novamente!";
            return View("Error");
        }

        // GET: CAD_CATEGORIA/Delete/5
        [Authorize(Roles = "Prestador, Fornecedor, Admin")]
        public ActionResult DeletarEndereco(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var usuario = db.CAD_PESSOA.Where(x => x.UserName == userName).FirstOrDefault();
                CAD_PES_ENDERECO cAD_ENDERECO = db.CAD_PES_ENDERECO.Where(x => x.CD_PESSOA == usuario.Id && x.SQ_ENDERECO == id).FirstOrDefault();
                if (cAD_ENDERECO != null)
                {
                    return View("DeletarEndereco", cAD_ENDERECO);
                }
                ViewBag.errorMessage = "Não conseguimos identificar o endereço. Por favor tente novamente com um endereço válido";
                return View("Error");

            }
            ViewBag.errorMessage = "Você precisa ser um prestador de serviço e deve estar logado para acessar essa página.";
            return View("Error");

        }

        // POST: CAD_CATEGORIA/Delete/5
        [HttpPost, ActionName("DeletarEndereco")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletarEnderecoOk(int id)
        {
            CAD_PES_ENDERECO cAD_ENDERECO = db.CAD_PES_ENDERECO.Where(x => x.SQ_ENDERECO == id).FirstOrDefault();
            db.CAD_PES_ENDERECO.Remove(cAD_ENDERECO);
            db.SaveChanges();
            return RedirectToAction("Enderecos");
        }
        /*Fim dos métodos para gerenciamento de endereços*/

        /*Início dos métodos para gerenciamento de telefones*/
        [Authorize(Roles = "Prestador, Fornecedor, Admin")]
        public ActionResult Telefones()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var usuario = db.CAD_PESSOA.Where(x => x.UserName == userName).FirstOrDefault();
                var telefone = db.CAD_PES_FONE.Where(x => x.CD_PESSOA == usuario.Id).ToList();
                if (telefone != null)
                {
                    //var cliente = db.CAD_CLIENTE.Include(x => x.CD_PESSOA == pes_juridica.CD_PESSOA);
                    return View("Telefones", telefone);
                }
                else
                {
                    ViewBag.errorMessage = "Você precisa ser um prestador de serviço e deve estar logado para acessar essa página.";
                    return View("Error");
                }
            }
            else
            {
                ViewBag.errorMessage = "Você precisa ser um prestador de serviço e deve estar logado para acessar essa página.";
                return View("Error");
            }
        }


        // GET: CAD_PES_ENDERECO/Details/5
        [Authorize(Roles = "Prestador, Fornecedor, Admin")]
        public ActionResult DetalhesTelefone(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var usuario = db.CAD_PESSOA.Where(x => x.UserName == userName).FirstOrDefault();
                var telefone = db.CAD_PES_FONE.Where(x => x.CD_PESSOA == usuario.Id && x.SQ_FONE == id).FirstOrDefault();

                if (telefone != null)
                {
                    //var cliente = db.CAD_CLIENTE.Include(x => x.CD_PESSOA == pes_juridica.CD_PESSOA);
                    return View("DetalhesTelefone", telefone);
                }
                else
                {
                    ViewBag.errorMessage = "Não conseguimos identificar o telefone. Por favor tente novamente com um telefone válido";
                    return View("Error");
                }
            }
            else
            {
                ViewBag.errorMessage = "Você precisa ser um prestador de serviço e deve estar logado para acessar essa página.";
                return View("Error");
            }
        }

        [Authorize(Roles = "Prestador, Fornecedor, Admin")]
        public ActionResult CriarTelefone()
        {
            //ViewBag.CD_PES_JURIDICA = new SelectList(db.CAD_PES_JURIDICA, "CD_PESSOA", "CD_CNPJ");
            return View("CriarTelefone");
        }

        // POST: CAD_CATEGORIA/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CriarTelefone(CAD_PES_FONE cAD_PES_FONE)
        {
            if (ModelState.IsValid)
            {
                var userName = User.Identity.Name;
                var usuario = db.CAD_PESSOA.Where(x => x.UserName == userName).FirstOrDefault();
                CAD_PES_FONE telefone = new CAD_PES_FONE
                {
                    CD_PESSOA = usuario.Id,
                    CD_FIXO = cAD_PES_FONE.CD_FIXO,
                    CD_CELULAR = cAD_PES_FONE.CD_CELULAR,
                    WHATSAPP = cAD_PES_FONE.WHATSAPP,
                    NM_OPERADORA = cAD_PES_FONE.NM_OPERADORA
                };
                db.CAD_PES_FONE.Add(telefone);
                db.SaveChanges();
                return RedirectToAction("Telefones");
            }

            //ViewBag.CD_PESSOA = new SelectList(db.CAD_PES_JURIDICA, "CD_PESSOA", "CD_CNPJ", cAD_PES_ENDERECO.CD_PESSOA);
            ViewBag.errorMessage = "Não foi possível adicionar o telefone. Tenha certeza de que você escolheu um telefone válido e tente novamente!";
            return View("Error");
        }

        // GET: CAD_CATEGORIA/Delete/5
        [Authorize(Roles = "Prestador, Fornecedor, Admin")]
        public ActionResult DeletarTelefone(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var usuario = db.CAD_PESSOA.Where(x => x.UserName == userName).FirstOrDefault();
                CAD_PES_FONE cAD_PES_FONE = db.CAD_PES_FONE.Where(x => x.CD_PESSOA == usuario.Id && x.SQ_FONE == id).FirstOrDefault();
                if (cAD_PES_FONE != null)
                {
                    return View("DeletarTelefone", cAD_PES_FONE);
                }
                ViewBag.errorMessage = "Não conseguimos identificar o telefone. Por favor tente novamente com um telefone válido";
                return View("Error");

            }
            ViewBag.errorMessage = "Você precisa ser um prestador de serviço e deve estar logado para acessar essa página.";
            return View("Error");

        }

        // POST: CAD_CATEGORIA/Delete/5
        [HttpPost, ActionName("DeletarTelefone")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletarTelefoneOk(int id)
        {
            CAD_PES_FONE cAD_PES_FONE = db.CAD_PES_FONE.Where(x => x.SQ_FONE == id).FirstOrDefault();
            db.CAD_PES_FONE.Remove(cAD_PES_FONE);
            db.SaveChanges();
            return RedirectToAction("Telefones");
        }
        /*Fim dos métodos para gerenciamento de telefones*/

        /*Início dos métodos para gerenciamento do publico alvo*/
        [Authorize(Roles = "Prestador, Admin")]
        public ActionResult publicoAlvo()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var prestador = db.CAD_PES_JURIDICA.Where(x => x.CD_PESSOA == userId).FirstOrDefault();
                if (prestador != null)
                {
                    //var cliente = db.CAD_CLIENTE.Include(x => x.CD_PESSOA == pes_juridica.CD_PESSOA);
                    return View("publicoAlvo", prestador);
                }
                else
                {
                    ViewBag.errorMessage = "Você precisa ser um prestador de serviço e deve estar logado para acessar essa página.";
                    return View("Error");
                }
            }
            else
            {
                ViewBag.errorMessage = "Você precisa ser um prestador de serviço e deve estar logado para acessar essa página.";
                return View("Error");
            }
        }

        [Authorize(Roles = "Prestador, Admin")]
        public ActionResult EditarPublicoAlvo()
        {
            if (User.Identity.GetUserId() == null)
            {
                ViewBag.errorMessage = "Você precisa ser um prestador de serviço e deve estar logado para acessar essa página.";
                return View("Error");
            }

            var userId = User.Identity.GetUserId();
            var prestador = db.CAD_PES_JURIDICA.Where(x => x.CD_PESSOA == userId).FirstOrDefault();

            //CAD_PES_JURIDICA cAD_CATEGORIA = db.CAD_CATEGORIA.Where(x => x.SQ_CATEGORIA == id).FirstOrDefault();
            return View("EditarPublicoAlvo", prestador);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarPublicoAlvo(editarPublicoAlvo publicoAlvo)
        {
            var userId = User.Identity.GetUserId();
            var publico = db.CAD_PORTE_EMPRESA.Where(x => x.CD_PESSOA == userId).FirstOrDefault();
            if (ModelState.IsValid)
            {
                CAD_PORTE_EMPRESA editarPublico = new CAD_PORTE_EMPRESA
                {
                    CD_PESSOA = userId,
                    PESSOA_FISICA = publicoAlvo.PESSOA_FISICA,
                    MICRO_EMPRESA = publicoAlvo.MICRO_EMPRESA,
                    PEQUENAS_EMPRESAS = publicoAlvo.PEQUENAS_EMPRESAS,
                    EMPRESA_GRANDE_PORTE = publicoAlvo.EMPRESA_GRANDE_PORTE
                };
                db.CAD_PORTE_EMPRESA.AddOrUpdate(editarPublico);
                db.SaveChanges();
                return RedirectToAction("publicoAlvo");
            }
            return RedirectToAction("EditarPublicoAlvo", publico);
        }
        /*Fim dos métodos para gerenciamento do publico alvo*/

        /*Fim dos métodos para gerenciamento das formas de pagamento*/
        [Authorize(Roles = "Prestador, Admin")]
        public ActionResult formasPagamento()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var prestador = db.CAD_PES_JURIDICA.Where(x => x.CD_PESSOA == userId).FirstOrDefault();
                if (prestador != null)
                {
                    //var cliente = db.CAD_CLIENTE.Include(x => x.CD_PESSOA == pes_juridica.CD_PESSOA);
                    return View("formasPagamento", prestador);
                }
                else
                {
                    ViewBag.errorMessage = "Você precisa ser um prestador de serviço e deve estar logado para acessar essa página.";
                    return View("Error");
                }
            }
            else
            {
                ViewBag.errorMessage = "Você precisa ser um prestador de serviço e deve estar logado para acessar essa página.";
                return View("Error");
            }
        }

        [Authorize(Roles = "Prestador, Admin")]
        public ActionResult EditarFormasPagamento()
        {
            if (User.Identity.GetUserId() == null)
            {
                ViewBag.errorMessage = "Você precisa ser um prestador de serviço e deve estar logado para acessar essa página.";
                return View("Error");
            }

            var userId = User.Identity.GetUserId();
            var prestador = db.CAD_PES_JURIDICA.Where(x => x.CD_PESSOA == userId).FirstOrDefault();

            //CAD_PES_JURIDICA cAD_CATEGORIA = db.CAD_CATEGORIA.Where(x => x.SQ_CATEGORIA == id).FirstOrDefault();
            return View("EditarFormasPagamento", prestador);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarFormasPagamento(editarFormasPagamento formasPagamento)
        {
            var userId = User.Identity.GetUserId();
            var pagamento = db.CAD_FORMA_PAGAMENTO.Where(x => x.CD_PESSOA == userId).FirstOrDefault();
            if (ModelState.IsValid)
            {
                CAD_FORMA_PAGAMENTO editarFormasPagamento = new CAD_FORMA_PAGAMENTO
                {
                    CD_PESSOA = userId,
                    DINHEIRO = formasPagamento.DINHEIRO,
                    CHEQUE = formasPagamento.CHEQUE,
                    DEBITO = formasPagamento.DEBITO,
                    CREDITO = formasPagamento.CREDITO,
                    OUTROS = formasPagamento.OUTROS
                };
                db.CAD_FORMA_PAGAMENTO.AddOrUpdate(editarFormasPagamento);
                db.SaveChanges();
                return RedirectToAction("formasPagamento");
            }
            return RedirectToAction("EditarFormasPagamento", pagamento);
        }
        /*Fim dos métodos para gerenciamento das formas de pagamento*/


        /*Início dos métodos para gerenciamento dos horários de atendimento*/
        [Authorize(Roles ="Prestador, Admin")]
        public ActionResult horarioAtendimento()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var prestador = db.CAD_PES_JURIDICA.Where(x => x.CD_PESSOA == userId).FirstOrDefault();
                if (prestador != null)
                {
                    //var cliente = db.CAD_CLIENTE.Include(x => x.CD_PESSOA == pes_juridica.CD_PESSOA);
                    return View("horarioAtendimento", prestador);
                }
                else
                {
                    ViewBag.errorMessage = "Você precisa ser um prestador de serviço e deve estar logado para acessar essa página.";
                    return View("Error");
                }
            }
            else
            {
                ViewBag.errorMessage = "Você precisa ser um prestador de serviço e deve estar logado para acessar essa página.";
                return View("Error");
            }
        }


        [Authorize(Roles = "Prestador, Admin")]
        public ActionResult EditarHorarioAtendimento()
        {
            if (User.Identity.GetUserId() == null)
            {
                ViewBag.errorMessage = "Você precisa ser um prestador de serviço e deve estar logado para acessar essa página.";
                return View("Error");
            }

            var userId = User.Identity.GetUserId();
            var horarios = db.CAD_HORA_ATENDIMENTO.Where(x => x.CD_PES_JURIDICA == userId).ToList();

            //CAD_PES_JURIDICA cAD_CATEGORIA = db.CAD_CATEGORIA.Where(x => x.SQ_CATEGORIA == id).FirstOrDefault();
            return View("EditarHorarioAtendimento", horarios);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarHorarioAtendimento(EditarHorarioAtendimento cAD_HORA_ATENDIMENTO, bool tododia)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var horarios = db.CAD_HORA_ATENDIMENTO.Where(x => x.CD_PES_JURIDICA == userId).ToList();
                var prestador = db.CAD_PES_JURIDICA.Where(x => x.CD_PESSOA == userId).FirstOrDefault();
                if (tododia != prestador.TODO_DIA)
                {
                    CAD_PES_JURIDICA cAD_PES_JURIDICA = new CAD_PES_JURIDICA
                    {
                        CD_PESSOA = prestador.CD_PESSOA,
                        NM_NOME_PRESTADOR = prestador.NM_NOME_PRESTADOR,
                        CD_CNPJ = prestador.CD_CNPJ,
                        DS_SOBRE = prestador.DS_SOBRE,
                        DS_QUEM_SOMOS = prestador.DS_QUEM_SOMOS,
                        TODO_DIA = tododia,
                        ACEITE_CONTRATO = prestador.ACEITE_CONTRATO,
                        DIVULGACAO = prestador.DIVULGACAO,
                        ATIVO = prestador.ATIVO,
                    };
                    db.CAD_PES_JURIDICA.AddOrUpdate(cAD_PES_JURIDICA);
                    db.SaveChanges();
                }
                if(tododia == false)
                {
                    if(horarios.Count > 0)
                    {
                        db.CAD_HORA_ATENDIMENTO.RemoveRange(horarios);
                        //horarios.RemoveAll(x => x.CD_PES_JURIDICA == userId);
                        db.SaveChanges();
                    }
                    CAD_HORA_ATENDIMENTO horaAtendimentoDomingo = new CAD_HORA_ATENDIMENTO
                    {
                        CD_PES_JURIDICA = userId,
                        DIA_SEMANA = 0,
                        HORA_INICIO = cAD_HORA_ATENDIMENTO.DOMINGO_HORA_INICIO,
                        HORA_FIM = cAD_HORA_ATENDIMENTO.DOMINGO_HORA_FIM
                    };
                    db.CAD_HORA_ATENDIMENTO.Add(horaAtendimentoDomingo);
                    db.SaveChanges();
                    //transacao.Commit();

                    CAD_HORA_ATENDIMENTO horaAtendimentoSegunda = new CAD_HORA_ATENDIMENTO
                    {
                        CD_PES_JURIDICA = userId,
                        DIA_SEMANA = 1,
                        HORA_INICIO = cAD_HORA_ATENDIMENTO.SEGUNDA_HORA_INICIO,
                        HORA_FIM = cAD_HORA_ATENDIMENTO.SEGUNDA_HORA_FIM
                    };
                    db.CAD_HORA_ATENDIMENTO.Add(horaAtendimentoSegunda);
                    db.SaveChanges();
                    //transacao.Commit();

                    CAD_HORA_ATENDIMENTO horaAtendimentoTerca = new CAD_HORA_ATENDIMENTO
                    {
                        CD_PES_JURIDICA = userId,
                        DIA_SEMANA = 2,
                        HORA_INICIO = cAD_HORA_ATENDIMENTO.TERCA_HORA_INICIO,
                        HORA_FIM = cAD_HORA_ATENDIMENTO.TERCA_HORA_FIM
                    };
                    db.CAD_HORA_ATENDIMENTO.Add(horaAtendimentoTerca);
                    db.SaveChanges();
                    //transacao.Commit();

                    CAD_HORA_ATENDIMENTO horaAtendimentoQuarta = new CAD_HORA_ATENDIMENTO
                    {
                        CD_PES_JURIDICA = userId,
                        DIA_SEMANA = 3,
                        HORA_INICIO = cAD_HORA_ATENDIMENTO.QUARTA_HORA_INICIO,
                        HORA_FIM = cAD_HORA_ATENDIMENTO.QUARTA_HORA_FIM
                    };
                    db.CAD_HORA_ATENDIMENTO.Add(horaAtendimentoQuarta);
                    db.SaveChanges();
                    //transacao.Commit();

                    CAD_HORA_ATENDIMENTO horaAtendimentoQuinta = new CAD_HORA_ATENDIMENTO
                    {
                        CD_PES_JURIDICA = userId,
                        DIA_SEMANA = 4,
                        HORA_INICIO = cAD_HORA_ATENDIMENTO.QUINTA_HORA_INICIO,
                        HORA_FIM = cAD_HORA_ATENDIMENTO.QUINTA_HORA_FIM
                    };
                    db.CAD_HORA_ATENDIMENTO.Add(horaAtendimentoQuinta);
                    db.SaveChanges();
                    //transacao.Commit();

                    CAD_HORA_ATENDIMENTO horaAtendimentoSexta = new CAD_HORA_ATENDIMENTO
                    {
                        CD_PES_JURIDICA = userId,
                        DIA_SEMANA = 5,
                        HORA_INICIO = cAD_HORA_ATENDIMENTO.SEXTA_HORA_INICIO,
                        HORA_FIM = cAD_HORA_ATENDIMENTO.SEXTA_HORA_FIM
                    };
                    db.CAD_HORA_ATENDIMENTO.Add(horaAtendimentoSexta);
                    db.SaveChanges();
                    //transacao.Commit();

                    CAD_HORA_ATENDIMENTO horaAtendimentoSabado = new CAD_HORA_ATENDIMENTO
                    {
                        CD_PES_JURIDICA = userId,
                        DIA_SEMANA = 6,
                        HORA_INICIO = cAD_HORA_ATENDIMENTO.SABADO_HORA_INICIO,
                        HORA_FIM = cAD_HORA_ATENDIMENTO.SABADO_HORA_FIM
                    };
                    db.CAD_HORA_ATENDIMENTO.Add(horaAtendimentoSabado);
                    db.SaveChanges();
                    return RedirectToAction("horarioAtendimento");

                }
            }
            return RedirectToAction("horarioAtendimento");
        }

        /*Fim dos métodos para gerenciamento dos horários de atendimento*/





        /*Fim dos métodos para gerenciamento do prestador de serviços*/

        /*Início dos métodos para gerenciamento de produtos do fornecedor*/

        [Authorize(Roles = "Fornecedor, Admin")]
        public ActionResult Produtos()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var usuario = db.CAD_PES_FORNECEDOR.Where(x => x.CD_PESSOA == userId).FirstOrDefault();
                var produtos = db.CAD_PRODUTO_FORNECEDOR.Where(x => x.CD_PESSOA == usuario.CD_PESSOA).ToList();

                if (produtos != null)
                {
                    //var cliente = db.CAD_CLIENTE.Include(x => x.CD_PESSOA == pes_juridica.CD_PESSOA);
                    return View("Produtos", produtos);
                }
                else
                {
                    ViewBag.errorMessage = "Você precisa ser um fornecedor e deve estar logado para acessar essa página.";
                    return View("Error");
                }
            }
            else
            {
                ViewBag.errorMessage = "Você precisa ser um fornecedor e deve estar logado para acessar essa página.";
                return View("Error");
            }
        }

        [Authorize(Roles = "Fornecedor, Admin")]
        public ActionResult CriarProduto()
        {
            //ViewBag.CD_PES_JURIDICA = new SelectList(db.CAD_PES_JURIDICA, "CD_PESSOA", "CD_CNPJ");
            var userId = User.Identity.GetUserId();
            //var usuario = db.CAD_PES_FORNECEDOR.Where(x => x.CD_PESSOA == userId);
            var cidade = db.CAD_CIDADES_DIVULGA_FORNECEDOR.Where(x => x.CD_PESSOA == userId).FirstOrDefault();
            if (cidade == null)
            {
                ViewBag.errorMessage = "Não esqueça de cadastrar as cidades onde quer divulgar seus produtos!";
                return View("CriarProduto");
            }
            return View("CriarProduto");
        }

        // POST: CAD_CATEGORIA/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CriarProduto(CAD_PRODUTO_FORNECEDOR cAD_PRODUTO_FORNECEDOR)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var usuario = db.CAD_PES_FORNECEDOR.Where(x => x.CD_PESSOA == userId).FirstOrDefault();
                CAD_PRODUTO_FORNECEDOR produto = new CAD_PRODUTO_FORNECEDOR
                {
                    CD_PESSOA = usuario.CD_PESSOA,
                    NM_PRODUTO = cAD_PRODUTO_FORNECEDOR.NM_PRODUTO,
                    DS_DESCRICAO = cAD_PRODUTO_FORNECEDOR.DS_DESCRICAO,
                    VALOR_PRODUTO = cAD_PRODUTO_FORNECEDOR.VALOR_PRODUTO,
                    DT_CRIACAO = DateTime.Today,
                    ATIVO = cAD_PRODUTO_FORNECEDOR.ATIVO,
                    TAGS = cAD_PRODUTO_FORNECEDOR.TAGS
                };
                db.CAD_PRODUTO_FORNECEDOR.Add(produto);
                db.SaveChanges();
                return RedirectToAction("Produtos");
            }

            //ViewBag.CD_PESSOA = new SelectList(db.CAD_PES_JURIDICA, "CD_PESSOA", "CD_CNPJ", cAD_PES_ENDERECO.CD_PESSOA);
            ViewBag.errorMessage = "Não foi possível adicionar o produto. Tenha certeza de que você escolheu um produto válido e tente novamente!";
            return View("Error");
        }

        [Authorize(Roles = "Fornecedor, Admin")]
        public ActionResult DeletarProduto(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var usuario = db.CAD_PES_FORNECEDOR.Where(x => x.CD_PESSOA == userId).FirstOrDefault();

                CAD_PRODUTO_FORNECEDOR cAD_PRODUTO_FORNECEDOR = db.CAD_PRODUTO_FORNECEDOR.Where(x => x.CD_PESSOA == usuario.CD_PESSOA && x.SQ_PRODUTO == id).FirstOrDefault();
                if (cAD_PRODUTO_FORNECEDOR != null)
                {
                    return View("DeletarProduto", cAD_PRODUTO_FORNECEDOR);
                }
                ViewBag.errorMessage = "Não conseguimos identificar o produto. Por favor tente novamente com um produto válido.";
                return View("Error");

            }
            ViewBag.errorMessage = "Você precisa ser um fornecedor e deve estar logado para acessar essa página.";
            return View("Error");

        }

        [HttpPost, ActionName("DeletarProduto")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletarProdutoOk(int id)
        {
            CAD_PRODUTO_FORNECEDOR cAD_PRODUTO_FORNECEDOR = db.CAD_PRODUTO_FORNECEDOR.Where(x => x.SQ_PRODUTO == id).FirstOrDefault();
            db.CAD_PRODUTO_FORNECEDOR.Remove(cAD_PRODUTO_FORNECEDOR);
            db.SaveChanges();
            return RedirectToAction("Produtos");
        }

        [Authorize(Roles = "Fornecedor, Admin")]
        public ActionResult DetalhesProduto(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var usuario = db.CAD_PRODUTO_FORNECEDOR.Where(x => x.CD_PESSOA == userId).FirstOrDefault();
                var produto = db.CAD_PRODUTO_FORNECEDOR.Where(x => x.CD_PESSOA == usuario.CD_PESSOA && x.SQ_PRODUTO == id).FirstOrDefault();

                if (produto != null)
                {
                    //var cliente = db.CAD_CLIENTE.Include(x => x.CD_PESSOA == pes_juridica.CD_PESSOA);
                    return View("DetalhesProduto", produto);
                }
                else
                {
                    ViewBag.errorMessage = "Não conseguimos identificar o produto. Por favor tente novamente com um produto válido";
                    return View("Error");
                }
            }
            else
            {
                ViewBag.errorMessage = "Você precisa ser um fornecedor e deve estar logado para acessar essa página.";
                return View("Error");
            }
        }

        [Authorize(Roles = "Fornecedor, Admin")]
        public ActionResult EditarProduto(int id)
        {
            if (User.Identity.GetUserId() == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userId = User.Identity.GetUserId();
            var produto = db.CAD_PRODUTO_FORNECEDOR.Where(x => x.CD_PESSOA == userId && x.SQ_PRODUTO == id).FirstOrDefault();

            //CAD_PRODUTO_FORNECEDOR cAD_PRODUTO_FORNECEDOR = db.CAD_PRODUTO_FORNECEDOR.Find(User.Identity.GetUserId());
            if (produto == null)
            {
                return HttpNotFound();
            }
            //CAD_PES_JURIDICA cAD_CATEGORIA = db.CAD_CATEGORIA.Where(x => x.SQ_CATEGORIA == id).FirstOrDefault();
            return View("EditarProduto", produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarProduto(CAD_PRODUTO_FORNECEDOR cAD_PRODUTO_FORNECEDOR, int id)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var cad_produto = db.CAD_PRODUTO_FORNECEDOR.Where(x => x.CD_PESSOA == userId && x.SQ_PRODUTO == id).FirstOrDefault();
                //CAD_PRODUTO_FORNECEDOR cad_produto = db.CAD_PRODUTO_FORNECEDOR.Find(User.Identity.GetUserId());

                CAD_PRODUTO_FORNECEDOR produto = new CAD_PRODUTO_FORNECEDOR
                {
                    CD_PESSOA = cad_produto.CD_PESSOA,
                    SQ_PRODUTO = cad_produto.SQ_PRODUTO,
                    NM_PRODUTO = cAD_PRODUTO_FORNECEDOR.NM_PRODUTO,
                    DS_DESCRICAO = cAD_PRODUTO_FORNECEDOR.DS_DESCRICAO,
                    VALOR_PRODUTO = cAD_PRODUTO_FORNECEDOR.VALOR_PRODUTO,
                    DT_CRIACAO = DateTime.Today,
                    ATIVO = cAD_PRODUTO_FORNECEDOR.ATIVO,
                    TAGS = cAD_PRODUTO_FORNECEDOR.TAGS
                };
                db.CAD_PRODUTO_FORNECEDOR.AddOrUpdate(produto);
                db.SaveChanges();
                return RedirectToAction("Produtos");

                //db.Entry(cAD_PES_JURIDICA).State = EntityState.Modified;

            }
            return View("EditarProduto", cAD_PRODUTO_FORNECEDOR);
        }

        [Authorize(Roles = "Fornecedor, Admin")]
        public ActionResult CidadesFornecedor()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var cidades = db.CAD_CIDADES_DIVULGA_FORNECEDOR.Where(x => x.CD_PESSOA == userId).ToList();
                if (cidades != null)
                {
                    //var cliente = db.CAD_CLIENTE.Include(x => x.CD_PESSOA == pes_juridica.CD_PESSOA);
                    return View("CidadesFornecedor", cidades);
                }
                else
                {
                    ViewBag.errorMessage = "Você precisa cadastrar uma cidade!";
                    return View("CadastrarCidade");
                }
            }
            else
            {
                ViewBag.errorMessage = "Você precisa ser um fornecedor e deve estar logado para acessar essa página.";
                return View("Error");
            }
        }



        [Authorize(Roles = "Fornecedor, Admin")]
        public ActionResult CadastrarCidade(string mensagem = "")
        {
            //ViewBag.CD_PES_JURIDICA = new SelectList(db.CAD_PES_JURIDICA, "CD_PESSOA", "CD_CNPJ");
            return View("CadastrarCidade");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastrarCidade(CAD_CIDADES_DIVULGA_FORNECEDOR cAD_CIDADES_DIVULGA_FORNECEDOR)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var usuario = db.CAD_PES_FORNECEDOR.Where(x => x.CD_PESSOA == userId).FirstOrDefault();
                if (usuario != null)
                {
                    if (cAD_CIDADES_DIVULGA_FORNECEDOR.BRASIL == true)
                    {
                        var cidade = "Brasil";
                        CAD_CIDADES_DIVULGA_FORNECEDOR brasil = new CAD_CIDADES_DIVULGA_FORNECEDOR
                        {
                            CD_PESSOA = usuario.CD_PESSOA,
                            NM_CIDADE = cidade,
                            NM_ESTADO = cAD_CIDADES_DIVULGA_FORNECEDOR.NM_ESTADO,
                            BRASIL = cAD_CIDADES_DIVULGA_FORNECEDOR.BRASIL
                        };
                        db.CAD_CIDADES_DIVULGA_FORNECEDOR.Add(brasil);
                        db.SaveChanges();
                    }
                    else
                    {
                        CAD_CIDADES_DIVULGA_FORNECEDOR endereco = new CAD_CIDADES_DIVULGA_FORNECEDOR
                        {
                            CD_PESSOA = usuario.CD_PESSOA,
                            NM_CIDADE = cAD_CIDADES_DIVULGA_FORNECEDOR.NM_CIDADE,
                            NM_ESTADO = cAD_CIDADES_DIVULGA_FORNECEDOR.NM_ESTADO,
                            BRASIL = cAD_CIDADES_DIVULGA_FORNECEDOR.BRASIL,
                            DILGAR_ESTADO = cAD_CIDADES_DIVULGA_FORNECEDOR.DILGAR_ESTADO
                        };
                        db.CAD_CIDADES_DIVULGA_FORNECEDOR.Add(endereco);
                        db.SaveChanges();
                    }
                }
                else
                {
                    ViewBag.errorMessage = "Você precisa ser um fornecedor e deve estar logado para acessar essa página.";
                    return View("Error");
                }
                return RedirectToAction("CidadesFornecedor");
            }

            //ViewBag.CD_PESSOA = new SelectList(db.CAD_PES_JURIDICA, "CD_PESSOA", "CD_CNPJ", cAD_PES_ENDERECO.CD_PESSOA);
            ViewBag.errorMessage = "Não foi possível adicionar o endereço. Tenha certeza de que você escolheu um endereço válido e tente novamente!";
            return View("Error");
        }

        [Authorize(Roles = "Fornecedor, Admin")]
        public ActionResult DeletarCidade(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var usuario = db.CAD_PES_FORNECEDOR.Where(x => x.CD_PESSOA == userId).FirstOrDefault();
                CAD_CIDADES_DIVULGA_FORNECEDOR cAD_CIDADES_DIVULGA_FORNECEDOR = db.CAD_CIDADES_DIVULGA_FORNECEDOR.Where(x => x.CD_PESSOA == usuario.CD_PESSOA && x.SQ_CIDADE == id).FirstOrDefault();
                if (cAD_CIDADES_DIVULGA_FORNECEDOR != null)
                {
                    return View("DeletarCidade", cAD_CIDADES_DIVULGA_FORNECEDOR);
                }
                ViewBag.errorMessage = "Não conseguimos identificar a cidade. Por favor tente novamente com uma cidade válido";
                return View("Error");

            }
            ViewBag.errorMessage = "Você precisa ser um fornecedor e deve estar logado para acessar essa página.";
            return View("Error");

        }

        // POST: CAD_CATEGORIA/Delete/5
        [HttpPost, ActionName("DeletarCidade")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletarCidadeOk(int id)
        {
            CAD_CIDADES_DIVULGA_FORNECEDOR cAD_CIDADES_DIVULGA_FORNECEDOR = db.CAD_CIDADES_DIVULGA_FORNECEDOR.Where(x => x.SQ_CIDADE == id).FirstOrDefault();
            db.CAD_CIDADES_DIVULGA_FORNECEDOR.Remove(cAD_CIDADES_DIVULGA_FORNECEDOR);
            db.SaveChanges();
            return RedirectToAction("CidadesFornecedor");
        }

        [Authorize(Roles = "Fornecedor, Admin")]
        public ActionResult DetalhesCidade(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var usuario = db.CAD_PES_FORNECEDOR.Where(x => x.CD_PESSOA == userId).FirstOrDefault();
                var cidade = db.CAD_CIDADES_DIVULGA_FORNECEDOR.Where(x => x.CD_PESSOA == usuario.CD_PESSOA && x.SQ_CIDADE == id).FirstOrDefault();

                if (cidade != null)
                {
                    //var cliente = db.CAD_CLIENTE.Include(x => x.CD_PESSOA == pes_juridica.CD_PESSOA);
                    return View("DetalhesCidade", cidade);
                }
                else
                {
                    ViewBag.errorMessage = "Não conseguimos identificar a cidade. Por favor tente novamente com uma cidade válida";
                    return View("Error");
                }
            }
            else
            {
                ViewBag.errorMessage = "Você precisa ser um fornecedor e deve estar logado para acessar essa página.";
                return View("Error");
            }
        }
        /*Fim dos métodos para gerenciamento de produtos do fornecedor*/


        /*Início dos métodos para gerenciamento do perfil do fornecedor*/
        
        [Authorize(Roles = "Fornecedor, Admin")]
        public ActionResult EditarPerfilFornecedor()
        {
            if (User.Identity.GetUserId() == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAD_PESSOA cAD_PES_FORNECEDOR = db.CAD_PESSOA.Find(User.Identity.GetUserId());
            if (cAD_PES_FORNECEDOR == null)
            {
                ViewBag.errorMessage = "Não conseguimos identificar seu perfil. Por favor, recarregue a página ou tente novamente mais tarde!";
                return View("Error");
            }
            //CAD_PES_JURIDICA cAD_CATEGORIA = db.CAD_CATEGORIA.Where(x => x.SQ_CATEGORIA == id).FirstOrDefault();
            return View("EditPerfilFornecedor", cAD_PES_FORNECEDOR);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarPerfilFornecedor(EditarPerfilFornecedor cAD_PES_FORNECEDOR)
        {
            CAD_PESSOA fornecedorAntes = db.CAD_PESSOA.Find(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                CAD_PES_FORNECEDOR usuarioFornecedor = db.CAD_PES_FORNECEDOR.Find(User.Identity.GetUserId());

                CAD_PES_FORNECEDOR fornecedorDepois = new CAD_PES_FORNECEDOR
                {
                    CD_PESSOA = fornecedorAntes.Id,
                    CD_CNPJ = cAD_PES_FORNECEDOR.CD_CNPJ,
                    ATIVO = cAD_PES_FORNECEDOR.ATIVO
                };
                db.CAD_PES_FORNECEDOR.AddOrUpdate(fornecedorDepois);

                CAD_PESSOA user = new CAD_PESSOA
                {
                    Id = fornecedorAntes.Id,
                    Email = cAD_PES_FORNECEDOR.Email,
                    UserName = cAD_PES_FORNECEDOR.Email,
                    NM_NOME_PESSOA = cAD_PES_FORNECEDOR.NM_NOME_PESSOA,
                    DT_DATA_CADASTRO = fornecedorAntes.DT_DATA_CADASTRO,
                    EmailConfirmed = fornecedorAntes.EmailConfirmed,
                    PasswordHash = fornecedorAntes.PasswordHash,
                    SecurityStamp = fornecedorAntes.SecurityStamp,
                    PhoneNumber = fornecedorAntes.PhoneNumber,
                    PhoneNumberConfirmed = fornecedorAntes.PhoneNumberConfirmed,
                    TwoFactorEnabled = fornecedorAntes.TwoFactorEnabled,
                    LockoutEndDateUtc = fornecedorAntes.LockoutEndDateUtc,
                    AccessFailedCount = fornecedorAntes.AccessFailedCount,
                    LockoutEnabled = fornecedorAntes.LockoutEnabled,
                    NEWSLETTER = fornecedorAntes.NEWSLETTER,
                };
                db.CAD_PESSOA.AddOrUpdate(user);
                db.SaveChanges();
                return RedirectToAction("Index");

                //db.Entry(cAD_PES_JURIDICA).State = EntityState.Modified;

            }
            return View("EditPerfilFornecedor", fornecedorAntes);
        }

        /*Fim dos métodos para gerenciamento do perfil do fornecedor*/

        /*Início dos métodos que mostram o fornecedor e seus produtos*/

        [Authorize(Roles = "Prestador, Admin")]
        [HttpGet]
        public ActionResult fornecedorProdutos(string userId = "")
        {
            if(userId != "")
            {
                var fornecedor = db.CAD_PES_FORNECEDOR.Where(x => x.CD_PESSOA == userId).FirstOrDefault();
                return View("fornecedorProdutos", fornecedor);
            }
            return RedirectToAction("MeusFornecedores");
        }
        /*Fim dos métodos que mostram o fornecedor e seus produtos*/


        /*Início do método que mostra os fornecedores para cada tipo de prestador de serviço*/
        [Authorize(Roles = "Prestador, Admin")]
        public ActionResult MeusFornecedores()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var usuario = db.CAD_PES_JURIDICA.Where(x => x.CD_PESSOA == userId).FirstOrDefault();
                var enderecosPrestador = db.CAD_PES_ENDERECO.Where(x => x.CD_PESSOA == userId);
                var servicos = db.CAD_CATEGORIA.Where(x => x.CD_PES_JURIDICA == userId).ToList();
                var produtoTemp = new List<CAD_PRODUTO_FORNECEDOR>();
                var produtos = new List<CAD_PRODUTO_FORNECEDOR>();
                var cidades = new List<CAD_CIDADES_DIVULGA_FORNECEDOR>();
                var fornecedores = new List<CAD_PES_FORNECEDOR>();
                var status = 0;
                var count = 0;

                fornecedores = db.CAD_PES_FORNECEDOR.Where(x => x.CAD_PESSOA.CAD_PES_FORNECEDOR.ATIVO == true).ToList();
                for (int i = fornecedores.Count - 1; i >= 0; i--)
                {
                    var idfornecedor = fornecedores[i].CD_PESSOA;
                    cidades = db.CAD_CIDADES_DIVULGA_FORNECEDOR.Where(x => x.CD_PESSOA == idfornecedor).ToList();
                    foreach (var ItemEndereco in enderecosPrestador)
                    {

                        foreach (var cidade in cidades)
                        {
                            if (ItemEndereco.NM_CIDADE != cidade.NM_CIDADE && cidade.BRASIL == false && cidade.DILGAR_ESTADO == false)
                            {
                                count = count + 1;
                            }
                        }
                        count = count + 1;
                    }
                    if (count == (cidades.Count + enderecosPrestador.Count()))
                    {
                        fornecedores.Remove(fornecedores[i]);
                    }
                    if (fornecedores.Count() == 0)
                    {
                        status = 1;
                        break;
                    }
                }
                if (status == 1)
                {
                    return View("Fornecedores", produtos);
                }
                else
                {
                    foreach (var item in servicos)
                    {
                        string[] words = item.DS_DESCRICAO.Split(' ');

                        foreach (var word in words)
                        {
                            if (word != "de" || word != "para")
                            {
                                foreach (var forn in fornecedores)
                                {
                                    produtoTemp = db.CAD_PRODUTO_FORNECEDOR.Where(x =>
                                     (x.TAGS.Contains(word)) ||
                                     (x.TAGS.Contains(item.NM_NOME)) ||
                                     (x.NM_PRODUTO.Contains(word)) ||
                                     (x.TAGS.Contains(usuario.CAD_PESSOA.NM_NOME_PESSOA))).ToList();
                                }

                                if (produtos.Any((x => produtoTemp.Contains(x))))
                                {

                                }
                                else
                                {
                                    produtos.AddRange(produtoTemp);
                                }
                            }
                        }
                    }
                    return View("Fornecedores", produtos);
                }
            }
            else
            {
                ViewBag.errorMessage = "Você precisa ser um fornecedor e deve estar logado para acessar essa página.";
                return View("Error");
            }
        }
        /*Fim do método que mostra os fornecedores para cada tipo de prestador de serviço*/


        /*Início do método que deleta o usuário criado pelas redes sociais*/
        [Authorize(Roles = "Usuario, Admin")]
        public ActionResult DeletarUsuario()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var usuario = db.CAD_PESSOA.Where(x => x.Id == userId).FirstOrDefault();
                if (usuario != null)
                {
                    return View("DeletarUsuario", usuario);
                }
                ViewBag.errorMessage = "Não conseguimos identificar suas informações. Por favor tente novamente!";
                return View("Error");

            }
            ViewBag.errorMessage = "Você precisa ser um usuário e deve estar logado para acessar essa página.";
            return View("Error");

        }

        
        [HttpPost, ActionName("DeletarUsuario")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletarUsuarioOk()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                CAD_PESSOA pessoa = db.CAD_PESSOA.Where(x => x.Id == userId).FirstOrDefault();
                CAD_PES_USUARIO usuario = db.CAD_PES_USUARIO.Where(x => x.CD_PESSOA == userId).FirstOrDefault();
                var avaliacoes = db.CAD_AVALIACAO.Where(x => x.NM_NOME_AVALIADOR == pessoa.UserName).ToList();
                if (avaliacoes.Count != 0)
                {
                    foreach (var avaliacao in avaliacoes)
                    {
                        db.CAD_AVALIACAO.Remove(avaliacao);
                        db.SaveChanges();
                    }
                }
                db.CAD_PES_USUARIO.Remove(usuario);
                db.SaveChanges();
                db.CAD_PESSOA.Remove(pessoa);
                db.SaveChanges();
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return Redirect("~/Home");
            }
            return View("Index");
        }

        /*Fim do método que deleta o usuário criado pelas redes sociais*/

        public static string RemoveAcento(String texto)
        {
            String normalizedString = texto.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++)
            {
                Char c = normalizedString[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString().ToLower();
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        #endregion
    }
}