using System.Web.Mvc;

namespace BibliotecaOnline.Filters
{
    public class Authentication : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Sessao sessao = new Sessao();
            string control = filterContext.Controller.ToString().Replace("BibliotecaOnline.Controllers.", "").Replace("Controller", "");

            string tipoUsuario = sessao.UsuarioTipo();

            if (tipoUsuario == "Usuario" && (control == "Usuarios" || control == "Devolucoes" || control == "Livros" || control == "Exemplares"))
            {
                ((Controller)filterContext.Controller).HttpContext.Response.Redirect("/Login/Index");
            }

            if (tipoUsuario == "Colaborador" && (control == "Usuarios" || control == "Livros" || control == "Exemplares"))
            {
                ((Controller)filterContext.Controller).HttpContext.Response.Redirect("/Login/Index");
            }


            if (!sessao.VerificaSessao())
            {
                ((Controller)filterContext.Controller).HttpContext.Response.Redirect("/Login/Index");
            }
        }
    }
}