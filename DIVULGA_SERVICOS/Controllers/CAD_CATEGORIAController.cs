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
            return View(db.CAD_CATEGORIA.ToList());
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
            return View();
        }

        // POST: CAD_CATEGORIA/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CD_CATEGORIA,CD_SERVICO,CD_PES_JURIDICA,NM_NOME")] CAD_CATEGORIA cAD_CATEGORIA)
        {
            if (ModelState.IsValid)
            {
                db.CAD_CATEGORIA.Add(cAD_CATEGORIA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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
            return View(cAD_CATEGORIA);
        }

        // POST: CAD_CATEGORIA/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CD_CATEGORIA,CD_SERVICO,CD_PES_JURIDICA,NM_NOME")] CAD_CATEGORIA cAD_CATEGORIA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cAD_CATEGORIA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
