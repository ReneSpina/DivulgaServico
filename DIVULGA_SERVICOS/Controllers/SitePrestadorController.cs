using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DIVULGA_SERVICOS.Models;

namespace DIVULGA_SERVICOS.Controllers
{
    public class SitePrestadorController : Controller
    {
        private PRINCIPAL db;

        public SitePrestadorController()
        {
            db = new PRINCIPAL();
        }

        // GET: SitePrestador
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SiteHomePrestador(string NomePrestador)
        {

            //if (id < 1)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}


            var prestador = db.CAD_PESSOA.Where(x => x.DS_APELIDO_SITE == NomePrestador).FirstOrDefault();

            if (prestador == null)
            {
                //return RedirectToRoute("Default", NomePrestador);
                //return RedirectToAction("Index", new { Controller = NomePrestador});
                return Redirect(NomePrestador+"/Index");//RedirectToAction("Index", ""+NomePrestador);
            }
            return View("SitePrestador", prestador);
        }
    }
}