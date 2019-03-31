using BibliotecaOnline.Filters;
using BibliotecaOnline.Models;
using BibliotecaOnline.Models.Enum;
using BibliotecaOnline.Models.ViewModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace BibliotecaOnline.Controllers
{
    [Authentication]
    public class ExemplaresController : Controller
    {
        private Context db = new Context();

        // GET: Exemplares
        public ActionResult Index()
        {

            IQueryable<LivroExemplaresViewModel> exemplares = (from l in db.Exemplares.Include(l => l.Livros)
                                                               join c in db.Cidades on l.Campus equals c.Codigo
                                                               select new LivroExemplaresViewModel
                                                               {
                                                                   Id = l.Id,
                                                                   Titulo = l.Livros.Titulo,
                                                                   Autor = l.Livros.Autor,
                                                                   Edicao = l.Livros.Edicao,
                                                                   Editora = l.Livros.Editora,
                                                                   CodigoDeBarras = l.CodigoDeBarras,
                                                                   Estante = l.Estante,
                                                                   Setor = l.Setor,
                                                                   Campus = c.Nome + " - " + c.UF,
                                                                   Status = l.Status
                                                               });

            return View(exemplares.ToList());
        }

        // GET: Exemplares/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LivroExemplar livroExemplar = db.Exemplares.Find(id);
            if (livroExemplar == null)
            {
                return HttpNotFound();
            }
            return View(livroExemplar);
        }

        // GET: Exemplares/Create
        public ActionResult Create()
        {
            ViewBag.LivroId = new SelectList(db.Livros, "Id", "Titulo");
            return View();
        }

        // POST: Exemplares/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CodigoDebarras,Estante,Setor,Campus,Quantidade,Status,LivroId")] LivroExemplar livroExemplar)
        {
            if (ModelState.IsValid)
            {
                for (int i = 0; i < livroExemplar.Quantidade; i++)
                {

                    string codigoDeBarras = Regex.Replace(Guid.NewGuid().ToString(), "[^.0-9]", "");

                    if (codigoDeBarras.Length < 20)
                    {
                        codigoDeBarras = codigoDeBarras + "".PadRight(20 - codigoDeBarras.Length, '0');
                    }
                    else
                    {
                        codigoDeBarras = codigoDeBarras.Substring(0, 20);
                    }
                    livroExemplar.CodigoDeBarras = codigoDeBarras;
                    livroExemplar.Status = LivroExemplarStatusEnum.Disponivel;
                    db.Exemplares.Add(livroExemplar);

                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            ViewBag.LivroId = new SelectList(db.Livros, "Id", "Titulo", livroExemplar.LivroId);
            return View(livroExemplar);
        }

        // GET: Exemplares/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //LivroExemplar livroExemplar = db.Exemplares.Find(id);
            LivroExemplar livroExemplar = db.Exemplares.Include(l => l.Livros).Where(l => l.Id == id).FirstOrDefault();

            if (livroExemplar == null)
            {
                return HttpNotFound();
            }
            ViewBag.LivroId = new SelectList(db.Livros, "Id", "Titulo", livroExemplar.LivroId);
            return View(livroExemplar);
        }

        // POST: Exemplares/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "Id,CodigoDeBarras,Estante,Setor,Campus,Status,LivroId")] LivroExemplar livroExemplar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(livroExemplar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LivroId = new SelectList(db.Livros, "Id", "Titulo", livroExemplar.LivroId);
            return View(livroExemplar);
        }

        // GET: Exemplares/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LivroExemplar livroExemplar = db.Exemplares.Find(id);
            if (livroExemplar == null)
            {
                return HttpNotFound();
            }
            return View(livroExemplar);
        }

        // POST: Exemplares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LivroExemplar livroExemplar = db.Exemplares.Find(id);
            db.Exemplares.Remove(livroExemplar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult AutoCompleteCampus(string campus)

        {
            var result = db.Cidades.Where(x => x.Nome.Contains(campus) || x.Codigo == campus).OrderBy(x => x.Nome).Select(x => new
            {
                Nome = x.Nome + " - " + x.UF,
                Id = x.Codigo
            }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
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
