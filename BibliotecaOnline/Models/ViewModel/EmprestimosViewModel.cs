using System.ComponentModel.DataAnnotations;

namespace BibliotecaOnline.Models.ViewModel
{
    public class EmprestimosViewModel
    {
        [Display(Name = "Código de barras")]
        public string CodigoDeBarras { get; set; }
        [Display(Name = "Título")]
        public string Titulo { get; set; }
        public string Autor { get; set; }
        [Display(Name = "Edição")]
        public string Edicao { get; set; }
        public string Editora { get; set; }
        public string Campus { get; set; }
        [Display(Name = "Data de empréstimo")]
        public string Emprestimo { get; set; }
        [Display(Name = "Data da devolução")]
        public string Devolucao { get; set; }
        [Display(Name = "Usuário")]
        public string Usuario { get; set; }
        public string Colaborador { get; set; }
    }
}