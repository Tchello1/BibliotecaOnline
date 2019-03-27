using BibliotecaOnline.Models.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaOnline.Models
{
    public class LivroExemplar
    {
        public LivroExemplar(int id, string codigoDeBarras, string estante, string setor, string campus, int livroId)
        {
            CodigoDeBarras = codigoDeBarras;
            Estante = estante;
            Setor = setor;
            Campus = campus;
            Status = LivroExemplarStatusEnum.Disponivel;
            LivroId = livroId;
            EmprestimoItens itens = new EmprestimoItens();
        }
        public LivroExemplar() { }

        public int Id { get; set; }
        [Display(Name = "Código de barras")]
        public string CodigoDeBarras { get; set; }
        public string Estante { get; set; }
        public string Setor { get; set; }
        public string Campus { get; set; }
        public int Quantidade { get; set; }
        public LivroExemplarStatusEnum Status { get; set; }
        public int LivroId { get; set; }
        public virtual Livro Livros { get; set; }
        public ICollection<EmprestimoItens> EmprestimoItens { get; set; }
    }
}