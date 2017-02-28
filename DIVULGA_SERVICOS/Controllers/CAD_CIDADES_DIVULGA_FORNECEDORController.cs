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
    public class CAD_CIDADES_DIVULGA_FORNECEDORController : Controller
    {
        private PRINCIPAL db = new PRINCIPAL();

        // GET: CAD_CIDADES_DIVULGA_FORNECEDOR
        public ActionResult Index()
        {
            var cAD_CIDADES_DIVULGA_FORNECEDOR = db.CAD_CIDADES_DIVULGA_FORNECEDOR.Include(c => c.CAD_PES_FORNECEDOR);
            return View(cAD_CIDADES_DIVULGA_FORNECEDOR.ToList());
        }

        // GET: CAD_CIDADES_DIVULGA_FORNECEDOR/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAD_CIDADES_DIVULGA_FORNECEDOR cAD_CIDADES_DIVULGA_FORNECEDOR = db.CAD_CIDADES_DIVULGA_FORNECEDOR.Find(id);
            if (cAD_CIDADES_DIVULGA_FORNECEDOR == null)
            {
                return HttpNotFound();
            }
            return View(cAD_CIDADES_DIVULGA_FORNECEDOR);
        }

        // GET: CAD_CIDADES_DIVULGA_FORNECEDOR/Create
        public ActionResult Create()
        {
            ViewBag.CD_PESSOA = new SelectList(db.CAD_PES_FORNECEDOR, "CD_PESSOA", "CD_CNPJ");
            return View();
        }

        // POST: CAD_CIDADES_DIVULGA_FORNECEDOR/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CD_PESSOA,SQ_CIDADE,NM_CIDADE,NM_ESTADO,BRASIL")] CAD_CIDADES_DIVULGA_FORNECEDOR cAD_CIDADES_DIVULGA_FORNECEDOR)
        {
            if (ModelState.IsValid)
            {
                db.CAD_CIDADES_DIVULGA_FORNECEDOR.Add(cAD_CIDADES_DIVULGA_FORNECEDOR);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CD_PESSOA = new SelectList(db.CAD_PES_FORNECEDOR, "CD_PESSOA", "CD_CNPJ", cAD_CIDADES_DIVULGA_FORNECEDOR.CD_PESSOA);
            return View(cAD_CIDADES_DIVULGA_FORNECEDOR);
        }

        // GET: CAD_CIDADES_DIVULGA_FORNECEDOR/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAD_CIDADES_DIVULGA_FORNECEDOR cAD_CIDADES_DIVULGA_FORNECEDOR = db.CAD_CIDADES_DIVULGA_FORNECEDOR.Find(id);
            if (cAD_CIDADES_DIVULGA_FORNECEDOR == null)
            {
                return HttpNotFound();
            }
            ViewBag.CD_PESSOA = new SelectList(db.CAD_PES_FORNECEDOR, "CD_PESSOA", "CD_CNPJ", cAD_CIDADES_DIVULGA_FORNECEDOR.CD_PESSOA);
            return View(cAD_CIDADES_DIVULGA_FORNECEDOR);
        }

        // POST: CAD_CIDADES_DIVULGA_FORNECEDOR/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CD_PESSOA,SQ_CIDADE,NM_CIDADE,NM_ESTADO,BRASIL")] CAD_CIDADES_DIVULGA_FORNECEDOR cAD_CIDADES_DIVULGA_FORNECEDOR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cAD_CIDADES_DIVULGA_FORNECEDOR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CD_PESSOA = new SelectList(db.CAD_PES_FORNECEDOR, "CD_PESSOA", "CD_CNPJ", cAD_CIDADES_DIVULGA_FORNECEDOR.CD_PESSOA);
            return View(cAD_CIDADES_DIVULGA_FORNECEDOR);
        }

        // GET: CAD_CIDADES_DIVULGA_FORNECEDOR/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAD_CIDADES_DIVULGA_FORNECEDOR cAD_CIDADES_DIVULGA_FORNECEDOR = db.CAD_CIDADES_DIVULGA_FORNECEDOR.Find(id);
            if (cAD_CIDADES_DIVULGA_FORNECEDOR == null)
            {
                return HttpNotFound();
            }
            return View(cAD_CIDADES_DIVULGA_FORNECEDOR);
        }

        // POST: CAD_CIDADES_DIVULGA_FORNECEDOR/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CAD_CIDADES_DIVULGA_FORNECEDOR cAD_CIDADES_DIVULGA_FORNECEDOR = db.CAD_CIDADES_DIVULGA_FORNECEDOR.Find(id);
            db.CAD_CIDADES_DIVULGA_FORNECEDOR.Remove(cAD_CIDADES_DIVULGA_FORNECEDOR);
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
