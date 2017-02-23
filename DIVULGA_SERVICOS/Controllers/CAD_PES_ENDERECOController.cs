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
    public class CAD_PES_ENDERECOController : Controller
    {
        private PRINCIPAL db = new PRINCIPAL();

        // GET: CAD_PES_ENDERECO
        public ActionResult Index()
        {
            var cAD_PES_ENDERECO = db.CAD_PES_ENDERECO.Include(c => c.CAD_PESSOA);
            return View(cAD_PES_ENDERECO.ToList());
        }


        // GET: CAD_PES_ENDERECO/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAD_PES_ENDERECO cAD_PES_ENDERECO = db.CAD_PES_ENDERECO.Find(id);
            if (cAD_PES_ENDERECO == null)
            {
                return HttpNotFound();
            }
            return View(cAD_PES_ENDERECO);
        }

        // GET: CAD_PES_ENDERECO/Create
        public ActionResult Create()
        {
            ViewBag.CD_PESSOA = new SelectList(db.CAD_PESSOA, "Id", "NM_NOME_PESSOA");
            return View();
        }

        // POST: CAD_PES_ENDERECO/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CD_PESSOA,SQ_ENDERECO,NM_CIDADE,NM_LOGRADOURO,NM_BAIRRO,NM_ESTADO,CD_CEP,NUMERO,localizacao")] CAD_PES_ENDERECO cAD_PES_ENDERECO)
        {
            if (ModelState.IsValid)
            {
                db.CAD_PES_ENDERECO.Add(cAD_PES_ENDERECO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CD_PESSOA = new SelectList(db.CAD_PESSOA, "Id", "NM_NOME_PESSOA", cAD_PES_ENDERECO.CD_PESSOA);
            return View(cAD_PES_ENDERECO);
        }

        // GET: CAD_PES_ENDERECO/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAD_PES_ENDERECO cAD_PES_ENDERECO = db.CAD_PES_ENDERECO.Find(id);
            if (cAD_PES_ENDERECO == null)
            {
                return HttpNotFound();
            }
            ViewBag.CD_PESSOA = new SelectList(db.CAD_PESSOA, "Id", "NM_NOME_PESSOA", cAD_PES_ENDERECO.CD_PESSOA);
            return View(cAD_PES_ENDERECO);
        }

        // POST: CAD_PES_ENDERECO/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CD_PESSOA,SQ_ENDERECO,NM_CIDADE,NM_LOGRADOURO,NM_BAIRRO,NM_ESTADO,CD_CEP,NUMERO,localizacao")] CAD_PES_ENDERECO cAD_PES_ENDERECO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cAD_PES_ENDERECO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CD_PESSOA = new SelectList(db.CAD_PESSOA, "Id", "NM_NOME_PESSOA", cAD_PES_ENDERECO.CD_PESSOA);
            return View(cAD_PES_ENDERECO);
        }

        // GET: CAD_PES_ENDERECO/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAD_PES_ENDERECO cAD_PES_ENDERECO = db.CAD_PES_ENDERECO.Find(id);
            if (cAD_PES_ENDERECO == null)
            {
                return HttpNotFound();
            }
            return View(cAD_PES_ENDERECO);
        }

        // POST: CAD_PES_ENDERECO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CAD_PES_ENDERECO cAD_PES_ENDERECO = db.CAD_PES_ENDERECO.Find(id);
            db.CAD_PES_ENDERECO.Remove(cAD_PES_ENDERECO);
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
