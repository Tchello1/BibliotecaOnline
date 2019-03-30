using BibliotecaOnline.Models.Enum;

namespace BibliotecaOnline.Models.ViewModel
{
    public class LivroExemplaresViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string CodigoDeBarras { get; set; }
        public string Estante { get; set; }
        public string Setor { get; set; }
        public string Campus { get; set; }
        public LivroExemplarStatusEnum Status { get; set; }
    }
}