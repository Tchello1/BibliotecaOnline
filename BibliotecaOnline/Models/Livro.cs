using System.Collections.Generic;

namespace BibliotecaOnline.Models
{
    public class Livro
    {
        public int Id { get; set; }
        public string Autor { get; set; }
        public string Editora { get; set; }
        public int Ano { get; set; }
        public string Idioma { get; set; }
        public string ISBN { get; set; }
        public string Edicao { get; set; }
        public int LivroExemplarId { get; set; }
        public virtual LivroExemplar LivroExemplar { get; set; }
    }
}