using BibliotecaOnline.Filters;
using System.Web.Mvc;

namespace BibliotecaOnline.Controllers
{
    public class HomeController : Controller
    {
        [Authentication]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Index_()
        {
            return View();
        }
    }
}