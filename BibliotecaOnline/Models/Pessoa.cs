using BibliotecaOnline.Models.Enum;

namespace BibliotecaOnline.Models
{
    public class Pessoa
    {
        public int Id { get; set; }
        public TipoPessoaEnum Tipo { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Matricula { get; set; }
        public string Senha { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public PessoaStatusEnum Status { get; set; }
    }
}