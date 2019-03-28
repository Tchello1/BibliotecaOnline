using System.Web.Mvc;

namespace BibliotecaOnline.Filters
{
    public class Authentication : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Sessao sessao = new Sessao();
            //Controller control = filterContext.Controller as Controller;

            if (!sessao.VerificaSessao())
            {
                ((Controller)filterContext.Controller).HttpContext.Response.Redirect("/Login/Index");
            }
        }
    }
}