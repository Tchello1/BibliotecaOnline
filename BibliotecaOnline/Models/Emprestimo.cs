using System;
using System.Collections.Generic;

namespace BibliotecaOnline.Models
{
    public class Emprestimo
    {
        public Emprestimo(int colaboradorId, int usuarioId, DateTime dataEmprestimo, int status)
        {
            ColaboradorId = colaboradorId;
            UsuarioId = usuarioId;
            DataEmprestimo = dataEmprestimo;
            Status = status;
        }

        public Emprestimo()
        {

        }

        public int Id { get; set; }
        public int ColaboradorId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public int Status { get; set; }

        public ICollection<EmprestimoItens> EmprestimoItens { get; set; }
    }
}