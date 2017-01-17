using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DIVULGA_SERVICOS.Models;
using System.Text;
using System.Globalization;
using System.Data.Entity.Spatial;

namespace DIVULGA_SERVICOS.Controllers
{
    public class CAD_PES_JURIDICAController : Controller
    {
        private PRINCIPAL db = new PRINCIPAL();

        [HttpGet]
        public ActionResult Pesquisa(string pesquisa = "", string lat = "", string lng = "")
       {
            //var cAD_PES_CATEGORIA = new List<CAD_CATEGORIA>();
            var enderecos = new List<CAD_PES_ENDERECO>();
            IList<CAD_PES_ENDERECO> Prestadores = new List<CAD_PES_ENDERECO>();
            string texto = "";
            ViewBag.latitude = lat;
            ViewBag.longitude = lng;
            //var localusuário = DbGeography.FromText("POINT (" + lat + " " + lng + ")");
            

            if (!String.IsNullOrEmpty(pesquisa))
            {
                texto = RemoveAcento(pesquisa);
                
                    enderecos = db.CAD_PES_ENDERECO.Where(
                    x => x.CAD_PESSOA.CAD_PES_JURIDICA.CAD_CATEGORIA.FirstOrDefault().DS_DESCRICAO.Contains(texto) ||
                    x.CAD_PESSOA.CAD_PES_JURIDICA.CAD_CATEGORIA.FirstOrDefault().DS_DESCRICAO.Contains(pesquisa) ||
                    x.CAD_PESSOA.CAD_PES_JURIDICA.CAD_CATEGORIA.FirstOrDefault().NM_NOME.Contains(texto) ||
                    x.CAD_PESSOA.CAD_PES_JURIDICA.CAD_CATEGORIA.FirstOrDefault().NM_NOME.Contains(pesquisa) ||
                    x.CAD_PESSOA.DS_APELIDO_SITE.Contains(texto) ||
                    x.CAD_PESSOA.DS_APELIDO_SITE.Contains(pesquisa)).ToList();
                ////enderecos = db.CAD_PES_ENDERECO.Where(c => c.localizacao.Distance(localusuário) < 10).ToList<CAD_PES_ENDERECO>();
                //foreach (var i in enderecos)
                //{
                //    Prestadores.Add(db.CAD_PES_ENDERECO.Where(x => x.CD_PESSOA == i.CD_PESSOA && (x.CAD_PESSOA.CAD_PES_JURIDICA.CAD_CATEGORIA.Single().DS_DESCRICAO.Contains(texto) || x.CAD_PESSOA.CAD_PES_JURIDICA.CAD_CATEGORIA.Single().NM_NOME.Contains(texto) || x.CAD_PESSOA.DS_APELIDO_SITE.Contains(texto))).ToList<CAD_PES_ENDERECO>().Single());
                //    //enderecos.Add(db.CAD_PES_ENDERECO.Where(c => c.CD_PESSOA == i.CD_PES_JURIDICA && c.localizacao.Distance(localusuário) < 10).ToList<CAD_PES_ENDERECO>().Single());
                //}
                //ViewData["DadosEndereco"] = Prestadores;
            }
            else if(!String.IsNullOrEmpty(lat))
            {
                enderecos = db.CAD_PES_ENDERECO.Include(c => c.CAD_PESSOA).ToList();
                return View("Index", enderecos);
            }
            else
            {
                //cAD_PES_JURIDICA = db.CAD_PES_JURIDICA.Include(c => c.CAD_PESSOA).ToList<CAD_PES_JURIDICA>();            
                //enderecos = db.CAD_PES_ENDERECO.Include(c => c.CAD_PESSOA).ToList<CAD_PES_ENDERECO>();
                //ViewData["DadosEndereco"] = enderecos;
                return RedirectToAction("Index");
            }
            return View("Index", enderecos);
        }

        public ActionResult Index()
        {
            //var enderecos = new List<CAD_PES_ENDERECO>();

            //enderecos = db.CAD_PES_ENDERECO.Include(x => x.CAD_PESSOA).ToList();
            //ViewBag.lati = "";
            //ViewBag.longi = "";
            return View();
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

        public static string RemoveAcento(String texto)
        {
            String normalizedString = texto.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++)
            {
                Char c = normalizedString[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString().ToLower();
        }
    }
}


