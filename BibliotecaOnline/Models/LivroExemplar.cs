namespace BibliotecaOnline.Models
{
    public class LivroExemplar
    {
        public LivroExemplar(int id, string codigoDeBarras, string estante, string setor, string campos, int status, int livroId)
        {
            CodigoDeBarras = codigoDeBarras;
            Estante = estante;
            Setor = setor;
            Campos = campos;
            Status = status;
            LivroId = livroId;
        }
        public LivroExemplar() { }

        public int Id { get; set; }
        public string CodigoDeBarras { get; set; }
        public string Estante { get; set; }
        public string Setor { get; set; }
        public string Campos { get; set; }
        public int Quantidade { get; set; }
        public int Status { get; set; }
        public int LivroId { get; set; }
        public virtual Livro Livros { get; set; }
    }
}