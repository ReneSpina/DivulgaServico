using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DIVULGA_SERVICOS.Models;
using System.Data.Entity.Migrations;

namespace DIVULGA_SERVICOS.Controllers
{
    public class AvaliacaoController : Controller
    {
        private PRINCIPAL db = new PRINCIPAL();

        // GET: Avaliacao
        public ActionResult Index()
        {
            var cAD_AVALIACAO = db.CAD_AVALIACAO.Include(c => c.CAD_PES_USUARIO);
            return View(cAD_AVALIACAO.ToList());
        }

        // GET: Avaliacao/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAD_AVALIACAO cAD_AVALIACAO = db.CAD_AVALIACAO.Find(id);
            if (cAD_AVALIACAO == null)
            {
                return HttpNotFound();
            }
            return View(cAD_AVALIACAO);
        }

        // GET: Avaliacao/Create
        public ActionResult Create()
        {
            ViewBag.CD_PES_USUARIO = new SelectList(db.CAD_PES_USUARIO, "CD_PESSOA", "CD_PESSOA");
            return View();
        }

        // POST: Avaliacao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "CD_PES_JURIDICA,CD_PES_USUARIO,PRECO_QUALIDADE,PONTUALIDADE,ORGANIZACAO,INDICACAO,SATISFACAO_SERVICO,DS_DESCRICAO")] CAD_AVALIACAO cAD_AVALIACAO, string returnUrl)
        {
            string url = Request.UrlReferrer.PathAndQuery;
            if (ModelState.IsValid)
            {
                //CAD_AVALIACAO avaliacaoExiste = db.CAD_AVALIACAO.Where(x => x.CD_PES_JURIDICA == cAD_AVALIACAO.CD_PES_JURIDICA && x.CD_PES_USUARIO == cAD_AVALIACAO.CD_PES_USUARIO).First();
                //if(avaliacaoExiste == null)
                //{
                    db.CAD_AVALIACAO.AddOrUpdate(cAD_AVALIACAO);
                    db.SaveChanges();
                    return Redirect(url);
                //}
                //else
                //{
                //    //db.CAD_AVALIACAO.Add(cAD_AVALIACAO);
                //    //db.Entry(cAD_AVALIACAO).State = EntityState.Added;
                //    db.Entry(cAD_AVALIACAO).State = EntityState.Modified;
                //    db.SaveChanges();
                //    return Redirect(url);
                //}
            }

            //ViewBag.CD_PES_USUARIO = new SelectList(db.CAD_PES_USUARIO, "CD_PESSOA", "CD_PESSOA", cAD_AVALIACAO.CD_PES_USUARIO);
            return Redirect(url);
        }

        //// GET: Avaliacao/Edit/5
        //public ActionResult Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CAD_AVALIACAO cAD_AVALIACAO = db.CAD_AVALIACAO.Find(id);
        //    if (cAD_AVALIACAO == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.CD_PES_USUARIO = new SelectList(db.CAD_PES_USUARIO, "CD_PESSOA", "CD_PESSOA", cAD_AVALIACAO.CD_PES_USUARIO);
        //    return View(cAD_AVALIACAO);
        //}

        //// POST: Avaliacao/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "CD_PES_JURIDICA,CD_PES_USUARIO,NT_MEDIA,PRECO_QUALIDADE,PONTUALIDADE,ORGANIZACAO,INDICACAO,SATISFACAO_ATENDIMENTO,SATISFACAO_SERVICO,DS_DESCRICAO")] CAD_AVALIACAO cAD_AVALIACAO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(cAD_AVALIACAO).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.CD_PES_USUARIO = new SelectList(db.CAD_PES_USUARIO, "CD_PESSOA", "CD_PESSOA", cAD_AVALIACAO.CD_PES_USUARIO);
        //    return View(cAD_AVALIACAO);
        //}

        // GET: Avaliacao/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAD_AVALIACAO cAD_AVALIACAO = db.CAD_AVALIACAO.Find(id);
            if (cAD_AVALIACAO == null)
            {
                return HttpNotFound();
            }
            return View(cAD_AVALIACAO);
        }

        // POST: Avaliacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CAD_AVALIACAO cAD_AVALIACAO = db.CAD_AVALIACAO.Find(id);
            db.CAD_AVALIACAO.Remove(cAD_AVALIACAO);
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
