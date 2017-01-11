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
    public class ATIVIDADESController : Controller
    {
        private PRINCIPAL db = new PRINCIPAL();

        // GET: ATIVIDADES
        public ActionResult Index()
        {
            return View(db.BASE_DE_DADOS.ToList());
        }

        // GET: ATIVIDADES/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BASE_DE_DADOS bASE_DE_DADOS = db.BASE_DE_DADOS.Find(id);
            if (bASE_DE_DADOS == null)
            {
                return HttpNotFound();
            }
            return View(bASE_DE_DADOS);
        }

        // GET: ATIVIDADES/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ATIVIDADES/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CD_BASE_DE_DADOS,NM_NOME_CATEGORIA,NM_NOME_SUBCATEGORIA,NM_NOME_ATIVIDADE")] BASE_DE_DADOS bASE_DE_DADOS)
        {
            if (ModelState.IsValid)
            {
                db.BASE_DE_DADOS.Add(bASE_DE_DADOS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bASE_DE_DADOS);
        }

        // GET: ATIVIDADES/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BASE_DE_DADOS bASE_DE_DADOS = db.BASE_DE_DADOS.Find(id);
            if (bASE_DE_DADOS == null)
            {
                return HttpNotFound();
            }
            return View(bASE_DE_DADOS);
        }

        // POST: ATIVIDADES/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CD_BASE_DE_DADOS,NM_NOME_CATEGORIA,NM_NOME_SUBCATEGORIA,NM_NOME_ATIVIDADE")] BASE_DE_DADOS bASE_DE_DADOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bASE_DE_DADOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bASE_DE_DADOS);
        }

        // GET: ATIVIDADES/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BASE_DE_DADOS bASE_DE_DADOS = db.BASE_DE_DADOS.Find(id);
            if (bASE_DE_DADOS == null)
            {
                return HttpNotFound();
            }
            return View(bASE_DE_DADOS);
        }

        // POST: ATIVIDADES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            BASE_DE_DADOS bASE_DE_DADOS = db.BASE_DE_DADOS.Find(id);
            db.BASE_DE_DADOS.Remove(bASE_DE_DADOS);
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
