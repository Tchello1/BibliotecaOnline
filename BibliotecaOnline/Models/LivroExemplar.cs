using BibliotecaOnline.Models.Enum;
using System.Collections.Generic;

namespace BibliotecaOnline.Models
{
    public class LivroExemplar
    {
        public LivroExemplar(int id, string codigoDeBarras, string estante, string setor, string campos, int livroId)
        {
            CodigoDeBarras = codigoDeBarras;
            Estante = estante;
            Setor = setor;
            Campos = campos;
            Status = LivroExemplarStatusEnum.Disponivel;
            LivroId = livroId;
        }
        public LivroExemplar() { }

        public int Id { get; set; }
        public string CodigoDeBarras { get; set; }
        public string Estante { get; set; }
        public string Setor { get; set; }
        public string Campos { get; set; }
        public int Quantidade { get; set; }
        public LivroExemplarStatusEnum Status { get; set; }
        public int LivroId { get; set; }
        public virtual Livro Livros { get; set; }
        public ICollection<EmprestimoItens> EmprestimoItens { get; set; }
    }
}