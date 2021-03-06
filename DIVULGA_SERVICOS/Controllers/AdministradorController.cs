﻿using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using DIVULGA_SERVICOS.Models;
using System.Data.Entity;
using System.Net;
using System.Collections.Generic;
using System.Text;

namespace DIVULGA_SERVICOS.Controllers
{
    [Authorize]
    public class AdministradorController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private PRINCIPAL db = new PRINCIPAL();

        public AdministradorController()
        {
        }

        public AdministradorController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
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

        // GET: Administrador
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var cAD_PESSOA = db.CAD_PESSOA.Include(c => c.CAD_PES_FORNECEDOR).Include(c => c.CAD_PES_JURIDICA).Include(c => c.CAD_PES_USUARIO).Count();
            ViewBag.quantidade = cAD_PESSOA;
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Lista()
        {
            var cAD_PESSOA = db.CAD_PESSOA.Include(c => c.CAD_PES_FORNECEDOR).Include(c => c.CAD_PES_JURIDICA).Include(c => c.CAD_PES_USUARIO).OrderByDescending(x => x.DT_DATA_CADASTRO);
            return View(cAD_PESSOA.ToList());
        }

        // GET: Administrador/Details/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CAD_PESSOA cAD_PESSOA = db.CAD_PESSOA.Find(id);
            CAD_PES_JURIDICA juridica = db.CAD_PES_JURIDICA.Find(id);
            CAD_PES_FORNECEDOR fornecedor = db.CAD_PES_FORNECEDOR.Find(id);

            if(juridica != null)
            {
                var removeRole = UserManager.RemoveFromRole(juridica.CD_PESSOA, "Prestador");
                CAD_FORMA_PAGAMENTO formaPagamento = db.CAD_FORMA_PAGAMENTO.Find(id);
                CAD_PORTE_EMPRESA porteEmpresa = db.CAD_PORTE_EMPRESA.Find(id);

                if (formaPagamento != null)
                {
                    formaPagamento = db.CAD_FORMA_PAGAMENTO.Remove(formaPagamento);
                }
                if (porteEmpresa != null)
                {
                    porteEmpresa = db.CAD_PORTE_EMPRESA.Remove(porteEmpresa);
                }

                var telefones = new List<CAD_PES_FONE>();
                telefones = db.CAD_PES_FONE.Where(x => x.CD_PESSOA == id).ToList();
                for (int i = telefones.Count - 1; i >= 0; i--)
                {
                    telefones.Remove(telefones[i]);
                }

                var enderecos = new List<CAD_PES_ENDERECO>();
                enderecos = db.CAD_PES_ENDERECO.Where(x => x.CD_PESSOA == id).ToList();
                for (int i = enderecos.Count - 1; i >= 0; i--)
                {
                    enderecos.Remove(enderecos[i]);
                }

                var horarios = new List<CAD_HORA_ATENDIMENTO>();
                horarios = db.CAD_HORA_ATENDIMENTO.Where(x => x.CD_PES_JURIDICA == id).ToList();
                for (int i = horarios.Count - 1; i >= 0; i--)
                {
                    horarios.Remove(horarios[i]);
                }

                var atividades = new List<CAD_CATEGORIA>();
                atividades = db.CAD_CATEGORIA.Where(x => x.CD_PES_JURIDICA == id).ToList();
                for (int i = atividades.Count - 1; i >= 0; i--)
                {
                    atividades.Remove(atividades[i]);
                }

                var avaliacao = new List<CAD_AVALIACAO>();
                avaliacao = db.CAD_AVALIACAO.Where(x => x.CD_PES_JURIDICA == id).ToList();
                for (int i = avaliacao.Count - 1; i >= 0; i--)
                {
                    avaliacao.Remove(avaliacao[i]);
                }
            }
            if(fornecedor != null)
            {
                var removeRole = UserManager.RemoveFromRole(id, "Fornecedor");
                var telefones = new List<CAD_PES_FONE>();
                telefones = db.CAD_PES_FONE.Where(x => x.CD_PESSOA == id).ToList();
                for (int i = telefones.Count - 1; i >= 0; i--)
                {
                    telefones.Remove(telefones[i]);
                }

                var enderecos = new List<CAD_PES_ENDERECO>();
                enderecos = db.CAD_PES_ENDERECO.Where(x => x.CD_PESSOA == id).ToList();
                for (int i = enderecos.Count - 1; i >= 0; i--)
                {
                    enderecos.Remove(enderecos[i]);
                }

                var enderecosDilvulga = new List<CAD_CIDADES_DIVULGA_FORNECEDOR>();
                enderecosDilvulga = db.CAD_CIDADES_DIVULGA_FORNECEDOR.Where(x => x.CD_PESSOA == id).ToList();
                for (int i = enderecosDilvulga.Count - 1; i >= 0; i--)
                {
                    enderecosDilvulga.Remove(enderecosDilvulga[i]);
                }

                var produtos = new List<CAD_PRODUTO_FORNECEDOR>();
                produtos = db.CAD_PRODUTO_FORNECEDOR.Where(x => x.CD_PESSOA == id).ToList();
                for (int i = produtos.Count - 1; i >= 0; i--)
                {
                    produtos.Remove(produtos[i]);
                }

                var avaliacao = new List<CAD_AVALIACAO>();
                avaliacao = db.CAD_AVALIACAO.Where(x => x.CD_AVALIADOR == id).ToList();
                for (int i = avaliacao.Count - 1; i >= 0; i--)
                {
                    avaliacao.Remove(avaliacao[i]);
                }
            }

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




        /*Início da administração das categorias de prestadores de serviços*/

        [Authorize(Roles = "Admin")]
        public ActionResult Servicos(string id)
        {
            var cAD_CATEGORIA = db.CAD_CATEGORIA.Where(c => c.CD_PES_JURIDICA == id);
            return View("Servicos", cAD_CATEGORIA.ToList());
        }

        [Authorize(Roles = "Admin")]
        //[ValidateAntiForgeryToken]
        public ActionResult DetalhesServico(string id, long sq)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userId = id;
            var sequencia = sq;
            var cAD_CATEGORIA = db.CAD_CATEGORIA
                .Where(x => x.CD_PES_JURIDICA == userId)
                .Where(x => x.SQ_CATEGORIA == sequencia).FirstOrDefault();

            if (cAD_CATEGORIA == null)
            {
                return HttpNotFound();
            }
            return View("DetalhesServico", cAD_CATEGORIA);
        }

        // GET: CAD_CATEGORIA/Create
        [Authorize(Roles = "Admin")]
        public ActionResult CriarServico()
        {
            ViewBag.CD_PES_JURIDICA = new SelectList(db.CAD_PES_JURIDICA, "CD_PESSOA", "NM_NOME_PRESTADOR");
            return View();
        }

        // POST: CAD_CATEGORIA/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult CriarServico([Bind(Include = "NM_NOME,DS_DESCRICAO, SQ_CATEGORIA, CD_PES_JURIDICA")] CAD_CATEGORIA cAD_CATEGORIA, string id)
        {
            if (ModelState.IsValid)
            {
                var nome_servico = "";
                var ds_descricao = "";
                StringBuilder texto_final = new StringBuilder();

                nome_servico = RemoveAcento(cAD_CATEGORIA.NM_NOME);
                ds_descricao = RemoveAcento(cAD_CATEGORIA.DS_DESCRICAO);
                texto_final = texto_final.Append(cAD_CATEGORIA.NM_NOME).Append(", ").Append(cAD_CATEGORIA.DS_DESCRICAO).Append(", ").Append(nome_servico).Append(", ").Append(ds_descricao);
                cAD_CATEGORIA.DS_DESCRICAO = texto_final.ToString();
                db.CAD_CATEGORIA.Add(cAD_CATEGORIA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CD_PES_JURIDICA = new SelectList(db.CAD_PES_JURIDICA, "CD_PESSOA", "NM_NOME_PRESTADOR", cAD_CATEGORIA.CD_PES_JURIDICA);
            return View(cAD_CATEGORIA);
        }

        //// GET: CAD_CATEGORIA/Edit/5
        //[Authorize(Roles = "Admin")]
        //public ActionResult EditarServico(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CAD_CATEGORIA cAD_CATEGORIA = db.CAD_CATEGORIA.Find(id);
        //    if (cAD_CATEGORIA == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.CD_PES_JURIDICA = new SelectList(db.CAD_PES_JURIDICA, "CD_PESSOA", "CD_CNPJ", cAD_CATEGORIA.CD_PES_JURIDICA);
        //    return View(cAD_CATEGORIA);
        //}

        //// POST: CAD_CATEGORIA/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[Authorize(Roles = "Admin")]
        //[ValidateAntiForgeryToken]
        //public ActionResult EditarServico([Bind(Include = "SQ_CATEGORIA,CD_PES_JURIDICA,NM_NOME,SHOW,DS_DESCRICAO")] CAD_CATEGORIA cAD_CATEGORIA)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(cAD_CATEGORIA).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Servicos", new { id = cAD_CATEGORIA.CD_PES_JURIDICA });
        //    }
        //    ViewBag.CD_PES_JURIDICA = new SelectList(db.CAD_PES_JURIDICA, "CD_PESSOA", "CD_CNPJ", cAD_CATEGORIA.CD_PES_JURIDICA);
        //    return View(cAD_CATEGORIA);
        //}

        //// GET: CAD_CATEGORIA/Delete/5
        //[Authorize(Roles = "Admin")]
        //public ActionResult DeletarServico(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CAD_CATEGORIA cAD_CATEGORIA = db.CAD_CATEGORIA.Find(id);
        //    if (cAD_CATEGORIA == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(cAD_CATEGORIA);
        //}

        //// POST: CAD_CATEGORIA/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[Authorize(Roles = "Admin")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeletarServicoOk(string id)
        //{
        //    CAD_CATEGORIA cAD_CATEGORIA = db.CAD_CATEGORIA.Find(id);
        //    db.CAD_CATEGORIA.Remove(cAD_CATEGORIA);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        /*Fim da administração das categorias de prestadores de serviços*/


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
