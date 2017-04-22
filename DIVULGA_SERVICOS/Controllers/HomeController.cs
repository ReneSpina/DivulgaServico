using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DIVULGA_SERVICOS.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sobre()
        {
            ViewBag.Message = "Sobre Nós";

            return View();
        }

        public ActionResult Contatos()
        {
            ViewBag.Message = "Nossos Contatos";

            return View();
        }

        public ActionResult Privacidade()
        {
            ViewBag.Message = "Políticas de Privacidade";

            return View();
        }

        public ActionResult termosDeUso()
        {
            ViewBag.Message = "Termos de Uso";

            return View();
        }

        public ActionResult ComoFunciona()
        {
            return View("PrestadorServico");
        }
        public ActionResult ComoFuncionaF()
        {
            return View("Fornecedor");
        }

        [HttpPost]
        public async Task<ActionResult> EmailContato(string name = "", string email = "", string messages = "")
        {
            var body = "Quem está entrando em contato: " + name +
                "<br />"+
                "Email de quem está entrando em contato: " + email +
                "<br />" +
                "Mensagem de quem está entrando em contato: " + messages;
            var message = new MailMessage();
            message.To.Add(new MailAddress("mercadodeservico@gmail.com"));  // replace with valid value 
            message.From = new MailAddress("ms@mercadodeservicos.com.br", "Mercado de Serviços");  // replace with valid value
            message.Subject = "Contato";
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
                return RedirectToAction("Contatos");
            }
        }
    }
}