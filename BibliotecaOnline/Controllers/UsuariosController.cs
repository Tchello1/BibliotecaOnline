using BibliotecaOnline.Filters;
using BibliotecaOnline.Models;
using BibliotecaOnline.Models.Enum;
using BibliotecaOnline.ViewModel;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace BibliotecaOnline.Controllers
{
    [Authentication]
    public class UsuariosController : Controller
    {
        private Context db = new Context();

        // GET: Usuarios
        public ActionResult Index()
        {
            IQueryable<PessoaViewModel> pessoas = (from l in db.Pessoas
                                                   join c in db.Cidades on l.Cidade equals c.Codigo
                                                   select new PessoaViewModel { Id = l.Id, Matricula = l.Matricula, Nome = l.Nome, Email = l.Email, UF = l.UF, Cidade = c.Nome, Tipo = l.Tipo, Status = l.Status });
            return View(pessoas);
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PessoaViewModel pessoa = (from l in db.Pessoas
                                      join c in db.Cidades on l.Cidade equals c.Codigo
                                      where l.Id == id
                                      select new PessoaViewModel { Id = l.Id, Matricula = l.Matricula, Nome = l.Nome, Email = l.Email, UF = l.UF, Cidade = c.Nome, Tipo = l.Tipo, Status = l.Status }).FirstOrDefault();

            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return View(pessoa);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            List<SelectListItem> selectList = db.Estados.OrderBy(x => x.Nome).Select(x => new SelectListItem { Text = x.Nome, Value = x.Uf }).ToList();

            ViewBag.Estado = selectList;
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Tipo,Nome,Email,Matricula,Senha,Cidade,UF,Status")] Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                pessoa.Status = PessoaStatusEnum.Ativo;
                db.Pessoas.Add(pessoa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pessoa);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = db.Pessoas.Find(id);


            List<SelectListItem> estado = db.Estados.OrderBy(x => x.Nome).Select(x => new SelectListItem { Text = x.Nome, Value = x.Uf }).ToList();

            ViewBag.Estado = estado;

            List<SelectListItem> cidade = db.Cidades.Where(x => x.UF == pessoa.UF).OrderBy(x => x.Nome).Select(x => new SelectListItem { Text = x.Nome, Value = x.Codigo }).ToList();


            ViewBag.Cidade = cidade;

            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return View(pessoa);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Tipo,Nome,Email,Matricula,Senha,Cidade,UF,Status")] Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                pessoa.Status = PessoaStatusEnum.Ativo;
                db.Entry(pessoa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pessoa);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = db.Pessoas.Find(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return View(pessoa);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pessoa pessoa = db.Pessoas.Find(id);
            db.Pessoas.Remove(pessoa);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ListaCidade(string uf)
        {
            List<SelectListItem> selectList = db.Cidades.Where(x => x.UF == uf).OrderBy(x => x.Nome).Select(x => new SelectListItem { Text = x.Nome, Value = x.Codigo }).ToList();

            return Json(selectList, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult VerificaUsuario(string matricula)
        {
            Pessoa result = db.Pessoas.Where(x => x.Matricula == matricula && x.Tipo == TipoPessoaEnum.Usuario).FirstOrDefault();
            string _mensagem = "ok";
            if (result == null)
            {
                return Json(new
                {
                    mensagem = _mensagem
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                mensagem = "Matricua ja vinculada a outro usuario"
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
