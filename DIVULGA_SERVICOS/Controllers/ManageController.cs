using System;
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
                    Body = "Your security code is: " + code
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
            ModelState.AddModelError("", "Flaha para verificar o número");
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
        public ActionResult Servicos()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var usuario = db.CAD_PESSOA.Where(x => x.UserName == userName).FirstOrDefault();
                var servico = db.CAD_CATEGORIA.Where(x => x.CD_PES_JURIDICA == usuario.Id);

                if (servico != null)
                {
                    //var cliente = db.CAD_CLIENTE.Include(x => x.CD_PESSOA == pes_juridica.CD_PESSOA);
                    return View("Gerenciamento_Servicos", servico.ToList());
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
                cAD_CATEGORIA.CD_PES_JURIDICA = User.Identity.GetUserId();
                db.CAD_CATEGORIA.Add(cAD_CATEGORIA);
                db.SaveChanges();
                return RedirectToAction("Servicos");
            }

            ViewBag.CD_PES_JURIDICA = new SelectList(db.CAD_PES_JURIDICA, "CD_PESSOA", "CD_CNPJ", cAD_CATEGORIA.CD_PES_JURIDICA);
            return View(cAD_CATEGORIA);
        }

        // GET: CAD_CATEGORIA/Details/5
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
            return View("DetalhesServico",cAD_CATEGORIA);
        }

        // GET: CAD_CATEGORIA/Edit/5
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
                cAD_CATEGORIA.CD_PES_JURIDICA = User.Identity.GetUserId();
                db.Entry(cAD_CATEGORIA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Servicos");
            }
            ViewBag.CD_PES_JURIDICA = new SelectList(db.CAD_PES_JURIDICA, "CD_PESSOA", "CD_CNPJ", cAD_CATEGORIA.CD_PES_JURIDICA);
            return View(cAD_CATEGORIA);
        }


        // GET: CAD_CATEGORIA/Delete/5
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
        [Authorize]
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
            return View("EditarPerfil" ,cAD_PES_JURIDICA);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPerfilJuridico(EditPerfilJuridico cAD_PES_JURIDICA)
        {
            if (ModelState.IsValid)
            {
                CAD_PES_JURIDICA PesJuridica = new CAD_PES_JURIDICA
                {
                   CD_PESSOA = User.Identity.GetUserId(),
                   DS_QUEM_SOMOS = cAD_PES_JURIDICA.DS_QUEM_SOMOS,
                   DS_SOBRE = cAD_PES_JURIDICA.DS_SOBRE,
                   CD_CNPJ = cAD_PES_JURIDICA.CD_CNPJ
                };
                db.CAD_PES_JURIDICA.AddOrUpdate(PesJuridica);

                CAD_PESSOA user = new CAD_PESSOA
                {
                    Id = User.Identity.GetUserId(),
                    Email = cAD_PES_JURIDICA.Email,
                    UserName = cAD_PES_JURIDICA.Email,
                    NM_NOME_PESSOA = cAD_PES_JURIDICA.NM_NOME_PESSOA
                };
                db.CAD_PESSOA.AddOrUpdate(user);
                db.SaveChanges();
                return RedirectToAction("Index");

                //db.Entry(cAD_PES_JURIDICA).State = EntityState.Modified;

            }
            return View("EditarPerfil", cAD_PES_JURIDICA);
        }

        //Fim dos métodos para gerenciamento do Perfil

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