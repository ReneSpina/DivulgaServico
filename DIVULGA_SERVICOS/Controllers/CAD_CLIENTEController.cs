using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DIVULGA_SERVICOS.Models;

namespace DIVULGA_SERVICOS.Controllers
{
    public class CAD_CLIENTEController : Controller
    {
        private PRINCIPAL db = new PRINCIPAL();

        // GET: CAD_CLIENTE
        public ActionResult Cliente()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var usuario = db.CAD_PESSOA.Where(x => x.UserName == userName).FirstOrDefault();
                var cliente = db.CAD_CLIENTE.Where(x => x.CD_PESSOA == usuario.Id);
                
                if (cliente != null)
                {
                    //var cliente = db.CAD_CLIENTE.Include(x => x.CD_PESSOA == pes_juridica.CD_PESSOA);
                    return View(cliente.ToList());
                }
                return View();
            }
            else
            {
                ViewBag.errorMessage = "Você precisa ser um prestador de serviço e deve estar logado para acessar essa página.";
                return View("Error");
            }
            //var cAD_CLIENTE = db.CAD_CLIENTE.Include(c => c.CAD_PES_JURIDICA);
        }

        // GET: CAD_CLIENTE/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAD_CLIENTE cAD_CLIENTE = db.CAD_CLIENTE.Find(id);
            if (cAD_CLIENTE == null)
            {
                return HttpNotFound();
            }
            return View(cAD_CLIENTE);
        }

        // GET: CAD_CLIENTE/Create
        public ActionResult Create()
        {
            ViewBag.CD_PESSOA = new SelectList(db.CAD_PES_JURIDICA, "CD_PESSOA", "CD_CNPJ");
            return View();
        }

        // POST: CAD_CLIENTE/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CD_PESSOA,SQ_CLIENTE,NM_NOME")] CAD_CLIENTE cAD_CLIENTE)
        {
            if (ModelState.IsValid)
            {
                db.CAD_CLIENTE.Add(cAD_CLIENTE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CD_PESSOA = new SelectList(db.CAD_PES_JURIDICA, "CD_PESSOA", "CD_CNPJ", cAD_CLIENTE.CD_PESSOA);
            return View(cAD_CLIENTE);
        }

        // GET: CAD_CLIENTE/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAD_CLIENTE cAD_CLIENTE = db.CAD_CLIENTE.Find(id);
            if (cAD_CLIENTE == null)
            {
                return HttpNotFound();
            }
            ViewBag.CD_PESSOA = new SelectList(db.CAD_PES_JURIDICA, "CD_PESSOA", "CD_CNPJ", cAD_CLIENTE.CD_PESSOA);
            return View(cAD_CLIENTE);
        }

        // POST: CAD_CLIENTE/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CD_PESSOA,SQ_CLIENTE,NM_NOME")] CAD_CLIENTE cAD_CLIENTE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cAD_CLIENTE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CD_PESSOA = new SelectList(db.CAD_PES_JURIDICA, "CD_PESSOA", "CD_CNPJ", cAD_CLIENTE.CD_PESSOA);
            return View(cAD_CLIENTE);
        }

        // GET: CAD_CLIENTE/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAD_CLIENTE cAD_CLIENTE = db.CAD_CLIENTE.Find(id);
            if (cAD_CLIENTE == null)
            {
                return HttpNotFound();
            }
            return View(cAD_CLIENTE);
        }

        // POST: CAD_CLIENTE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CAD_CLIENTE cAD_CLIENTE = db.CAD_CLIENTE.Find(id);
            db.CAD_CLIENTE.Remove(cAD_CLIENTE);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
