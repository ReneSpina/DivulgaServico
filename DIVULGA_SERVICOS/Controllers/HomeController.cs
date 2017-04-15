using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        public ActionResult TestaEmail()
        {
            return View();
        }
    }
}