using BibliotecaOnline.Models;
using System;
using System.Web;
using System.Web.UI;

namespace BibliotecaOnline.Filters
{
    public class Sessao : Page
    {
        private HttpCookie cook;

        public Sessao()
        {
        }
        public void Login(Pessoa usuario)
        {
            Session["Login"] = usuario;
            Session["Login_Id"] = usuario.Id;
            Session["Login_Nome"] = usuario.Nome;
            Session["Login_Matricula"] = usuario.Matricula;
            Session["Login_Tipo"] = usuario.Tipo;
            //this.Session.Timeout = 14400;

            cook = HttpContext.Current.Request.Cookies["LOGIN_tcn_COOKIE"];

            if (cook == null)
            {
                HttpCookie cookie = new HttpCookie("LOGIN_tcn_COOKIE");
                cookie.Values["Login_Id"] = usuario.Id.ToString();
                cookie.Values["Login_Nome"] = usuario.Nome;
                cookie.Values["Login_Matricula"] = usuario.Matricula;
                cookie.Values["Login_Senha"] = usuario.Senha;
                cookie.Values["Login_Tipo"] = usuario.Tipo.ToString();
                cookie.Values["Logado"] = "1";
                //cookie.Expires = DateTime.Now.AddHours(12);
                System.Web.HttpContext.Current.Response.AppendCookie(cookie);
            }
            else
            {
                bool Alterou = false;

                if (cook["Login_Id"] != usuario.Id.ToString())
                {
                    cook["Login_Id"] = usuario.Id.ToString();
                    Alterou = true;
                }

                if (cook["Login_Nome"] != usuario.Nome)
                {
                    cook["Login_Nome"] = usuario.Nome;
                    Alterou = true;
                }
                if (cook["Login_Matricula"] != usuario.Matricula)
                {
                    cook["Login_Matricula"] = usuario.Matricula;
                    Alterou = true;
                }
                if (cook["Login_Senha"] != usuario.Senha)
                {
                    cook["Login_Senha"] = usuario.Senha;
                    Alterou = true;
                }
                if (cook["Login_Tipo"].ToString() != usuario.Tipo.ToString())
                {
                    cook["Login_Tipo"] = usuario.Tipo.ToString();
                    Alterou = true;
                }
                if (cook["Logado"] != "1")
                {
                    cook["Logado"] = "1";
                    Alterou = true;
                }

                if (Alterou)
                {
                    HttpContext.Current.Response.AppendCookie(cook);
                }
            }

        }
        public void Logout()
        {
            cook = HttpContext.Current.Request.Cookies["LOGIN_tcn_COOKIE"];
            if (cook != null)
            {
                cook["Logado"] = "0";
                System.Web.HttpContext.Current.Response.AppendCookie(cook);
            }
            Session.Abandon();
            Session.RemoveAll();
        }
        public int UsuarioId()
        {
            return Convert.ToInt32(Session["Login_Id"]);
        }
        public string UsuarioLogin()
        {
            return Session["Login_Matricula"].ToString();
        }
        public string UsuarioNome()
        {
            return Session["Login_Nome"].ToString();
        }
        public string UsuarioTipo()
        {
            return Session["Login_Tipo"].ToString();
        }
        public bool VerificaSessao()
        {
            bool flag;
            flag = (HttpContext.Current.Session["Login_Id"] != null ? true : false);

            if (flag == false)
            {
                cook = HttpContext.Current.Request.Cookies["LOGIN_tcn_COOKIE"];

                if (cook == null)
                {
                    flag = false;
                }
                else
                {
                    if (cook["Logado"] == "1")
                    {
                        Session["Login_Id"] = cook["Login_Id"];
                        Session["Login_Nome"] = cook["Login_Nome"];
                        Session["Login_Matricula"] = cook["Login_Matricula"];

                        System.Web.HttpContext.Current.Response.AppendCookie(cook);
                        flag = true;
                    }
                }
            }

            return flag;
        }
    }
}