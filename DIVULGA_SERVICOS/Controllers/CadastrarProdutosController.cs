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
    public class CadastrarProdutosController : Controller
    {
        private PRINCIPAL db = new PRINCIPAL();

        // GET: CadastrarProdutos
        public ActionResult Index()
        {
            var cAD_PRODUTO_FORNECEDOR = db.CAD_PRODUTO_FORNECEDOR.Include(c => c.CAD_PES_FORNECEDOR);
            return View(cAD_PRODUTO_FORNECEDOR.ToList());
        }

        // GET: CadastrarProdutos/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAD_PRODUTO_FORNECEDOR cAD_PRODUTO_FORNECEDOR = db.CAD_PRODUTO_FORNECEDOR.Find(id);
            if (cAD_PRODUTO_FORNECEDOR == null)
            {
                return HttpNotFound();
            }
            return View(cAD_PRODUTO_FORNECEDOR);
        }

        // GET: CadastrarProdutos/Create
        public ActionResult Create()
        {
            ViewBag.CD_PESSOA = new SelectList(db.CAD_PES_FORNECEDOR, "CD_PESSOA", "CD_CNPJ");
            return View();
        }

        // POST: CadastrarProdutos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CD_PESSOA,SQ_PRODUTO,NM_PRODUTO,DS_DESCRICAO,VALOR_PRODUTO,DT_CRIACAO,ATIVO,TAGS")] CAD_PRODUTO_FORNECEDOR cAD_PRODUTO_FORNECEDOR)
        {
            if (ModelState.IsValid)
            {
                db.CAD_PRODUTO_FORNECEDOR.Add(cAD_PRODUTO_FORNECEDOR);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CD_PESSOA = new SelectList(db.CAD_PES_FORNECEDOR, "CD_PESSOA", "CD_CNPJ", cAD_PRODUTO_FORNECEDOR.CD_PESSOA);
            return View(cAD_PRODUTO_FORNECEDOR);
        }

        // GET: CadastrarProdutos/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAD_PRODUTO_FORNECEDOR cAD_PRODUTO_FORNECEDOR = db.CAD_PRODUTO_FORNECEDOR.Find(id);
            if (cAD_PRODUTO_FORNECEDOR == null)
            {
                return HttpNotFound();
            }
            ViewBag.CD_PESSOA = new SelectList(db.CAD_PES_FORNECEDOR, "CD_PESSOA", "CD_CNPJ", cAD_PRODUTO_FORNECEDOR.CD_PESSOA);
            return View(cAD_PRODUTO_FORNECEDOR);
        }

        // POST: CadastrarProdutos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CD_PESSOA,SQ_PRODUTO,NM_PRODUTO,DS_DESCRICAO,VALOR_PRODUTO,DT_CRIACAO,ATIVO,TAGS")] CAD_PRODUTO_FORNECEDOR cAD_PRODUTO_FORNECEDOR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cAD_PRODUTO_FORNECEDOR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CD_PESSOA = new SelectList(db.CAD_PES_FORNECEDOR, "CD_PESSOA", "CD_CNPJ", cAD_PRODUTO_FORNECEDOR.CD_PESSOA);
            return View(cAD_PRODUTO_FORNECEDOR);
        }

        // GET: CadastrarProdutos/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAD_PRODUTO_FORNECEDOR cAD_PRODUTO_FORNECEDOR = db.CAD_PRODUTO_FORNECEDOR.Find(id);
            if (cAD_PRODUTO_FORNECEDOR == null)
            {
                return HttpNotFound();
            }
            return View(cAD_PRODUTO_FORNECEDOR);
        }

        // POST: CadastrarProdutos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CAD_PRODUTO_FORNECEDOR cAD_PRODUTO_FORNECEDOR = db.CAD_PRODUTO_FORNECEDOR.Find(id);
            db.CAD_PRODUTO_FORNECEDOR.Remove(cAD_PRODUTO_FORNECEDOR);
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
