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
    public class FornecedorController : Controller
    {
        private PRINCIPAL db = new PRINCIPAL();

        // GET: Fornecedor
        public ActionResult Index()
        {
            var cAD_PES_FORNECEDOR = db.CAD_PES_FORNECEDOR.Include(c => c.CAD_PESSOA);
            return View(cAD_PES_FORNECEDOR.ToList());
        }

        // GET: Fornecedor/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAD_PES_FORNECEDOR cAD_PES_FORNECEDOR = db.CAD_PES_FORNECEDOR.Find(id);
            if (cAD_PES_FORNECEDOR == null)
            {
                return HttpNotFound();
            }
            return View(cAD_PES_FORNECEDOR);
        }

        // GET: Fornecedor/Create
        public ActionResult Create()
        {
            ViewBag.CD_PESSOA = new SelectList(db.CAD_PESSOA, "Id", "NM_NOME_PESSOA");
            return View();
        }

        // POST: Fornecedor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CD_PESSOA,CD_CNPJ,ATIVO,CD_STATUS_PAGT,ACEITE_CONTRATO")] CAD_PES_FORNECEDOR cAD_PES_FORNECEDOR)
        {
            if (ModelState.IsValid)
            {
                db.CAD_PES_FORNECEDOR.Add(cAD_PES_FORNECEDOR);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CD_PESSOA = new SelectList(db.CAD_PESSOA, "Id", "NM_NOME_PESSOA", cAD_PES_FORNECEDOR.CD_PESSOA);
            return View(cAD_PES_FORNECEDOR);
        }

        // GET: Fornecedor/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAD_PES_FORNECEDOR cAD_PES_FORNECEDOR = db.CAD_PES_FORNECEDOR.Find(id);
            if (cAD_PES_FORNECEDOR == null)
            {
                return HttpNotFound();
            }
            ViewBag.CD_PESSOA = new SelectList(db.CAD_PESSOA, "Id", "NM_NOME_PESSOA", cAD_PES_FORNECEDOR.CD_PESSOA);
            return View(cAD_PES_FORNECEDOR);
        }

        // POST: Fornecedor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CD_PESSOA,CD_CNPJ,ATIVO,CD_STATUS_PAGT,ACEITE_CONTRATO")] CAD_PES_FORNECEDOR cAD_PES_FORNECEDOR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cAD_PES_FORNECEDOR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CD_PESSOA = new SelectList(db.CAD_PESSOA, "Id", "NM_NOME_PESSOA", cAD_PES_FORNECEDOR.CD_PESSOA);
            return View(cAD_PES_FORNECEDOR);
        }

        // GET: Fornecedor/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAD_PES_FORNECEDOR cAD_PES_FORNECEDOR = db.CAD_PES_FORNECEDOR.Find(id);
            if (cAD_PES_FORNECEDOR == null)
            {
                return HttpNotFound();
            }
            return View(cAD_PES_FORNECEDOR);
        }

        // POST: Fornecedor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CAD_PES_FORNECEDOR cAD_PES_FORNECEDOR = db.CAD_PES_FORNECEDOR.Find(id);
            db.CAD_PES_FORNECEDOR.Remove(cAD_PES_FORNECEDOR);
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
