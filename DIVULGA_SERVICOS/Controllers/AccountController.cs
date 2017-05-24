using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using DIVULGA_SERVICOS.Models;
using System.Data.Entity;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Principal;
using System.Data.Entity.Spatial;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Web.Security;

namespace DIVULGA_SERVICOS.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private PRINCIPAL db = new PRINCIPAL();

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            //CAD_PESSOA userModel = new CAD_PESSOA();
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //var user = await UserManager.FindByNameAsync(model.Email);
            //if (user != null)
            //{
            //    if (!await UserManager.IsEmailConfirmedAsync(user.Id))
            //    {
            //        ViewBag.errorMessage = "Você precisa confirmar seu email antes de logar pela primeira vez.";
            //        return View("Error");
            //    }
            //}

            //CAD_PESSOA user = null;
            //db.CAD_PESSOA.Single(u => u.Email.ToString().Equals(model.Email));
            //var user = db.CAD_PESSOA.SqlQuery("SELECT * FROM AspNetUsers WHERE Email = '" + model.Email + "'");
            //user.First<CAD_PESSOA>().Email;
            //ModelState.AddModelError("", user.Email);
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true

            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: true);
            //  if(result == 0)
            //{
            //    CAD_PESSOA curUser = db.CAD_PESSOA.SqlQuery("SELECT * FROM AspNetUsers WHERE Email = '" + model.Email + "'").SingleOrDefault<CAD_PESSOA>();
            //    if (curUser != null)
            //    {
            //        var claims = new List<Claim>();
            //        claims.Add(new Claim("Nome", curUser.NM_NOME_PESSOA));
            //        claims.Add(new Claim("Apelido", curUser.DS_APELIDO_SITE));
            //        claims.Add(new Claim("Email", curUser.Email));

            //        var id = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            //        var ctx = Request.GetOwinContext();
            //        AuthenticationManager.SignIn(id);
            //        //ViewBag.Nome = curUser.NM_NOME_PESSOA;
            //        return RedirectToLocal(returnUrl);
            //    }
            //}

            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Tentativa de login inválida.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterPrestadorViewModel model)
        {
            //string link_site;
            //long indicacao;
            var ExisteEndereco = new List<CAD_PES_ENDERECO>();
            var lat = "";
            var lng = "";
            if (ModelState.IsValid && model.ACEITE_CONTRATO == true)
            {
                var usuarioExistente = db.CAD_PESSOA.Where(x => x.Email == model.UserName).ToList();
                if (usuarioExistente.Count() == 0)
                {
                    var user = new ApplicationUser
                    {
                        NM_NOME_PESSOA = model.NM_NOME_PESSOA,
                        UserName = model.UserName,
                        //TF_TEL_CEL = model.TF_TEL_CEL,
                        //TF_TEL_FIXO = model.TF_TEL_FIXO,
                        DT_DATA_CADASTRO = DateTime.Today,
                        Email = model.UserName,
                        NEWSLETTER = true,
                        EmailConfirmed = true,

                        //DS_EMAIL = model.UserName,
                    };
                    var result = await UserManager.CreateAsync(user, model.Password);

                    var addrole = UserManager.AddToRole(user.Id, "Prestador");

                    if (model.UserName == "dourado.spina@gmail.com")
                    {
                        var removeRole = UserManager.RemoveFromRole(user.Id, "Prestador");
                        addrole = UserManager.AddToRole(user.Id, "Admin");
                    }

                    if (addrole.Succeeded)
                    {
                        if(model.CD_LAT.Length > 10)
                        {
                            lat = model.CD_LAT.Substring(0, 10);
                        }
                        else
                        {
                            lat = model.CD_LAT;
                        }
                        if(model.CD_LONG.Length > 10)
                        {
                            lng = model.CD_LONG.Substring(0, 10);
                        }
                        else
                        {
                            lng = model.CD_LONG;
                        }
                        DbGeography validarEndereco = DbGeography.FromText("POINT(" + lat + " " + lng + ")");
                        ExisteEndereco = db.CAD_PES_ENDERECO
                            .Where(x => x.localizacao.Longitude == validarEndereco.Longitude)
                            .Where(x => x.NM_ESTADO == model.NM_ESTADO)
                            .Where(x => x.NM_CIDADE == model.NM_CIDADE)
                            .Where(x => x.NM_LOGRADOURO == model.NM_LOGRADOURO).ToList();

                        if (ExisteEndereco.Count > 0)
                        {
                            double incremento = 00.000300;
                            for (var i = 0; i < ExisteEndereco.Count; i++)
                            {
                                incremento = incremento + 00.000300;
                            }

                            var lngCadastro = validarEndereco.Latitude;
                            var lngCadastrar = (lngCadastro + incremento).ToString().Replace(",", ".");
                            if(lngCadastrar.Length > 10)
                            {
                                lngCadastrar = lngCadastrar.Substring(0, 10);
                            }
                            validarEndereco = DbGeography.FromText("POINT(" + lat + " " + lngCadastrar + ")");
                        }


                        DbContextTransaction transacao = db.Database.BeginTransaction();
                        try
                        {
                            if (result.Succeeded)
                            {
                                //IdentityResult resultClaim = await UserManager
                                //  .AddClaimAsync(user.Id, new Claim("Nome", model.NM_NOME_PESSOA));
                                //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                                CAD_PES_JURIDICA juridica = new CAD_PES_JURIDICA
                                {
                                    CD_PESSOA = user.Id,
                                    NM_NOME_PRESTADOR = model.NM_NOME_PRESTADOR,
                                    CD_CNPJ = model.CD_CNPJ,
                                    TODO_DIA = model.TODO_DIA,
                                    ATIVO = true,
                                    DIVULGACAO = model.DIVULGACAO,
                                    DS_O_QUE_FAZEMOS = model.DS_O_QUE_FAZEMOS,
                                    ACEITE_CONTRATO = model.ACEITE_CONTRATO
                                };
                                db.CAD_PES_JURIDICA.Add(juridica);
                                db.SaveChanges();

                                CAD_PES_FONE telefone = new CAD_PES_FONE
                                {
                                    CD_PESSOA = user.Id,
                                    CD_FIXO = model.TF_TEL_FIXO,
                                    CD_CELULAR = model.TF_TEL_CEL,
                                    NM_OPERADORA = model.NM_OPERADORA,
                                    WHATSAPP = model.WHATSAPP
                                };
                                db.CAD_PES_FONE.Add(telefone);
                                db.SaveChanges();

                                CAD_PES_ENDERECO endereco = new CAD_PES_ENDERECO
                                {
                                    CD_PESSOA = user.Id,
                                    NM_CIDADE = model.NM_CIDADE,
                                    NM_LOGRADOURO = model.NM_LOGRADOURO,
                                    NM_BAIRRO = "NULL",
                                    NUMERO = model.NUMERO,
                                    NM_ESTADO = model.NM_ESTADO,
                                    CD_CEP = model.CD_CEP,
                                    localizacao = validarEndereco
                                    //TP_TIPO_LOGRADOURO = model.TP_TIPO_LOGRADOURO,
                                };
                                db.CAD_PES_ENDERECO.Add(endereco);
                                db.SaveChanges();

                                CAD_FORMA_PAGAMENTO formaPagamento = new CAD_FORMA_PAGAMENTO
                                {
                                    //CAD_PES_JURIDICA = null,
                                    CD_PESSOA = user.Id,
                                    DINHEIRO = model.DINHEIRO,
                                    CHEQUE = model.CHEQUE,
                                    DEBITO = model.DEBITO,
                                    CREDITO = model.CREDITO,
                                    OUTROS = model.OUTROS
                                };
                                db.CAD_FORMA_PAGAMENTO.Add(formaPagamento);
                                db.SaveChanges();

                                CAD_PORTE_EMPRESA porteEmpresa = new CAD_PORTE_EMPRESA
                                {
                                    //CAD_PES_JURIDICA = null,
                                    CD_PESSOA = user.Id,
                                    PESSOA_FISICA = model.PESSOA_FISICA,
                                    MICRO_EMPRESA = model.MICRO_EMPRESA,
                                    PEQUENAS_EMPRESAS = model.PEQUENAS_EMPRESAS,
                                    EMPRESA_GRANDE_PORTE = model.EMPRESA_GRANDE_PORTE,
                                };
                                db.CAD_PORTE_EMPRESA.Add(porteEmpresa);
                                db.SaveChanges();
                                if (model.TODO_DIA == true)
                                {

                                }
                                else
                                {
                                    CAD_HORA_ATENDIMENTO horaAtendimentoDomingo = new CAD_HORA_ATENDIMENTO
                                    {
                                        CD_PES_JURIDICA = user.Id,
                                        DIA_SEMANA = 0,
                                        HORA_INICIO = model.DOMINGO_HORA_INICIO,
                                        HORA_FIM = model.DOMINGO_HORA_FIM
                                    };
                                    db.CAD_HORA_ATENDIMENTO.Add(horaAtendimentoDomingo);
                                    db.SaveChanges();
                                    //transacao.Commit();

                                    CAD_HORA_ATENDIMENTO horaAtendimentoSegunda = new CAD_HORA_ATENDIMENTO
                                    {
                                        CD_PES_JURIDICA = user.Id,
                                        DIA_SEMANA = 1,
                                        HORA_INICIO = model.SEGUNDA_HORA_INICIO,
                                        HORA_FIM = model.SEGUNDA_HORA_FIM
                                    };
                                    db.CAD_HORA_ATENDIMENTO.Add(horaAtendimentoSegunda);
                                    db.SaveChanges();
                                    //transacao.Commit();

                                    CAD_HORA_ATENDIMENTO horaAtendimentoTerca = new CAD_HORA_ATENDIMENTO
                                    {
                                        CD_PES_JURIDICA = user.Id,
                                        DIA_SEMANA = 2,
                                        HORA_INICIO = model.TERCA_HORA_INICIO,
                                        HORA_FIM = model.TERCA_HORA_FIM
                                    };
                                    db.CAD_HORA_ATENDIMENTO.Add(horaAtendimentoTerca);
                                    db.SaveChanges();
                                    //transacao.Commit();

                                    CAD_HORA_ATENDIMENTO horaAtendimentoQuarta = new CAD_HORA_ATENDIMENTO
                                    {
                                        CD_PES_JURIDICA = user.Id,
                                        DIA_SEMANA = 3,
                                        HORA_INICIO = model.QUARTA_HORA_INICIO,
                                        HORA_FIM = model.QUARTA_HORA_FIM
                                    };
                                    db.CAD_HORA_ATENDIMENTO.Add(horaAtendimentoQuarta);
                                    db.SaveChanges();
                                    //transacao.Commit();

                                    CAD_HORA_ATENDIMENTO horaAtendimentoQuinta = new CAD_HORA_ATENDIMENTO
                                    {
                                        CD_PES_JURIDICA = user.Id,
                                        DIA_SEMANA = 4,
                                        HORA_INICIO = model.QUINTA_HORA_INICIO,
                                        HORA_FIM = model.QUINTA_HORA_FIM
                                    };
                                    db.CAD_HORA_ATENDIMENTO.Add(horaAtendimentoQuinta);
                                    db.SaveChanges();
                                    //transacao.Commit();

                                    CAD_HORA_ATENDIMENTO horaAtendimentoSexta = new CAD_HORA_ATENDIMENTO
                                    {
                                        CD_PES_JURIDICA = user.Id,
                                        DIA_SEMANA = 5,
                                        HORA_INICIO = model.SEXTA_HORA_INICIO,
                                        HORA_FIM = model.SEXTA_HORA_FIM
                                    };
                                    db.CAD_HORA_ATENDIMENTO.Add(horaAtendimentoSexta);
                                    db.SaveChanges();
                                    //transacao.Commit();

                                    CAD_HORA_ATENDIMENTO horaAtendimentoSabado = new CAD_HORA_ATENDIMENTO
                                    {
                                        CD_PES_JURIDICA = user.Id,
                                        DIA_SEMANA = 6,
                                        HORA_INICIO = model.SABADO_HORA_INICIO,
                                        HORA_FIM = model.SABADO_HORA_FIM
                                    };
                                    db.CAD_HORA_ATENDIMENTO.Add(horaAtendimentoSabado);
                                    db.SaveChanges();
                                    //transacao.Commit();
                                }

                                var nome_principal = "";
                                var nome_empresa = "";
                                var nome_servico = "";
                                var ds_descricao = "";
                                StringBuilder texto_final = new StringBuilder();

                                nome_principal = RemoveAcento(model.NM_NOME_PESSOA);
                                nome_empresa = RemoveAcento(model.NM_NOME_PRESTADOR);
                                nome_servico = RemoveAcento(model.NM_NOME_ATIVIDADE);
                                ds_descricao = RemoveAcento(model.DS_DESCRICAO_ATIVIDADE);
                                texto_final = texto_final.Append(model.DS_DESCRICAO_ATIVIDADE).Append(", ").Append(nome_servico).Append(", ").Append(nome_principal).Append(", ").Append(nome_empresa).Append(", ").Append(ds_descricao);
                                CAD_CATEGORIA atividade = new CAD_CATEGORIA
                                {
                                    CD_PES_JURIDICA = user.Id,
                                    NM_NOME = model.NM_NOME_ATIVIDADE,
                                    DS_DESCRICAO = texto_final.ToString(),
                                };
                                db.CAD_CATEGORIA.Add(atividade);
                                db.SaveChanges();
                                transacao.Commit();

                                //Envio de email para confirmação da conta cadastrada.
                                //string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                                //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                                //await UserManager.SendEmailAsync(user.Id, "Confirme sua conta", "Por favor, confirme sua conta clicando <a href=\"" + callbackUrl + "\">aqui</a>");

                                //ViewBag.Message = "Check your email and confirm your account, you must be confirmed "
                                // + "before you can log in.";
                                var body = "<div class='corpo' style='width:100%; height: 100%'><div style='height: 40%; widows:100%;'><h3>Mercado de Serviços</h3><h4>Olá " + model.NM_NOME_PESSOA + ", tudo bem?</h4><h4>ESTAMOS FELIZES POR TER VOCÊ COMO PARCEIRO!</h4><p>NÓS TEMOS O QUE VOCÊ PRECISA. PESQUISE, DIVULGUE E VENDA MAIS!</p></div><div><p>Nossa proposta é a de facilitar a vida do usuário em busca dos mais variados tipos de serviços do dia-a-dia.</p><p>De outro lado, queremos possibilitar que prestadores de serviços encontrem um local propício para divulgar seus trabalhos de forma simples, rápida, abrangente e GRATUITA. Por este motivo, o MERCADO DE SERVIÇOS tem por objetivo atingir as mais diversas áreas de atuação no campo da prestação de serviços, atuando como um elo facilitador entre o usuário e o serviço especializado mais próximo.</p><p>Para atingirmos esta finalidade, contamos com uma crescente rede de cadastros de prestadores de serviço, distribuídos por todo o país, permitindo assim que sua demanda seja atendida da forma mais rápida e satisfatória possível. Por isso, contamos com que você, usuário, desfrute desta nova ferramenta e deixe sua avaliação após ter utilizado o serviço. Dessa forma teremos a condição de oferecer sempre o melhor do que você precisa, ampliando a rede de ofertas e primando sempre pela melhor qualidade.</p><p>O Fornecedor pode aproveitar isso para divulgar seus produtos/equipamentos/materiais de forma direcionada. O Fornecedor tem a opção de divulgar para prestadores de serviços que estão relacionados ao que ele vende. O Fornecedor também pode limitar a divulgação por cidades e estados específicos ou divulgar a nível Brasil!</p></div><footer><p>Mercado de Serviços <a href='https://www.mercadodeservicos.com.br'>www.mercadodeservicos.com.br</a></p></footer></div>";
                                var message = new MailMessage();
                                message.To.Add(new MailAddress(model.UserName));  // replace with valid value 
                                message.From = new MailAddress("ms@mercadodeservicos.com.br", "Mercado de Serviços");  // replace with valid value
                                message.Subject = "Estamos felizes por ter você como parceiro!";
                                message.Body = body;
                                message.IsBodyHtml = true;

                                using (var smtp = new SmtpClient())
                                {
                                    var credential = new NetworkCredential
                                    {
                                        UserName = "ms@mercadodeservicos.com.br",  // replace with valid value
                                        Password = "Mercado@745"  // replace with valid value
                                    };
                                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                    smtp.Credentials = credential;
                                    smtp.Host = "smtp.mercadodeservicos.com.br";
                                    smtp.Port = 587;
                                    smtp.EnableSsl = false;
                                    await smtp.SendMailAsync(message);
                                    var loginResult = Login(new LoginViewModel()
                                    {
                                        Email = model.UserName,
                                        Password = model.Password,
                                        RememberMe = true,
                                    }, "https://www.mercadodeservicos.com.br/Manage");
                                    return await loginResult;
                                }
                                //AddErrors(result);
                                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                                // Send an email with this link

                            }
                            else
                            {
                                var id = user.Id;
                                var removeRole = UserManager.RemoveFromRole(id, "Prestador");
                                CAD_FORMA_PAGAMENTO formaPagamento = db.CAD_FORMA_PAGAMENTO.Find(id);
                                formaPagamento = db.CAD_FORMA_PAGAMENTO.Remove(formaPagamento);
                                CAD_PORTE_EMPRESA porteEmpresa = db.CAD_PORTE_EMPRESA.Find(id);
                                porteEmpresa = db.CAD_PORTE_EMPRESA.Remove(porteEmpresa);


                                var telefones = new List<CAD_PES_FONE>();
                                telefones = db.CAD_PES_FONE.Where(x => x.CD_PESSOA == id).ToList();
                                for (int i = telefones.Count - 1; i >= 0; i--)
                                {
                                    telefones.Remove(telefones[i]);
                                }

                                var enderecos = new List<CAD_PES_ENDERECO>();
                                enderecos = db.CAD_PES_ENDERECO.Where(x => x.CD_PESSOA == id).ToList();
                                for (int i = enderecos.Count - 1; i >= 0; i--)
                                {
                                    enderecos.Remove(enderecos[i]);
                                }

                                var horarios = new List<CAD_HORA_ATENDIMENTO>();
                                horarios = db.CAD_HORA_ATENDIMENTO.Where(x => x.CD_PES_JURIDICA == id).ToList();
                                for (int i = horarios.Count - 1; i >= 0; i--)
                                {
                                    horarios.Remove(horarios[i]);
                                }

                                var atividades = new List<CAD_CATEGORIA>();
                                atividades = db.CAD_CATEGORIA.Where(x => x.CD_PES_JURIDICA == id).ToList();
                                for (int i = atividades.Count - 1; i >= 0; i--)
                                {
                                    atividades.Remove(atividades[i]);
                                }
                                UserManager.Delete(user);
                                transacao.Rollback();
                                AddErrors(result);
                            }
                        }
                        catch (Exception ex)
                        {
                            var id = user.Id;
                            var removeRole = UserManager.RemoveFromRole(id, "Prestador");
                            CAD_FORMA_PAGAMENTO formaPagamento = db.CAD_FORMA_PAGAMENTO.Find(id);
                            formaPagamento = db.CAD_FORMA_PAGAMENTO.Remove(formaPagamento);
                            CAD_PORTE_EMPRESA porteEmpresa = db.CAD_PORTE_EMPRESA.Find(id);
                            porteEmpresa = db.CAD_PORTE_EMPRESA.Remove(porteEmpresa);


                            var telefones = new List<CAD_PES_FONE>();
                            telefones = db.CAD_PES_FONE.Where(x => x.CD_PESSOA == id).ToList();
                            for (int i = telefones.Count - 1; i >= 0; i--)
                            {
                                telefones.Remove(telefones[i]);
                            }

                            var enderecos = new List<CAD_PES_ENDERECO>();
                            enderecos = db.CAD_PES_ENDERECO.Where(x => x.CD_PESSOA == id).ToList();
                            for (int i = enderecos.Count - 1; i >= 0; i--)
                            {
                                enderecos.Remove(enderecos[i]);
                            }

                            var horarios = new List<CAD_HORA_ATENDIMENTO>();
                            horarios = db.CAD_HORA_ATENDIMENTO.Where(x => x.CD_PES_JURIDICA == id).ToList();
                            for (int i = horarios.Count - 1; i >= 0; i--)
                            {
                                horarios.Remove(horarios[i]);
                            }

                            var atividades = new List<CAD_CATEGORIA>();
                            atividades = db.CAD_CATEGORIA.Where(x => x.CD_PES_JURIDICA == id).ToList();
                            for (int i = atividades.Count - 1; i >= 0; i--)
                            {
                                atividades.Remove(atividades[i]);
                            }
                            UserManager.Delete(user);
                            transacao.Rollback();
                            throw ex;
                        }
                    }
                }
                else
                {
                    var userIdExistente = usuarioExistente.FirstOrDefault().Id;
                    var fornecedorExistente = db.CAD_PES_FORNECEDOR.Where(x => x.CD_PESSOA == userIdExistente).ToList();
                    if (fornecedorExistente.Count() > 0)
                    {
                        ViewBag.errorMessage = "Este email está cadastrado para um fornecedor. Se não lembrar a senha, solicite o reset de senha. Não é possível cadastrar um mesmo email para dois usuários. Caso tenha feito o cadastro de fornecedor por engano, entre em contato com a gente ou faça o cadastro com outra conta de email.";
                        return View("Error");
                    }
                    else
                    {
                        ViewBag.errorMessage = "Este email está cadastrado para um prestador de serviços. Se não lembrar a senha, solicite o reset de senha. Não é possível cadastrar um mesmo email para dois usuários diferentes! Caso tenha feito o cadastro de prestador de serviços por engano, entre em contato com a gente ou faça o cadastro com outra conta de email.";
                        return View("Error");
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult CadastrarFornecedor()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CadastrarFornecedor(RegisterFornecedorViewModel model)
        {
            var usuarioExistente = db.CAD_PESSOA.Where(x => x.Email == model.UserName).ToList();
            if (usuarioExistente.Count() == 0)
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser
                    {
                        NM_NOME_PESSOA = model.NM_NOME_PESSOA,
                        UserName = model.UserName,
                        //TF_TEL_CEL = model.TF_TEL_CEL,
                        //TF_TEL_FIXO = model.TF_TEL_FIXO,
                        DT_DATA_CADASTRO = System.DateTime.Today,
                        Email = model.UserName,
                        NEWSLETTER = true
                        //DS_EMAIL = model.UserName,
                    };
                    var result = await UserManager.CreateAsync(user, model.Password);

                    var addrole = UserManager.AddToRole(user.Id, "Fornecedor");

                    DbContextTransaction transacao = db.Database.BeginTransaction();
                    try
                    {
                        if (result.Succeeded)
                        {
                            if (addrole.Succeeded)
                            {

                                CAD_PES_FORNECEDOR fornecedor = new CAD_PES_FORNECEDOR
                                {
                                    CD_PESSOA = user.Id,
                                    CD_CNPJ = model.CD_CNPJ,
                                    CD_STATUS_PAGT = 1,
                                    ATIVO = true,
                                    ACEITE_CONTRATO = model.ACEITE_CONTRATO
                                };
                                db.CAD_PES_FORNECEDOR.Add(fornecedor);
                                db.SaveChanges();

                                CAD_PES_FONE telefone = new CAD_PES_FONE
                                {
                                    CD_PESSOA = user.Id,
                                    CD_FIXO = model.TF_TEL_FIXO,
                                    CD_CELULAR = model.TF_TEL_CEL,
                                    NM_OPERADORA = model.NM_OPERADORA,
                                    WHATSAPP = model.WHATSAPP
                                };
                                db.CAD_PES_FONE.Add(telefone);
                                db.SaveChanges();

                                CAD_PES_ENDERECO endereco = new CAD_PES_ENDERECO
                                {
                                    CD_PESSOA = user.Id,
                                    NM_CIDADE = model.NM_CIDADE,
                                    NM_LOGRADOURO = model.NM_LOGRADOURO,
                                    NM_BAIRRO = "NULL",
                                    NUMERO = model.NUMERO,
                                    NM_ESTADO = model.NM_ESTADO,
                                    CD_CEP = model.CD_CEP,
                                    localizacao = DbGeography.FromText("POINT(" + model.CD_LAT + " " + model.CD_LONG + ")")
                                    //TP_TIPO_LOGRADOURO = model.TP_TIPO_LOGRADOURO,
                                };
                                db.CAD_PES_ENDERECO.Add(endereco);
                                db.SaveChanges();
                                transacao.Commit();

                                var body = "<div class='corpo' style='width:100%; height: 100%'><div style='height: 40%; widows:100%;'><h3>Mercado de Serviços</h3><h4>Olá " + model.NM_NOME_PESSOA + ", tudo bem?</h4><h4>ESTAMOS FELIZES POR TER VOCÊ COMO PARCEIRO!</h4><p>NÓS TEMOS O QUE VOCÊ PRECISA. PESQUISE, DIVULGUE E VENDA MAIS!</p></div><div><p>Nossa proposta é a de facilitar a vida do usuário em busca dos mais variados tipos de serviços do dia-a-dia.</p><p>De outro lado, queremos possibilitar que prestadores de serviços encontrem um local propício para divulgar seus trabalhos de forma simples, rápida, abrangente e GRATUITA. Por este motivo, o MERCADO DE SERVIÇOS tem por objetivo atingir as mais diversas áreas de atuação no campo da prestação de serviços, atuando como um elo facilitador entre o usuário e o serviço especializado mais próximo.</p><p>Para atingirmos esta finalidade, contamos com uma crescente rede de cadastros de prestadores de serviço, distribuídos por todo o país, permitindo assim que sua demanda seja atendida da forma mais rápida e satisfatória possível. Por isso, contamos com que você, usuário, desfrute desta nova ferramenta e deixe sua avaliação após ter utilizado o serviço. Dessa forma teremos a condição de oferecer sempre o melhor do que você precisa, ampliando a rede de ofertas e primando sempre pela melhor qualidade.</p><p>O Fornecedor pode aproveitar isso para divulgar seus produtos/equipamentos/materiais de forma direcionada. O Fornecedor tem a opção de divulgar para prestadores de serviços que estão relacionados ao que ele vende. O Fornecedor também pode limitar a divulgação por cidades e estados específicos ou divulgar a nível Brasil!</p></div><footer><p>Mercado de Serviços <a href='https://www.mercadodeservicos.com.br'>www.mercadodeservicos.com.br</a></p></footer></div>";
                                var message = new MailMessage();
                                message.To.Add(new MailAddress(model.UserName));  // replace with valid value 
                                message.From = new MailAddress("ms@mercadodeservicos.com.br", "Mercado de Serviços");  // replace with valid value
                                message.Subject = "Estamos felizes por ter você como parceiro!";
                                message.Body = body;
                                message.IsBodyHtml = true;

                                using (var smtp = new SmtpClient())
                                {
                                    var credential = new NetworkCredential
                                    {
                                        UserName = "ms@mercadodeservicos.com.br",  // replace with valid value
                                        Password = "Mercado@745"  // replace with valid value
                                    };
                                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                    smtp.Credentials = credential;
                                    smtp.Host = "smtp.mercadodeservicos.com.br";
                                    smtp.Port = 587;
                                    smtp.EnableSsl = false;
                                    await smtp.SendMailAsync(message);
                                    var loginResult = Login(new LoginViewModel()
                                    {
                                        Email = model.UserName,
                                        Password = model.Password,
                                        RememberMe = true,
                                    }, "https://www.mercadodeservicos.com.br/Manage");
                                    return await loginResult;
                                }
                                //Envio de email para confirmação da conta cadastrada.
                                //string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                                //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                                //await UserManager.SendEmailAsync(user.Id, "Confirme sua conta", "Por favor, confirme sua conta clicando <a href=\"" + callbackUrl + "\">aqui</a>");

                                //ViewBag.Message = "Check your email and confirm your account, you must be confirmed "
                                // + "before you can log in.";
                                //AddErrors(result);
                                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                                // Send an email with this link
                            }
                        }
                        else
                        {
                            var removeRole = UserManager.RemoveFromRole(user.Id, "Fornecedor");
                            var id = user.Id;
                            var telefones = new List<CAD_PES_FONE>();
                            telefones = db.CAD_PES_FONE.Where(x => x.CD_PESSOA == id).ToList();
                            for (int i = telefones.Count - 1; i >= 0; i--)
                            {
                                telefones.Remove(telefones[i]);
                            }

                            var enderecos = new List<CAD_PES_ENDERECO>();
                            enderecos = db.CAD_PES_ENDERECO.Where(x => x.CD_PESSOA == id).ToList();
                            for (int i = enderecos.Count - 1; i >= 0; i--)
                            {
                                enderecos.Remove(enderecos[i]);
                            }
                            UserManager.Delete(user);
                            transacao.Rollback();
                            AddErrors(result);
                        }
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: true);
                    }
                    catch (Exception ex)
                    {
                        var removeRole = UserManager.RemoveFromRole(user.Id, "Fornecedor");
                        var id = user.Id;
                        var telefones = new List<CAD_PES_FONE>();
                        telefones = db.CAD_PES_FONE.Where(x => x.CD_PESSOA == id).ToList();
                        for (int i = telefones.Count - 1; i >= 0; i--)
                        {
                            telefones.Remove(telefones[i]);
                        }

                        var enderecos = new List<CAD_PES_ENDERECO>();
                        enderecos = db.CAD_PES_ENDERECO.Where(x => x.CD_PESSOA == id).ToList();
                        for (int i = enderecos.Count - 1; i >= 0; i--)
                        {
                            enderecos.Remove(enderecos[i]);
                        }
                        UserManager.Delete(user);
                        transacao.Rollback();
                        throw ex;
                    }
                }
            }
            else
            {
                var userIdExistente = usuarioExistente.FirstOrDefault().Id;
                var fornecedorExistente = db.CAD_PES_FORNECEDOR.Where(x => x.CD_PESSOA == userIdExistente).ToList();
                if (fornecedorExistente.Count() > 0)
                {
                    ViewBag.errorMessage = "Este email está cadastrado para um fornecedor. Se não lembrar a senha, solicite o reset de senha. Não é possível cadastrar um mesmo email para dois usuários. Caso tenha feito o cadastro do fornecedor por engano, acesse a conta do fornecedor, vá até a área de gerenciamento de perfil e delete a conta ou altere o email.";
                    return View("Error");
                }
                else
                {
                    ViewBag.errorMessage = "Este email está cadastrado para um prestador de serviços. Se não lembrar a senha, solicite o reset de senha. Não é possível cadastrar um mesmo email para dois usuários diferentes! Caso tenha feito o cadastro do prestador por engano, acesse a conta do prestador, vá até a área de gerenciamento de perfil e delete a conta ou altere o email.";
                    return View("Error");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }




        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                //await UserManager.SendEmailAsync(user.Id, "Reset de Senha", "Por favor, altere sua senha clicando <a href=\'" + callbackUrl + "\'>aqui</a>");
                var body = "Por favor, altere sua senha clicando <a href=\'" + callbackUrl + "\'>aqui</a>";
                var message = new MailMessage();
                message.To.Add(new MailAddress(model.Email));  // replace with valid value 
                message.From = new MailAddress("noreply@mercadodeservicos.com.br", "Mercado de Serviços - Reset de Senha");  // replace with valid value
                message.Subject = "Reset de Senha";
                message.Body = body;
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "noreply@mercadodeservicos.com.br",  // replace with valid value
                        Password = "noreply@745"  // replace with valid value
                    };
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.mercadodeservicos.com.br";
                    smtp.Port = 587;
                    smtp.EnableSsl = false;
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("ForgotPasswordConfirmation", "Account");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            string url = Request.UrlReferrer.PathAndQuery;
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = url }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return View("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }

                var usuario = db.CAD_PESSOA.Where(x => x.Email == info.Email).ToList();

                if (usuario.Count != 0)
                {
                    ViewBag.errorMessage = "Este email já está cadastrado. Caso não lembre a senha, solicite o <a href='~/Account/ForgotPassword'>reset de senha</a>!";
                    return View("Error");
                }

                var user = new ApplicationUser
                {
                    NM_NOME_PESSOA = info.ExternalIdentity.Name,
                    UserName = model.Email,
                    DT_DATA_CADASTRO = System.DateTime.Today,
                    Email = model.Email
                };
                var result = await UserManager.CreateAsync(user);
                var addrole = UserManager.AddToRole(user.Id, "Usuario");

                if (addrole.Succeeded)
                {

                    if (result.Succeeded)
                    {
                        DbContextTransaction transacao = db.Database.BeginTransaction();
                        try
                        {
                            var cAD_PES_USUARIO = new CAD_PES_USUARIO
                            {
                                CD_PESSOA = user.Id
                            };
                            db.CAD_PES_USUARIO.Add(cAD_PES_USUARIO);
                            db.SaveChanges();
                            transacao.Commit();
                        }
                        catch (Exception ex)
                        {
                            transacao.Rollback();
                            throw ex;
                        }
                    }
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            string url = Request.UrlReferrer.PathAndQuery;
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToLocal(url);
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
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

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        //public ActionResult Valida_NM_Site(string DS_APELIDO_SITE)
        //{
        //    var nome_site = db.CAD_PESSOA.SqlQuery("SELECT * FROM AspNetUsers WHERE DS_APELIDO_SITE = '" + DS_APELIDO_SITE + "'");

        //    return Json(nome_site.All(x => x.DS_APELIDO_SITE.Equals(DS_APELIDO_SITE)), JsonRequestBehavior.AllowGet);
        //}
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


        #endregion
    }
}