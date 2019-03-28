using System;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaOnline.Models.ViewModel
{
    public class EmprestimosViewModel
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Campus { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime Emprestimo { get; set; }
        public string Colaborador { get; set; }
    }
}