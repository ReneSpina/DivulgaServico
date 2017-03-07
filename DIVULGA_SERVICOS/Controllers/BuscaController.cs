﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DIVULGA_SERVICOS.Models;
using System.Text;
using System.Globalization;
using System.Data.Entity.Spatial;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Principal;
using System.Web;

namespace DIVULGA_SERVICOS.Controllers
{
    public class BuscaController : Controller
    {
        private ApplicationUserManager _userManager;
        private PRINCIPAL db = new PRINCIPAL();

        public BuscaController()
        {
        }

        public BuscaController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        [HttpGet]
        public ActionResult Pesquisa(string pesquisa = "", string lat = "", string lng = "", int distancia = 0, bool aberto = false, bool vintequatro = false, bool aceitacartao = false)
        {
            //var cAD_PES_CATEGORIA = new List<CAD_CATEGORIA>();
            var enderecos = new List<CAD_PES_ENDERECO>();
            var enderecosTemp = new List<CAD_PES_ENDERECO>();
            var atendimentos = new List<CAD_HORA_ATENDIMENTO>();
            ViewBag.latitude = lat;
            ViewBag.longitude = lng;
            ViewBag.Status = 0;
            var status = 0;
            var localusuario = DbGeography.FromText("POINT (" + lat + " " + lng + ")", 4326);

            if (String.IsNullOrEmpty(lat))
            {
                ViewBag.Status = 1;
                ViewBag.erro = "Nao conseguimos identificar sua localizacao. Recarregue a pagina e digite seu endereco na busca avancada!";
                return View("Index", enderecos);
            }
            else if (pesquisa.Contains(" "))
            {
                string[] words = pesquisa.Split(' ');
                foreach (var word in words)
                {
                    if (word != "de" || word != "para")
                    {
                        var texto = "";
                        if (distancia != 0)
                        {
                            texto = RemoveAcento(word);
                            enderecosTemp = db.CAD_PES_ENDERECO
                                    .Where(x => x.CAD_PESSOA.ATIVADO == true)
                                    .Where(x => ((int)(((x.localizacao.Distance(localusuario)) / 0.62137) / 1000) <= (distancia)))
                                    .Where(x =>
                                            x.CAD_PESSOA.CAD_PES_JURIDICA.CAD_CATEGORIA.FirstOrDefault().DS_DESCRICAO.Contains(texto) ||
                                            x.CAD_PESSOA.CAD_PES_JURIDICA.CAD_CATEGORIA.FirstOrDefault().DS_DESCRICAO.Contains(pesquisa) ||
                                            x.CAD_PESSOA.CAD_PES_JURIDICA.CAD_CATEGORIA.FirstOrDefault().NM_NOME.Contains(texto) ||
                                            x.CAD_PESSOA.CAD_PES_JURIDICA.CAD_CATEGORIA.FirstOrDefault().NM_NOME.Contains(pesquisa) ||
                                            x.CAD_PESSOA.NM_NOME_PESSOA.Contains(texto) ||
                                            x.CAD_PESSOA.NM_NOME_PESSOA.Contains(pesquisa)).ToList();
                        }
                        else
                        {
                            texto = RemoveAcento(word);
                            enderecosTemp = db.CAD_PES_ENDERECO
                                .Where(x => x.CAD_PESSOA.ATIVADO == true)
                                .Where(x =>
                                        x.CAD_PESSOA.CAD_PES_JURIDICA.CAD_CATEGORIA.FirstOrDefault().DS_DESCRICAO.Contains(texto) ||
                                        x.CAD_PESSOA.CAD_PES_JURIDICA.CAD_CATEGORIA.FirstOrDefault().DS_DESCRICAO.Contains(pesquisa) ||
                                        x.CAD_PESSOA.CAD_PES_JURIDICA.CAD_CATEGORIA.FirstOrDefault().NM_NOME.Contains(texto) ||
                                        x.CAD_PESSOA.CAD_PES_JURIDICA.CAD_CATEGORIA.FirstOrDefault().NM_NOME.Contains(pesquisa) ||
                                        x.CAD_PESSOA.NM_NOME_PESSOA.Contains(texto) ||
                                        x.CAD_PESSOA.NM_NOME_PESSOA.Contains(pesquisa)).ToList();
                        }
                        if (enderecos.Any((item => enderecosTemp.Contains(item))))
                        {

                        }
                        else
                        {
                            if (aberto == true)
                            {
                                for (int i = enderecosTemp.Count - 1; i >= 0; i--)
                                {
                                    if (enderecosTemp[i].CAD_PESSOA.CAD_PES_JURIDICA.TODO_DIA == true)
                                    {

                                    }
                                    else
                                    {
                                        status = 0;
                                        atendimentos = db.CAD_HORA_ATENDIMENTO.Where(x => x.CD_PES_JURIDICA == enderecosTemp[i].CD_PESSOA &&
                                        x.DIA_SEMANA == (int)DateTime.Today.DayOfWeek).ToList();
                                        foreach (var atendimento in atendimentos)
                                        {
                                            if (atendimento.HORA_INICIO <= DateTime.Now.Hour && atendimento.HORA_FIM > DateTime.Now.Hour)
                                            {

                                            }
                                            else
                                            {
                                                status = 1;
                                            }
                                        }
                                        if (status == 1)
                                        {
                                            enderecosTemp.Remove(enderecosTemp[i]);
                                        }
                                    }

                                }
                            }
                            if (vintequatro == true)
                            {
                                for (int i = enderecosTemp.Count - 1; i >= 0; i--)
                                {
                                    if (enderecosTemp[i].CAD_PESSOA.CAD_PES_JURIDICA.TODO_DIA == true)
                                    {

                                    }
                                    else
                                    {
                                        enderecosTemp.Remove(enderecosTemp[i]);
                                    }

                                }
                            }

                            if (aceitacartao == true)
                            {
                                for (int i = enderecosTemp.Count - 1; i >= 0; i--)
                                {
                                    if (enderecosTemp[i].CAD_PESSOA.CAD_PES_JURIDICA.CAD_FORMA_PAGAMENTO.CREDITO == true ||
                                        enderecosTemp[i].CAD_PESSOA.CAD_PES_JURIDICA.CAD_FORMA_PAGAMENTO.DEBITO == true)
                                    {

                                    }
                                    else
                                    {
                                        enderecosTemp.Remove(enderecosTemp[i]);
                                    }

                                }
                            }
                            enderecos.AddRange(enderecosTemp.ToList());
                        }
                    }
                }
            }
            else if (!String.IsNullOrEmpty(pesquisa))
            {
                var texto = "";
                if (distancia != 0)
                {
                    texto = RemoveAcento(pesquisa);
                    enderecos = db.CAD_PES_ENDERECO
                        .Where(x => x.CAD_PESSOA.ATIVADO == true)
                        .Where(x => ((int)(((x.localizacao.Distance(localusuario)) / 0.62137) / 1000) <= (distancia)))
                        .Where(x =>
                                    x.CAD_PESSOA.CAD_PES_JURIDICA.CAD_CATEGORIA.FirstOrDefault().DS_DESCRICAO.Contains(texto) ||
                                    x.CAD_PESSOA.CAD_PES_JURIDICA.CAD_CATEGORIA.FirstOrDefault().DS_DESCRICAO.Contains(pesquisa) ||
                                    x.CAD_PESSOA.CAD_PES_JURIDICA.CAD_CATEGORIA.FirstOrDefault().NM_NOME.Contains(texto) ||
                                    x.CAD_PESSOA.CAD_PES_JURIDICA.CAD_CATEGORIA.FirstOrDefault().NM_NOME.Contains(pesquisa) ||
                                    x.CAD_PESSOA.NM_NOME_PESSOA.Contains(texto) ||
                                    x.CAD_PESSOA.NM_NOME_PESSOA.Contains(pesquisa)).ToList();
                }
                else
                {
                    texto = RemoveAcento(pesquisa);
                    enderecos = db.CAD_PES_ENDERECO
                    .Where(x => x.CAD_PESSOA.ATIVADO == true)
                    .Where(x =>
                                x.CAD_PESSOA.CAD_PES_JURIDICA.CAD_CATEGORIA.FirstOrDefault().DS_DESCRICAO.Contains(texto) ||
                                x.CAD_PESSOA.CAD_PES_JURIDICA.CAD_CATEGORIA.FirstOrDefault().DS_DESCRICAO.Contains(pesquisa) ||
                                x.CAD_PESSOA.CAD_PES_JURIDICA.CAD_CATEGORIA.FirstOrDefault().NM_NOME.Contains(texto) ||
                                x.CAD_PESSOA.CAD_PES_JURIDICA.CAD_CATEGORIA.FirstOrDefault().NM_NOME.Contains(pesquisa) ||
                                x.CAD_PESSOA.NM_NOME_PESSOA.Contains(texto) ||
                                x.CAD_PESSOA.NM_NOME_PESSOA.Contains(pesquisa)).ToList();
                }
                if (aberto == true)
                {
                    for (int i = enderecos.Count - 1; i >= 0; i--)
                    {
                        if (enderecos[i].CAD_PESSOA.CAD_PES_JURIDICA.TODO_DIA == true)
                        {

                        }
                        else
                        {
                            status = 0;
                            atendimentos = db.CAD_HORA_ATENDIMENTO.Where(x => x.CD_PES_JURIDICA == enderecos[i].CD_PESSOA &&
                            x.DIA_SEMANA == (int)DateTime.Today.DayOfWeek).ToList();
                            foreach (var atendimento in atendimentos)
                            {
                                if (atendimento.HORA_INICIO <= DateTime.Now.Hour && atendimento.HORA_FIM > DateTime.Now.Hour)
                                {

                                }
                                else
                                {
                                    status = 1;
                                }
                            }
                            if (status == 1)
                            {
                                enderecos.Remove(enderecos[i]);
                            }
                        }

                    }
                }
                if (vintequatro == true)
                {
                    for (int i = enderecos.Count - 1; i >= 0; i--)
                    {
                        if (enderecos[i].CAD_PESSOA.CAD_PES_JURIDICA.TODO_DIA == true)
                        {

                        }
                        else
                        {
                            enderecos.Remove(enderecos[i]);
                        }

                    }
                }

                if (aceitacartao == true)
                {
                    for (int i = enderecos.Count - 1; i >= 0; i--)
                    {
                        if (enderecos[i].CAD_PESSOA.CAD_PES_JURIDICA.CAD_FORMA_PAGAMENTO.CREDITO == true ||
                            enderecos[i].CAD_PESSOA.CAD_PES_JURIDICA.CAD_FORMA_PAGAMENTO.DEBITO == true)
                        {

                        }
                        else
                        {
                            enderecos.Remove(enderecos[i]);
                        }

                    }
                }
            }
            else
            {
                return RedirectToAction("Index", enderecos);
            }
            //distancia = (int)(((endereco.s.LastOrDefault().localizacao.Distance(localusuario))/0.62137)/1000);
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

        [HttpGet]
        public async Task<ActionResult> AlertaPrestador(string email = "")
        {
            var user = db.CAD_PESSOA.Where(x => x.Email == email).FirstOrDefault();
            if (user == null)
            {
                ViewBag.errorMessage = "Não conseguimos identificar este prestador de serviços";
                return View("Error");
            }
            //string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
            //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
            await UserManager.SendEmailAsync(user.Id, "Alerta Sobre Prestador de Servico", "Atenção ao prestador "+user.NM_NOME_PESSOA+"!");
            return Redirect(Request.UrlReferrer.PathAndQuery);
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
        public ActionResult Create([Bind(Include = "CD_PESSOA, CD_CNPJ,DS_LINK_SITE,DS_SOBRE,DS_QUEM_SOMOS,ID_PLANO")] CAD_PES_JURIDICA cAD_PES_JURIDICA)
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


