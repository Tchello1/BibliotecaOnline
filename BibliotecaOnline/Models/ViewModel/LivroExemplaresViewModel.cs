
﻿using BibliotecaOnline.Models.Enum;

﻿using System.ComponentModel.DataAnnotations;


namespace BibliotecaOnline.Models.ViewModel
{
    public class LivroExemplaresViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Título")]
        public string Titulo { get; set; }
        public string Autor { get; set; }
        [Display(Name = "Código de barras")]
        public string CodigoDeBarras { get; set; }
        public string Estante { get; set; }
        public string Setor { get; set; }
        public string Campus { get; set; }
        public LivroExemplarStatusEnum Status { get; set; }
    }
}