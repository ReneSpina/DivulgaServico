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
    public class CAD_PES_JURIDICAController : Controller
    {
        private PRINCIPAL db = new PRINCIPAL();

        // GET: CAD_PES_JURIDICA
        public ActionResult Index()
        {

            var cAD_PES_JURIDICA = db.CAD_PES_JURIDICA.Include(c => c.CAD_PESSOA);
            IList<CAD_PES_ENDERECO> enderecos = new List<CAD_PES_ENDERECO>();



            enderecos = db.CAD_PES_ENDERECO.Include(c => c.CAD_PESSOA).ToList<CAD_PES_ENDERECO>();
            ViewData["DadosEndereco"] = enderecos;
            return View(cAD_PES_JURIDICA.ToList());
        }

        // GET: CAD_PES_JURIDICA/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAD_PES_JURIDICA cAD_PES_JURIDICA = db.CAD_PES_JURIDICA.Find(id);
            if (cAD_PES_JURIDICA == null)
            {
                return HttpNotFound();
            }
            return View(cAD_PES_JURIDICA);
        }

        // GET: CAD_PES_JURIDICA/Create
        public ActionResult Create()
        {
            ViewBag.CD_PESSOA = new SelectList(db.CAD_PESSOA, "Id", "NM_NOME_PESSOA");
            return View();
        }

        // POST: CAD_PES_JURIDICA/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CD_PESSOA,CD_CODIGO_INDICACAO,CD_CNPJ,DS_LINK_SITE,DS_SOBRE,DS_QUEM_SOMOS,ID_PLANO")] CAD_PES_JURIDICA cAD_PES_JURIDICA)
        {
            if (ModelState.IsValid)
            {
                db.CAD_PES_JURIDICA.Add(cAD_PES_JURIDICA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CD_PESSOA = new SelectList(db.CAD_PESSOA, "Id", "NM_NOME_PESSOA", cAD_PES_JURIDICA.CD_PESSOA);
            return View(cAD_PES_JURIDICA);
        }

        // GET: CAD_PES_JURIDICA/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAD_PES_JURIDICA cAD_PES_JURIDICA = db.CAD_PES_JURIDICA.Find(id);
            if (cAD_PES_JURIDICA == null)
            {
                return HttpNotFound();
            }
            ViewBag.CD_PESSOA = new SelectList(db.CAD_PESSOA, "Id", "NM_NOME_PESSOA", cAD_PES_JURIDICA.CD_PESSOA);
            return View(cAD_PES_JURIDICA);
        }

        // POST: CAD_PES_JURIDICA/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CD_PESSOA,CD_CODIGO_INDICACAO,CD_CNPJ,DS_LINK_SITE,DS_SOBRE,DS_QUEM_SOMOS,ID_PLANO")] CAD_PES_JURIDICA cAD_PES_JURIDICA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cAD_PES_JURIDICA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CD_PESSOA = new SelectList(db.CAD_PESSOA, "Id", "NM_NOME_PESSOA", cAD_PES_JURIDICA.CD_PESSOA);
            return View(cAD_PES_JURIDICA);
        }

        // GET: CAD_PES_JURIDICA/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAD_PES_JURIDICA cAD_PES_JURIDICA = db.CAD_PES_JURIDICA.Find(id);
            if (cAD_PES_JURIDICA == null)
            {
                return HttpNotFound();
            }
            return View(cAD_PES_JURIDICA);
        }

        // POST: CAD_PES_JURIDICA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CAD_PES_JURIDICA cAD_PES_JURIDICA = db.CAD_PES_JURIDICA.Find(id);
            db.CAD_PES_JURIDICA.Remove(cAD_PES_JURIDICA);
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
