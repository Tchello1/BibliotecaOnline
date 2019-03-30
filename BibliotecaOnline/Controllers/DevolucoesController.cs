using BibliotecaOnline.Filters;
using BibliotecaOnline.Models;
using BibliotecaOnline.Models.Enum;
using BibliotecaOnline.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace BibliotecaOnline.Controllers
{
    [Authentication]
    public class DevolucoesController : Controller
    {
        private Context db = new Context();
        private readonly Sessao sessao = new Sessao();

        // GET: Devolucoes
        public ActionResult Index()
        {
            List<EmprestimosViewModel> devolucao = (from x in db.Emprestimos
                                                    join i in db.EmprestimoItens on x.Id equals i.EmprestimoId
                                                    join c in db.Pessoas on i.ColaboradorIdDevolucao equals c.Id
                                                    join ci in db.Cidades on c.Cidade equals ci.Codigo
                                                    from u in db.Pessoas.Where(u => u.Id == x.UsuarioId).DefaultIfEmpty()
                                                    where c.Cidade == u.Cidade && i.Status == LivroExemplarStatusEnum.Devolvido
                                                    select new EmprestimosViewModel
                                                    {
                                                        Id = x.Id,
                                                        Usuario = u.Nome,
                                                        Campus = ci.Nome + " - " + ci.UF,
                                                        Emprestimo = x.DataEmprestimo,
                                                        Devolucao = i.DataDevolucao,
                                                        Colaborador = c.Nome
                                                    }).ToList();
            return View(devolucao);
        }

        // GET: Devolucoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<EmprestimoItensViewModel> devolucao = (from x in db.EmprestimoItens
                                                        join l in db.Livros on x.LivroId equals l.Id
                                                        join e in db.Exemplares on x.ExemplarId equals e.Id
                                                        join u in db.Pessoas on x.UsuarioId equals u.Id
                                                        where x.EmprestimoId == id && x.Status == Models.Enum.LivroExemplarStatusEnum.Devolvido
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


            return View(devolucao);
        }

        // GET: Devolucoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Devolucoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(List<EmprestimoItens> exemplars)
        {
            if (ModelState.IsValid)
            {
                foreach (EmprestimoItens item in exemplars)
                {
                    LivroExemplar exemplar = db.Exemplares.Find(item.ExemplarId);
                    exemplar.Status = LivroExemplarStatusEnum.Disponivel;

                    db.Entry(exemplar).State = EntityState.Modified;
                    db.SaveChanges();


                    EmprestimoItens emprestimoItem = db.EmprestimoItens.Find(item.ExemplarId);
                    emprestimoItem.Status = LivroExemplarStatusEnum.Devolvido;
                    emprestimoItem.DataDevolucao = DateTime.Now;
                    emprestimoItem.ColaboradorIdDevolucao = sessao.UsuarioId();

                    db.Entry(emprestimoItem).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return Json(new
                {
                    mensagem = "ok"
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                mensagem = ""
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PesquisaCodigoDeBarras(string codigo)
        {
            if (!string.IsNullOrEmpty(codigo) && !string.IsNullOrWhiteSpace(codigo))
            {
                LivroExemplar result = db.Exemplares.Where(x => x.CodigoDeBarras == codigo.Replace(" ", "")).Include(x => x.Livros).FirstOrDefault();

                EmprestimoItens emprestimo = db.EmprestimoItens.Where(x => x.LivroId == result.LivroId && x.Exemplares.Status == LivroExemplarStatusEnum.Empresatado).FirstOrDefault();
                string _mensagem = "ok";

                if (result == null)
                {
                    _mensagem = "Codigo de barras invalido ou produto nao existe";
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
            return Json(new
            {
                mensagem = ""
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
