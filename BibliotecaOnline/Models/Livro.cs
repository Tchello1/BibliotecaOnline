using System.Collections.Generic;

namespace BibliotecaOnline.Models
{
    public class Livro
    {
        public Livro(int id, string titulo, string autor, string editora, string ano, string idioma, string iSBN, string edicao)
        {
            Titulo = titulo;
            Autor = autor;
            Editora = editora;
            Ano = ano;
            Idioma = idioma;
            ISBN = iSBN;
            Edicao = edicao;
        }
        public Livro() { }
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Editora { get; set; }
        public string Ano { get; set; }
        public string Idioma { get; set; }
        public string ISBN { get; set; }
        public string Edicao { get; set; }
        public ICollection<LivroExemplar> Exemplares { get; set; }
    }
}