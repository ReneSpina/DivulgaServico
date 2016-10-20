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
    public class CAD_CATEGORIAController : Controller
    {
        private PRINCIPAL db = new PRINCIPAL();

        // GET: CAD_CATEGORIA
        public ActionResult Index()
        {
            var cAD_CATEGORIA = db.CAD_CATEGORIA.Include(c => c.CAD_PES_JURIDICA);
            return View(cAD_CATEGORIA.ToList());
        }

        // GET: CAD_CATEGORIA/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAD_CATEGORIA cAD_CATEGORIA = db.CAD_CATEGORIA.Find(id);
            if (cAD_CATEGORIA == null)
            {
                return HttpNotFound();
            }
            return View(cAD_CATEGORIA);
        }

        // GET: CAD_CATEGORIA/Create
        public ActionResult Create()
        {
            ViewBag.CD_PES_JURIDICA = new SelectList(db.CAD_PES_JURIDICA, "CD_PESSOA", "CD_CNPJ");
            return View();
        }

        // POST: CAD_CATEGORIA/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SQ_CATEGORIA,CD_PES_JURIDICA,NM_NOME,SHOW,DS_DESCRICAO")] CAD_CATEGORIA cAD_CATEGORIA)
        {
            if (ModelState.IsValid)
            {
                db.CAD_CATEGORIA.Add(cAD_CATEGORIA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CD_PES_JURIDICA = new SelectList(db.CAD_PES_JURIDICA, "CD_PESSOA", "CD_CNPJ", cAD_CATEGORIA.CD_PES_JURIDICA);
            return View(cAD_CATEGORIA);
        }

        // GET: CAD_CATEGORIA/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAD_CATEGORIA cAD_CATEGORIA = db.CAD_CATEGORIA.Find(id);
            if (cAD_CATEGORIA == null)
            {
                return HttpNotFound();
            }
            ViewBag.CD_PES_JURIDICA = new SelectList(db.CAD_PES_JURIDICA, "CD_PESSOA", "CD_CNPJ", cAD_CATEGORIA.CD_PES_JURIDICA);
            return View(cAD_CATEGORIA);
        }

        // POST: CAD_CATEGORIA/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SQ_CATEGORIA,CD_PES_JURIDICA,NM_NOME,SHOW,DS_DESCRICAO")] CAD_CATEGORIA cAD_CATEGORIA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cAD_CATEGORIA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CD_PES_JURIDICA = new SelectList(db.CAD_PES_JURIDICA, "CD_PESSOA", "CD_CNPJ", cAD_CATEGORIA.CD_PES_JURIDICA);
            return View(cAD_CATEGORIA);
        }

        // GET: CAD_CATEGORIA/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAD_CATEGORIA cAD_CATEGORIA = db.CAD_CATEGORIA.Find(id);
            if (cAD_CATEGORIA == null)
            {
                return HttpNotFound();
            }
            return View(cAD_CATEGORIA);
        }

        // POST: CAD_CATEGORIA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            CAD_CATEGORIA cAD_CATEGORIA = db.CAD_CATEGORIA.Find(id);
            db.CAD_CATEGORIA.Remove(cAD_CATEGORIA);
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
