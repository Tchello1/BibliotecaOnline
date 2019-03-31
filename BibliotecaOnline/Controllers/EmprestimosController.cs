using BibliotecaOnline.Filters;
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
    [Authentication]
    public class EmprestimosController : Controller
    {
        private Context db = new Context();
        private readonly Sessao sessao = new Sessao();

        // GET: Emprestimos
        public ActionResult Index()
        {
            List<EmprestimosViewModel> emprestimo = (from x in db.Emprestimos
                                                     join c in db.Pessoas on x.ColaboradorId equals c.Id
                                                     join ci in db.Cidades on c.Cidade equals ci.Codigo
                                                     from u in db.Pessoas.Where(u => u.Id == x.UsuarioId).DefaultIfEmpty()
                                                     where c.Cidade == u.Cidade
                                                     select new EmprestimosViewModel
                                                     {
                                                         Id = x.Id,
                                                         Usuario = u.Nome,
                                                         Campus = ci.Nome + " - " + ci.UF,
                                                         Emprestimo = x.DataEmprestimo,
                                                         Colaborador = c.Nome
                                                     }).ToList();


            return View(emprestimo);
        }

        // GET: Emprestimos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<EmprestimoItensViewModel> emprestimo = (from x in db.EmprestimoItens
                                                         join l in db.Livros on x.LivroId equals l.Id
                                                         join e in db.Exemplares on x.ExemplarId equals e.Id
                                                         join u in db.Pessoas on x.UsuarioId equals u.Id
                                                         where x.EmprestimoId == id
                                                         select new EmprestimoItensViewModel
                                                         {
                                                             CodigoDeBarras = e.CodigoDeBarras,
                                                             Titulo = l.Titulo,
                                                             Edicao = l.Edicao,
                                                             Idioma = l.Idioma,
                                                             Editora = l.Editora,
                                                             Autor = l.Autor,
                                                             DataEmprestimo = x.DataEmprestimo,
                                                             DataDevolucao = x.DataDevolucao,
                                                             Status = x.Status,
                                                             Usuario = u.Nome,
                                                             Matricula = u.Matricula
                                                         }).ToList();


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
                emprestimo.Status = EmprestimosStatusEnum.Andamento;
                emprestimo.ColaboradorId = sessao.UsuarioId();
                db.Emprestimos.Add(emprestimo);
                db.SaveChanges();

                EmprestimoItens itens = new EmprestimoItens();

                DateTime DataLimite = DateTime.Now.AddDays(5);

                foreach (EmprestimoItens item in exemplars)
                {
                    itens.ColaboradorId = emprestimo.ColaboradorId;
                    itens.UsuarioId = emprestimo.UsuarioId;
                    itens.ColaboradorId = emprestimo.ColaboradorId;
                    itens.DataEmprestimo = DateTime.Now;
                    itens.EmprestimoId = emprestimo.Id;
                    itens.LivroId = db.Exemplares.Where(x => x.Id == item.ExemplarId).Select(x => x.LivroId).FirstOrDefault();
                    itens.ExemplarId = item.ExemplarId;
                    itens.DataDevolucao = null;
                    itens.DataRenovacao = null;
                    itens.DataLimite = DataLimite;
                    itens.Status = LivroExemplarStatusEnum.Emprestado;

                    db.EmprestimoItens.Add(itens);
                    db.SaveChanges();


                    LivroExemplar ex = db.Exemplares.Where(x => x.Id == item.ExemplarId).FirstOrDefault();

                    if (item.ExemplarId == ex.Id)
                    {
                        ex.Status = LivroExemplarStatusEnum.Emprestado;
                        db.Entry(ex).State = EntityState.Modified;
                        db.SaveChanges();
                    }
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
        public JsonResult PesquisaCodigoDeBarras(string codigo, int usuarioId)
        {
            LivroExemplar result = db.Exemplares.Where(x => x.CodigoDeBarras == codigo).Include(x => x.Livros).FirstOrDefault();

            EmprestimoItens emprestimo = db.EmprestimoItens.Where(x => x.LivroId == result.LivroId && x.UsuarioId == usuarioId && x.Exemplares.Status == LivroExemplarStatusEnum.Emprestado).FirstOrDefault();
            string _mensagem = "ok";

            if (emprestimo != null)
            {
                _mensagem = "Este usuario ja possui um exemplar deste livro";
                return Json(new
                {
                    mensagem = _mensagem
                }, JsonRequestBehavior.AllowGet);
            }

            if (result == null)
            {
                _mensagem = "Codigo de barras invalido ou produto nao existe";
                return Json(new
                {
                    mensagem = _mensagem
                }, JsonRequestBehavior.AllowGet);
            }
            else
                if (result.Status == LivroExemplarStatusEnum.Emprestado)
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
                _mensagem = "Matricula invalida ou usuario nao existe";
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
