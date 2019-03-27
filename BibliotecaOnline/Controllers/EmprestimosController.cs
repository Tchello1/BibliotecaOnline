using BibliotecaOnline.Models;
using BibliotecaOnline.Models.Enum;
using BibliotecaOnline.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace BibliotecaOnline.Controllers
{
    public class EmprestimosController : Controller
    {
        private Context db = new Context();

        // GET: Emprestimos
        //public ActionResult Index()
        //{
        //    IQueryable<EmprestimosViewModel> exemplares = (from l in db.Exemplares.Include(l => l.Livros)
        //                                                   join c in db.Cidades on l.Campos equals c.Codigo
        //                                                   join e in db.Emprestimos on l.Livros.Exemplares equals e.Emprestimos
        //                                                   select new EmprestimosViewModel
        //                                                   {
        //                                                       CodigoDeBarras = l.CodigoDeBarras,
        //                                                       Titulo = l.Livros.Titulo,
        //                                                       Edicao = l.Livros.Edicao,
        //                                                       Autor = l.Livros.Autor,
        //                                                       Editora = l.Livros.Editora,
        //                                                       Campus = "",
                                                               
        //                                                   });

        //    return View(exemplares.ToList());
        //}

        // GET: Emprestimos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emprestimo emprestimo = db.Emprestimos.Find(id);
            if (emprestimo == null)
            {
                return HttpNotFound();
            }
            return View(emprestimo);
        }

        // GET: Emprestimos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Emprestimos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,ColaboradorId,UsuarioId,DataEmprestimo,Status")] Emprestimo emprestimo, List<EmprestimoItens> exemplars)
        {
            if (ModelState.IsValid)
            {
                emprestimo.DataEmprestimo = DateTime.Now;
                emprestimo.ColaboradorId = 0;
                emprestimo.UsuarioId = Convert.ToInt32(exemplars.Select(x => x.UsuarioId).FirstOrDefault());
                emprestimo.Status = 1;
                db.Emprestimos.Add(emprestimo);
                db.SaveChanges();

                EmprestimoItens itens = new EmprestimoItens();
                DateTime DataLimite = DateTime.Now.AddDays(5);

                foreach (EmprestimoItens item in exemplars)
                {
                    itens.ColaboradorId = emprestimo.ColaboradorId;
                    itens.UsuarioId = emprestimo.UsuarioId;
                    itens.DataEmprestimo = DateTime.Now;
                    itens.EmprestimoId = emprestimo.Id;
                    itens.ExemplarId = item.ExemplarId;
                    itens.DataDevolucao = null;
                    itens.DataRenovacao = null;
                    itens.DataLimite = DataLimite;

                    db.EmprestimoItens.Add(itens);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            return View(emprestimo);
        }

        // GET: Emprestimos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emprestimo emprestimo = db.Emprestimos.Find(id);
            if (emprestimo == null)
            {
                return HttpNotFound();
            }
            return View(emprestimo);
        }

        // POST: Emprestimos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ColaboradorId,UsuarioId,DataEmprestimo,Status")] Emprestimo emprestimo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emprestimo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(emprestimo);
        }

        // GET: Emprestimos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emprestimo emprestimo = db.Emprestimos.Find(id);
            if (emprestimo == null)
            {
                return HttpNotFound();
            }
            return View(emprestimo);
        }

        // POST: Emprestimos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Emprestimo emprestimo = db.Emprestimos.Find(id);
            db.Emprestimos.Remove(emprestimo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult PesquisaCodigoDeBarras(string codigo)
        {
            LivroExemplar result = db.Exemplares.Where(x => x.CodigoDeBarras == codigo).Include(x => x.Livros).FirstOrDefault();
            string _mensagem = "ok";
            if (result == null)
            {
                _mensagem = "Codigo de barras invalido ou produto nao existe";
                return Json(new
                {
                    mensagem = _mensagem
                }, JsonRequestBehavior.AllowGet);
            }
            else
                if (result.Status == LivroExemplarStatusEnum.Indiponivel)
            {
                _mensagem = "Exemplar indisponivel";
                return Json(new
                {
                    mensagem = _mensagem
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                codigoDeBarras = result.CodigoDeBarras,
                exemplarId = result.Id,
                titulo = result.Livros.Titulo,
                autor = result.Livros.Autor,
                edicao = result.Livros.Edicao,
                editora = result.Livros.Editora,
                mensagem = _mensagem
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EmprestimoALuno(string matricula)
        {
            Pessoa result = db.Pessoas.Where(x => x.Matricula == matricula && x.Tipo == TipoPessoaEnum.Usuario).FirstOrDefault();
            string _mensagem = "ok";
            if (result == null)
            {
                _mensagem = "Matricula invalidao ou usuario nao existe";
                return Json(new
                {
                    mensagem = _mensagem
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                usuarioId = result.Id,
                nome = result.Nome,
                mensagem = _mensagem
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AutenticaEmprestimo(int usuarioId, string senha)
        {
            Pessoa result = db.Pessoas.Where(x => x.Id == usuarioId && x.Senha == senha).FirstOrDefault();
            string _mensagem = "ok";
            if (result == null)
            {
                _mensagem = "senha incorreta";
                return Json(new
                {
                    mensagem = _mensagem
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                mensagem = _mensagem
            }, JsonRequestBehavior.AllowGet);
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
