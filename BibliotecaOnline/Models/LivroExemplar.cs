using System.Collections.Generic;

namespace BibliotecaOnline.Models
{
    public class LivroExemplar
    {
        public int Id { get; set; }
        public string CodigoDeBarras { get; set; }
        public string Estante { get; set; }
        public string Setor { get; set; }
        public string Campos { get; set; }
        public int Status { get; set; }
        public int LivroId { get; set; }
        public virtual ICollection<Livro> Livros { get; set; }

    }
}