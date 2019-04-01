using BibliotecaOnline.Filters;
using BibliotecaOnline.Models;
using BibliotecaOnline.Models.Enum;
using BibliotecaOnline.Models.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BibliotecaOnline.Controllers
{
    public class HomeController : Controller
    {
        private readonly Context db = new Context();
        private readonly Sessao sessao = new Sessao();

        [Authentication]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Index_()
        {
            if (sessao.VerificaSessao() == true)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public JsonResult AutoCompletePesquisa(string pesquisa)
        {
            var result = db.Livros.Where(x => x.Titulo.Contains(pesquisa) || x.Autor.Contains(pesquisa)
            || x.Editora.Contains(pesquisa)).OrderBy(x => x.Titulo).Select(x => new
            {
                Nome = x.Titulo + " - " + x.Autor,
                Id = x.Id
            }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult ResultadoPesquisa(int livroId)
        {
            List<PesquisaViewModel> resultado = (from x in db.Livros
                                                 join y in db.Exemplares on x.Id equals y.LivroId
                                                 join c in db.Cidades on y.Campus equals c.Codigo
                                                 where x.Id == livroId && y.Status == LivroExemplarStatusEnum.Disponivel
                                                 group new { x, y, c } by new { x.Titulo, x.Edicao, x.Idioma, x.Autor, x.Editora, x.Ano, c.Nome, c.UF, y.Setor, y.Estante }
                                                 into g
                                                 select new PesquisaViewModel
                                                 {
                                                     Titulo = g.Key.Titulo,
                                                     Edicao = g.Key.Edicao,
                                                     Idioma = g.Key.Idioma,
                                                     Ano = g.Key.Ano,
                                                     Autor = g.Key.Autor,
                                                     Editora = g.Key.Editora,
                                                     Campus = g.Key.Nome + " - " + g.Key.UF,
                                                     Setor = g.Key.Setor,
                                                     Estante = g.Key.Estante,
                                                     Quantidade = g.Count()
                                                 }).ToList();

            return Json(new
            {
                resultado
            }, JsonRequestBehavior.AllowGet);
        }
    }
}