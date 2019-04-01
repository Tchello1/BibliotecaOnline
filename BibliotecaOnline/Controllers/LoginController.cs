using BibliotecaOnline.Filters;
using BibliotecaOnline.Models;
using System.Linq;
using System.Web.Mvc;

namespace BibliotecaOnline.Controllers
{
    public class LoginController : Controller
    {
        private Sessao sessao = new Sessao();
        private readonly Context db = new Context();

        // GET: Login
        [AllowAnonymous]
        [OutputCache(Duration = 20)]
        public ActionResult Index(string Msg)
        {
            if (sessao.VerificaSessao())
            {
                TempData["Msg"] = Msg;
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: Login
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(Login model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                TempData["erroLogin"] = "";
                Pessoa user = db.Pessoas.Where(x => x.Matricula == model.Matricula && x.Senha == model.Senha).FirstOrDefault();

                if (user != null)
                {
                    sessao.Login(user);
                    if (ReturnUrl != null && ReturnUrl != "")
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "A matricula ou senha estão incorretas");
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult LogOut()
        {
            sessao.Logout();
            return RedirectToAction("Index_", "Home", null);
        }
    }
}