using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DIVULGA_SERVICOS.Models;
using System.Web.Routing;

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
            var prestador = db.CAD_PESSOA.Where(x => x.DS_APELIDO_SITE == NomePrestador).FirstOrDefault();

            if (prestador == null)
            {
                //return RedirectToRoute("Default", NomePrestador);
                //return RedirectToAction("Index", new { Controller = NomePrestador});
                return Redirect(NomePrestador+"/Index");//RedirectToAction("Index", ""+NomePrestador);
            }
            else
            {
                return View("SitePrestador", prestador);
            }
        }

        public ActionResult ItemMenu(string NomePrestador, string ItemMenu)
        {
            var prestador = db.CAD_PESSOA.Where(x => x.DS_APELIDO_SITE == NomePrestador).FirstOrDefault();
            //var action = ItemMenu;
            //var controller = NomePrestador;
            if (prestador != null)
            {
                if(ItemMenu == "Servico")
                {
                    var servicos = db.CAD_CATEGORIA.Where(x => x.CD_PES_JURIDICA == prestador.Id);
                    return View("SiteServicoPrestador", servicos.ToList());
                }
                else if(ItemMenu == "Cliente")
                    {
                        var clientes = db.CAD_CLIENTE.Where(x => x.CD_PESSOA == prestador.Id);
                        return View("SiteClientePrestador", clientes.ToList());
                }
                else
                {
                    return View();
                }
            }
            else
            {
                //return RedirectToRoute("Default", (RouteTable.Routes[name: "Default"] as Route).Constraints);
                return RedirectToRoute("Default");
            }
        }
    }
}