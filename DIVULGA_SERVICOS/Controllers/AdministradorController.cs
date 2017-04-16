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
    public class AdministradorController : Controller
    {
        private PRINCIPAL db = new PRINCIPAL();

        // GET: Administrador
        public ActionResult Index()
        {
            var cAD_PESSOA = db.CAD_PESSOA.Include(c => c.CAD_PES_FORNECEDOR).Include(c => c.CAD_PES_JURIDICA).Include(c => c.CAD_PES_USUARIO);
            return View(cAD_PESSOA.ToList());
        }

        // GET: Administrador/Details/5
        [Authorize(Roles = "Prestador")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAD_PESSOA cAD_PESSOA = db.CAD_PESSOA.Find(id);
            if (cAD_PESSOA == null)
            {
                return HttpNotFound();
            }
            return View(cAD_PESSOA);
        }

        // GET: Administrador/Create
        [Authorize(Roles = "Prestador")]
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.CAD_PES_FORNECEDOR, "CD_PESSOA", "CD_CNPJ");
            ViewBag.Id = new SelectList(db.CAD_PES_JURIDICA, "CD_PESSOA", "CD_CNPJ");
            ViewBag.Id = new SelectList(db.CAD_PES_USUARIO, "CD_PESSOA", "CD_PESSOA");
            return View();
        }

        // POST: Administrador/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Prestador")]
        public ActionResult Create([Bind(Include = "Id,NM_NOME_PESSOA,NEWSLETTER,DT_DATA_CADASTRO,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] CAD_PESSOA cAD_PESSOA)
        {
            if (ModelState.IsValid)
            {
                db.CAD_PESSOA.Add(cAD_PESSOA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.CAD_PES_FORNECEDOR, "CD_PESSOA", "CD_CNPJ", cAD_PESSOA.Id);
            ViewBag.Id = new SelectList(db.CAD_PES_JURIDICA, "CD_PESSOA", "CD_CNPJ", cAD_PESSOA.Id);
            ViewBag.Id = new SelectList(db.CAD_PES_USUARIO, "CD_PESSOA", "CD_PESSOA", cAD_PESSOA.Id);
            return View(cAD_PESSOA);
        }

        // GET: Administrador/Edit/5
        [Authorize(Roles = "Prestador")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAD_PESSOA cAD_PESSOA = db.CAD_PESSOA.Find(id);
            if (cAD_PESSOA == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.CAD_PES_FORNECEDOR, "CD_PESSOA", "CD_CNPJ", cAD_PESSOA.Id);
            ViewBag.Id = new SelectList(db.CAD_PES_JURIDICA, "CD_PESSOA", "CD_CNPJ", cAD_PESSOA.Id);
            ViewBag.Id = new SelectList(db.CAD_PES_USUARIO, "CD_PESSOA", "CD_PESSOA", cAD_PESSOA.Id);
            return View(cAD_PESSOA);
        }

        // POST: Administrador/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Prestador")]
        public ActionResult Edit([Bind(Include = "Id,NM_NOME_PESSOA,NEWSLETTER,DT_DATA_CADASTRO,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] CAD_PESSOA cAD_PESSOA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cAD_PESSOA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.CAD_PES_FORNECEDOR, "CD_PESSOA", "CD_CNPJ", cAD_PESSOA.Id);
            ViewBag.Id = new SelectList(db.CAD_PES_JURIDICA, "CD_PESSOA", "CD_CNPJ", cAD_PESSOA.Id);
            ViewBag.Id = new SelectList(db.CAD_PES_USUARIO, "CD_PESSOA", "CD_PESSOA", cAD_PESSOA.Id);
            return View(cAD_PESSOA);
        }

        // GET: Administrador/Delete/5
        [Authorize(Roles = "Prestador")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAD_PESSOA cAD_PESSOA = db.CAD_PESSOA.Find(id);
            if (cAD_PESSOA == null)
            {
                return HttpNotFound();
            }
            return View(cAD_PESSOA);
        }

        // POST: Administrador/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Prestador")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CAD_PESSOA cAD_PESSOA = db.CAD_PESSOA.Find(id);
            db.CAD_PESSOA.Remove(cAD_PESSOA);
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
