using System.ComponentModel.DataAnnotations;

namespace BibliotecaOnline.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Informe a matricula", AllowEmptyStrings = false)]
        public string Matricula { get; set; }
        [Required(ErrorMessage = "Informe a senha", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        public string Mensagem { get; set; }
    }
}